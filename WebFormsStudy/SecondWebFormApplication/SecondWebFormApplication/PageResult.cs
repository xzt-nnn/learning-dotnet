namespace SecondWebFormApplication
{
    /// <summary>
    /// 统一分页返回对象
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (PageSize == 0) return 0;
                return (TotalCount + PageSize - 1) / PageSize;
            }
        }
        /// <summary>
        /// 当前页数据
        /// </summary>
        public T Data { get; set; }
    }
}
