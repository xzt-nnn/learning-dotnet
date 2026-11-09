using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondMVCDemo.Models
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; } //当前页数据
        public int TotalCount { get; set; } //总记录数

        public int PageIndex { get; set; } //当前第几页

        public int PageSize { get; set; } //每页条数
                                        
        public int TotalPages  //总页数
        {
            get { return (int)Math.Ceiling((double)TotalCount / PageSize); }
        }


    }
}