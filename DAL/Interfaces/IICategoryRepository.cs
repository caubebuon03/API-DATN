using Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace DAL.Interfaces
{
    public partial interface IICategoryRepository
    {
        bool Create(CategoryModel model);
        bool Update(CategoryModel model);
        bool Delete(string id);
        CategoryModel GetDatabyID(string id);
        List<CategoryModel> GetData();
        List<CategoryModel> TimKiem(int pageIndex, int pageSize, out long total, string category_name);
    }
}
