using SecondMVCDemo.DAL;
using SecondMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondMVCDemo.BLL
{
    //调用DAL
    public class CityBLL:ICityBLL
    {

        private readonly CityDAL cityDAL;

        public CityBLL(CityDAL cityDAL)
        {
            this.cityDAL = cityDAL;
        }

        //增加城市
        public int AddCity(City city)
        {
            int rows = cityDAL.addCity(city);
            if (rows > 0)
            {
                Console.WriteLine("城市添加成功");
                return rows;
            }
            else
            {
                throw new Exception("添加城市失败");
            }
        }

        public int deleteCity(int id)
        {
            return cityDAL.deleteCity(id);
        }

        public City detailCity(int id)
        {
            return cityDAL.detailCity(id);
        }

        //获取全部城市
        public List<City> getAllCity()
        {
            return cityDAL.getAllCity();
        }

        public PageResult<City> getPageCity(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            List<City> cityList = cityDAL.getPageCity(pageIndex, pageSize);
            int total = cityDAL.getCityCount();
            return new PageResult<City>
            {
                Items=cityList,
                TotalCount=total,
                PageIndex=pageIndex,
                PageSize=pageSize
            };
        }

        public int updateCity(City city)
        {
            return cityDAL.updateCity(city);
        }
    }
}