using System;
using Model;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface ICategoryBusiness
    {
        bool Create(CategoryModel model);
        bool Update(CategoryModel model);
        CategoryModel GetDatabyID(string id);
        bool Delete(string id);
        List<CategoryModel> GetData();
        List<CategoryModel> TimKiem(int pageIndex, int pageSize, out long total, string category_name);
    }
}
