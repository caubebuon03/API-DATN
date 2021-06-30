using System;
using Model;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IProductBusiness
    {
        bool Create(ProductModel model);
        bool Update(ProductModel model);
        bool Delete(string id);
        ProductModel GetDatabyID(int id);
        List<ProductModel> GetDataAll(int page_index, int page_size, out long total);
        List<ProductModel> GetDataNew();
        List<ProductModel> Gettuongtu(int product_id);
        List<ProductModel> Search(int pageIndex, int pageSize, out long total, string category_id);
        List<ProductModel> Search1(int pageIndex, int pageSize, out long total, string brand_id);
        List<ProductModel> TimKiem(int pageIndex, int pageSize, out long total, string product_name);
        List<ProductModel> TimKiemTrangChu(string keyWord, string maDanhMuc, string maThuongHieu, int? ram, int? minPrice, int? maxPrice, int? sort, int? pageIndex, int? pageSize, out long total);
    }
}
