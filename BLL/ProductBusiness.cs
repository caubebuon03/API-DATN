using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class ProductBusiness : IProductBusiness
    {
        private IIProductRepository _res;
        private IICategoryRepository categoryRepository;
        private IIBrandRepository brandRepository;
        public ProductBusiness(IIProductRepository ProductRes, IICategoryRepository categoryRepository, IIBrandRepository brandRepository)
        {
            _res = ProductRes;
            //khi có biến toàn cục trùng tên với tham số của, hàm, dùng this để trỏ tới biến toàn cục
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
        }
        public bool Create(ProductModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }

        public bool Update(ProductModel model)
        {
            return _res.Update(model);
        }
        public ProductModel GetDatabyID(int id)
        {
            var kq = _res.GetDatabyID(id);
            GetProductCategoryAndBrand(kq);
            return kq;
        }
        public List<ProductModel> GetDataAll(int page_index, int page_size, out long total)
        {
            var kq = _res.GetDataAll(page_index, page_size, out total);
            foreach (var item in kq)
            {
                GetProductCategoryAndBrand(item);
            }

            return kq;
        }

        public List<ProductModel> GetDataNew()
        {
            var kq = _res.GetDataNew();
            foreach (var item in kq)
            {
                GetProductCategoryAndBrand(item);
            }

            return kq;
        }

        public List<ProductModel> Gettuongtu(int product_id)
        {
            var kq = _res.Gettuongtu(product_id);
            foreach (var item in kq)
            {
                GetProductCategoryAndBrand(item);
            }

            return kq;
        }
        public List<ProductModel> Search(int pageIndex, int pageSize, out long total, string category_id)
        {
            var kq = _res.Search(pageIndex, pageSize, out total, category_id);
            foreach (var item in kq)
            {
                GetProductCategoryAndBrand(item);
            }

            return kq;

        }

        public List<ProductModel> Search1(int pageIndex, int pageSize, out long total, string brand_id)
        {
            var kq = _res.Search1(pageIndex, pageSize, out total, brand_id);
            foreach (var item in kq)
            {
                GetProductCategoryAndBrand(item);
            }

            return kq;

        }

        public List<ProductModel> TimKiem(int pageIndex, int pageSize, out long total, string product_name)
        {
            var kq = _res.TimKiem(pageIndex, pageSize, out total, product_name);
            foreach (var item in kq)
            {
                GetProductCategoryAndBrand(item);
            }

            return kq;
        }
        public List<ProductModel> TimKiemTrangChu(string keyWord, string maDanhMuc, string maThuongHieu, int? ram, int? minPrice, int? maxPrice, int? sort, int? pageIndex, int? pageSize, out long total)
        {
            var kq = _res.TimKiemTrangChu(keyWord, maDanhMuc, maThuongHieu, ram, minPrice, maxPrice, sort, pageIndex, pageSize, out total);
            foreach (var item in kq)
            {
                GetProductCategoryAndBrand(item);
            }

            return kq;
        }
        public void GetProductCategoryAndBrand(ProductModel product)
        {
            if (product!=null)
            {
                if (!string.IsNullOrWhiteSpace(product.category_id))
                {
                    product.Category = categoryRepository.GetDatabyID(product.category_id);
                }

                if (!string.IsNullOrWhiteSpace(product.brand_id))
                {
                    product.Brand = brandRepository.GetDatabyID(product.brand_id);
                }
            }
        }
    }
}
