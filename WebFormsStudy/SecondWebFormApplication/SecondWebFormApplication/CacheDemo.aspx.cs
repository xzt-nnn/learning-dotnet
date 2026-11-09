using System;
using System.Data;
using System.Runtime.Caching;
using SecondWebFormApplication.BLL;

namespace SecondWebFormApplication
{
    public partial class CacheDemo : System.Web.UI.Page
    {
        private readonly CityBLL bll = new CityBLL();
        private readonly MemoryCache _cache = MemoryCache.Default;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCityData();
            }
        }

        void LoadCityData()
        {
            const string cacheKey = "CityList";
            DataTable dt = _cache.Get(cacheKey) as DataTable;

            if (dt == null)
            {
                int total;
                dt = bll.GetCityPage(0, 10, out total);

                // 滑动过期10分钟
                var policy = new CacheItemPolicy
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                _cache.Set(cacheKey, dt, policy);

                lblInfo.Text = "✅ 从数据库查询，写入缓存，时间：" + DateTime.Now.ToString("HH:mm:ss");
            }
            else
            {
                lblInfo.Text = "✅ 直接读取缓存，不用查数据库，时间：" + DateTime.Now.ToString("HH:mm:ss");
            }
        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            _cache.Remove("CityList");
            LoadCityData();
        }
    }
}
