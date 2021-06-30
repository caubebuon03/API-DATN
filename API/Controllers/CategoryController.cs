using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBusiness _categoryBusiness;
        public CategoryController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        [Route("get-category")]
        [HttpGet]
        public IEnumerable<CategoryModel> GetAllMenu()
        {
            return _categoryBusiness.GetData();
        }

        [Route("create-category")]
        [HttpPost]
        public CategoryModel CreateItem([FromBody] CategoryModel model)
        {
            
            model.category_id = Guid.NewGuid().ToString();
            model.parent_category_id = "1";
            _categoryBusiness.Create(model);
            return model;
        }

        [Route("delete-category")]
        [HttpPost]
        public IActionResult DeleteCategory([FromBody] Dictionary<string, object> formData)
        {
            string category_id = "";
            if (formData.Keys.Contains("category_id") && !string.IsNullOrEmpty(Convert.ToString(formData["category_id"]))) { category_id = Convert.ToString(formData["category_id"]); }
            _categoryBusiness.Delete(category_id);
            return Ok();
        }

        [Route("update-category")]
        [HttpPost]
        public CategoryModel UpdateCategory([FromBody] CategoryModel model)
        {
            model.parent_category_id = "1";
            _categoryBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public CategoryModel GetDatabyID(string id)
        {
            return _categoryBusiness.GetDatabyID(id);
        }



        [Route("search-category")]
        [HttpPost]
        public ResponseModel TimKiem([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string category_name = "";
                if (formData.Keys.Contains("category_name") && !string.IsNullOrEmpty(Convert.ToString(formData["category_name"]))) { category_name = Convert.ToString(formData["category_name"]); }
                long total = 0;
                var data = _categoryBusiness.TimKiem(page, pageSize, out total, category_name);
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
