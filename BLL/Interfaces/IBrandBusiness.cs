using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface IBrandBusiness
    {
        bool Create(BrandModel model);
        bool Update(BrandModel model);
        BrandModel GetDatabyID(string id);
        bool Delete(string id);
        List<BrandModel> GetData();
        List<BrandModel> TimKiem(int pageIndex, int pageSize, out long total, string brand_name);
    }
}
