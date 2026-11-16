using LibraryDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDemo.Services
{
    public class BorrowService : IBorrowService
    {
        
        //借书的最大天数
        private const int BorrowDays = 30;

        //单个读者同时最多可以借几本书

        private const int MaxBorrowCount = 3;

        private readonly LibraryContext _db;

        public BorrowService(LibraryContext db)
        {
            _db = db;
        }

        public void Borrowed(int bookId, int readerId)
        {
            //判断这本书存不存在（要查图书表 BorrowRecords 是借阅记录表）
            var book = _db.Books.AsNoTracking().FirstOrDefault(b => b.Id == bookId);
            if (book == null) throw new BusinessException("该图书不存在");
            //判断用户存不存在
            var reader = _db.Readers.AsNoTracking().FirstOrDefault(r => r.Id == readerId);
            if (reader == null) throw new BusinessException("该读者不存在");
            if (reader.Status != 0) throw new BusinessException("该读者已停用，无法借阅图书");

            var now = DateTime.Now;

            //同一本书未还时不能再借一本（r.Status==0 表示这条借阅记录还没还）
            bool duplicated = _db.BorrowRecords.Any(r => r.ReaderId == readerId
            && r.BookId == bookId && r.Status == 0);
            if (duplicated) throw new BusinessException("该读者尚未归还本书，无法借阅图书");

            //预期未还的也不能借
            bool hasOverdue = _db.BorrowRecords.Any(r=>
            r.ReaderId==readerId && r.Status==0&&r.DueDate<now);
            if (hasOverdue) throw new BusinessException("该读者存在预期未还图书，不能继续借阅图书");

            int unreturned = GetUnreturnedCount(readerId);

            if (unreturned >= MaxBorrowCount)
                throw new BusinessException("该读者借阅数量已达上限，无法继续借阅");
            //开启事务
            using (var tx = _db.Database.BeginTransaction())
            {
                // 条件扣减库存：受影响行数为 0 说明已无可借副本
                int affected = _db.Database.ExecuteSqlCommand(
                    "UPDATE Book SET AvailableCopies = AvailableCopies - 1 WHERE Id = {0} AND AvailableCopies > 0",
                    bookId);

                if (affected != 1)
                {
                    //回滚事务
                    tx.Rollback();
                    throw new BusinessException("该图书已无可借副本");
                }

                _db.BorrowRecords.Add(new BorrowRecord
                {
                    BookId = bookId,
                    ReaderId = readerId,
                    BorrowDate = now,
                    DueDate = now.AddDays(BorrowDays),
                    Status = 0
                });
                _db.SaveChanges();
                //提交事务
                tx.Commit();
            }
        }

        public IList<Reader> GetActiveReaders()
        {
            return _db.Readers.AsNoTracking()
                .Where(r => r.Status == 0)
                .OrderBy(r => r.CardNo)
                .ToList();
        }

        public IList<BorrowRecord> GetRecords(bool onlyUnreturned = false)
        {
            var query = _db.BorrowRecords
                .Include("Book")
                .Include("Reader")
                .AsNoTracking()
                .AsQueryable();
            if (onlyUnreturned)
                query = query.Where(r => r.Status == 0);

            return query.OrderByDescending(r => r.BorrowDate)
                .ThenByDescending(r => r.Id)
                .ToList();
        }

        public int GetUnreturnedCount(int readerId)
        {
            return _db.BorrowRecords.Count(r => r.ReaderId == readerId && r.Status == 0);
        }

        public void Return(int recordId)
        {
            var record = _db.BorrowRecords.AsNoTracking().FirstOrDefault(r => r.Id == recordId);
            if (record == null) throw new BusinessException("借阅记录不存在");
            if (record.Status != 0) throw new BusinessException("该记录已归还，无需重复操作");

            var now = DateTime.Now;

            using (var tx = _db.Database.BeginTransaction())
            {
                // 条件更新：只有还在借出状态（Status=0）的记录才能被改成已还
                int affected = _db.Database.ExecuteSqlCommand(
                    "UPDATE BorrowRecord SET Status = 1, ReturnDate = {0} WHERE Id = {1} AND Status = 0",
                    now, recordId);

                if (affected != 1)
                {
                    tx.Rollback();
                    throw new BusinessException("该记录已被处理，请刷新后重试");
                }

                // 库存加回，同时保证不会超过馆藏总量
                int stock = _db.Database.ExecuteSqlCommand(
                    "UPDATE Book SET AvailableCopies = AvailableCopies + 1 WHERE Id = {0} AND AvailableCopies < TotalCopies",
                    record.BookId);

                if (stock != 1)
                {
                    tx.Rollback();
                    throw new BusinessException("库存数据异常：可借数量已达到馆藏总量");
                }

                tx.Commit();
            }
        }
    }
}