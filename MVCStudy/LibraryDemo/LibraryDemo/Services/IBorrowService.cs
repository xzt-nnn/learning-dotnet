using LibraryDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDemo.Services
{
    public interface IBorrowService
    {
        IList<Reader> GetActiveReaders();

        void Borrowed(int bookId, int readerId);

        int GetUnreturnedCount(int readerId);

        //借阅记录表，onlyUnreturned = true只列未还的
        IList<BorrowRecord> GetRecords(bool onlyUnreturned = false);

        //还书
        void Return(int recordId);

    }
}
