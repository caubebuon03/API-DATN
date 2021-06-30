using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBusiness _productBusiness;
        private string _path;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }

        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("create-product")]
        [HttpPost]
        public ProductModel CreateItem([FromBody] ProductModel model)
        {
            if (model.product_image != null)
            {
                var arrData = model.product_image.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"{arrData[0]}";
                    model.product_image = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }

            _productBusiness.Create(model);
            return model;
        }

        [Route("delete-product")]
        [HttpPost]
        public IActionResult DeleteProduct([FromBody] Dictionary<string, object> formData)
        {
            string product_id = "";
            if (formData.Keys.Contains("product_id") && !string.IsNullOrEmpty(Convert.ToString(formData["product_id"]))) { product_id = Convert.ToString(formData["product_id"]); }
            _productBusiness.Delete(product_id);
            return Ok();
        }

        [Route("update-product")]
        [HttpPost]
        public ProductModel UpdateUser([FromBody] ProductModel model)
        {
            if (model.product_image != null)
            {
                var arrData = model.product_image.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"{arrData[0]}";
                    model.product_image = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _productBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public ProductModel GetDatabyID(int id)
        {
            return _productBusiness.GetDatabyID(id);
        }

        [Route("get-all/{page_index}/{page_size}")]
        [HttpGet]
        public IEnumerable<ProductModel> GetDatabAll(int page_index, int page_size)
        {
            long total = 0;
            var kq = _productBusiness.GetDataAll(page_index, page_size, out total);
            foreach (var item in kq)
            {
                item.total = total;
            }
            return kq;
        }

        [Route("get-new")]
        [HttpGet]
        public IEnumerable<ProductModel> GetDataNew()
        {
            return _productBusiness.GetDataNew();
        }

        [Route("get-tuongtu/{id}")]
        [HttpGet]
        public IEnumerable<ProductModel> Gettuongtu(int id)
        {
            return _productBusiness.Gettuongtu(id);
        }


        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string category_id = "";
                if (formData.Keys.Contains("category_id") && !string.IsNullOrEmpty(Convert.ToString(formData["category_id"]))) { category_id = Convert.ToString(formData["category_id"]); }
                long total = 0;
                var data = _productBusiness.Search(page, pageSize, out total, category_id);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        [Route("search1")]
        [HttpPost]
        public ResponseModel Search1([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string brand_id = "";
                if (formData.Keys.Contains("brand_id") && !string.IsNullOrEmpty(Convert.ToString(formData["brand_id"]))) { brand_id = Convert.ToString(formData["brand_id"]); }
                long total = 0;
                var data = _productBusiness.Search1(page, pageSize, out total, brand_id);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        [Route("timkiem")]
        [HttpPost]
        public ResponseModel TimKiem([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string product_name = "";
                if (formData.Keys.Contains("product_name") && !string.IsNullOrEmpty(Convert.ToString(formData["product_name"]))) { product_name = Convert.ToString(formData["product_name"]); }

                long total = 0;
                var data = _productBusiness.TimKiem(page, pageSize, out total, product_name);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        [Route("tim-kiem-trang-chu/{keyWord?}/{maDanhMuc?}/{maThuongHieu?}/{ram?}/{minPrice?}/{maxPrice?}/{sort?}/{pageIndex?}/{pageSize?}")]
        public IActionResult TimKiemTrangChu(string keyWord, string maDanhMuc, string maThuongHieu, int? ram, int? minPrice, int? maxPrice, int? sort, int? pageIndex, int? pageSize)
        {
            long total;
            ResponseModel response = new ResponseModel();
            response.Data = _productBusiness.TimKiemTrangChu(keyWord, maDanhMuc, maThuongHieu, ram, minPrice, maxPrice, sort, pageIndex, pageSize, out total);
            response.NullableIndex = pageIndex;
            response.NullableSize = pageSize;
            response.TotalItems = total;
            return Ok(response);
        }
    }
}
