using SecondMVCDemo.Models;
using System.Collections.Generic;

namespace SecondMVCDemo.BLL
{
    public interface ICityBLL
    {
        List<City> getAllCity();

        PageResult<City> getPageCity(int pageIndex,int pageSize);

        int AddCity(City city);

        City detailCity(int id);

        int updateCity(City city);

        int deleteCity(int id);
    }
}