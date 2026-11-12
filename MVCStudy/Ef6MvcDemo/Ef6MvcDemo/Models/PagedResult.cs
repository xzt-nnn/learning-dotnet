using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ef6MvcDemo.Models
{
    //分页数据的类
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } //当前页的数据
        public int PageIndex { get; set; } //当前页码，从1开始
        public int PageSize { get; set; }//当前页有多少条数据
        public int TotalCount { get; set; }//总记录数
        public string Keyword { get; set; } //搜索关键词，翻页时要带回去

        public int TotalPages
        {
            get { return PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize); }
        }

        public bool HasPrev { get { return PageIndex > 1; } }
        public bool HasNext { get { return PageIndex < TotalPages; } }

    }
}
