using System.Data;
using SecondWebFormApplication.DAL;

namespace SecondWebFormApplication.BLL
{
    public class CityBLL
    {
        private readonly CityDAL dal = new CityDAL();

        public DataTable GetCityPage(int pageIndex, int pageSize, out int totalCount)
        {
            return dal.GetCityPage(pageIndex, pageSize, out totalCount);
        }

        public DataTable GetDistinctProvince()
        {
            return dal.GetDistinctProvince();
        }

        public bool AddCity(string province, string name)
        {
            // 业务校验可以写在这里，比如禁止重复城市
            return dal.AddCity(province, name);
        }

        public bool DeleteCity(int id)
        {
            return dal.DeleteCity(id);
        }

        public bool UpdateCity(int id, string province, string name)
        {
            return dal.UpdateCity(id, province, name);
        }
    }
}
