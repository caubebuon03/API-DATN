using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandBusiness _BrandBusiness;

        public BrandController(IBrandBusiness BrandBusiness)
        {
            _BrandBusiness = BrandBusiness;
        }

        [Route("get-brand")]
        [HttpGet]
        public IEnumerable<BrandModel> GetAllMenu()
        {
            return _BrandBusiness.GetData();
        }

        [Route("create-brand")]
        [HttpPost]
        public BrandModel CreateItem([FromBody] BrandModel model)
        {
            model.brand_id = Guid.NewGuid().ToString();
            model.parent_brand_id = "1";
            _BrandBusiness.Create(model);
            return model;
        }

        [Route("delete-brand")]
        [HttpPost]
        public IActionResult DeleteBrand([FromBody] Dictionary<string, object> formData)
        {
            string brand_id = "";
            if (formData.Keys.Contains("brand_id") && !string.IsNullOrEmpty(Convert.ToString(formData["brand_id"]))) { brand_id = Convert.ToString(formData["brand_id"]); }
            _BrandBusiness.Delete(brand_id);
            return Ok();
        }

        [Route("update-brand")]
        [HttpPost]
        public BrandModel UpdateBrand([FromBody] BrandModel model)
        {
            model.parent_brand_id = "1";
            _BrandBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public BrandModel GetDatabyID(string id)
        {
            return _BrandBusiness.GetDatabyID(id);
        }

        [Route("search-brand")]
        [HttpPost]
        public ResponseModel TimKiem([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string brand_name = "";
                if (formData.Keys.Contains("brand_name") && !string.IsNullOrEmpty(Convert.ToString(formData["brand_name"]))) { brand_name = Convert.ToString(formData["brand_name"]); }
                long total = 0;
                var data = _BrandBusiness.TimKiem(page, pageSize, out total, brand_name);
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
    }
}
