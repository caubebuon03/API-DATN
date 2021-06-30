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
    public class CustomerController : ControllerBase
    {
        private ICustomerBusiness _customerBusiness;
        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [Route("create-customer")]
        [HttpPost]
        public CustomerModel CreateCustomer([FromBody] CustomerModel model)
        {
            _customerBusiness.CreateCustomer(model);
            return model;
        }

        [Route("delete-customer")]
        [HttpPost]
        public IActionResult DeleteCustomer([FromBody] Dictionary<string, object> formData)
        {
            string customer_id = "";
            if (formData.Keys.Contains("customer_id") && !string.IsNullOrEmpty(Convert.ToString(formData["customer_id"]))) { customer_id = Convert.ToString(formData["customer_id"]); }
            _customerBusiness.Delete(customer_id);
            return Ok();
        }

        [Route("update-customer")]
        [HttpPost]
        public CustomerModel Updatecustomer([FromBody] CustomerModel model)
        {

            _customerBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public CustomerModel GetDatabyID(string id)
        {
            return _customerBusiness.GetDatabyID(id);
        }

        [Route("search-customer")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string customer_name = "";
                if (formData.Keys.Contains("customer_name") && !string.IsNullOrEmpty(Convert.ToString(formData["customer_name"]))) { customer_name = Convert.ToString(formData["customer_name"]); }
                string customer_email = "";
                if (formData.Keys.Contains("customer_email") && !string.IsNullOrEmpty(Convert.ToString(formData["customer_email"]))) { customer_email = Convert.ToString(formData["customer_email"]); }
                long total = 0;
                var data = _customerBusiness.Search(page, pageSize, out total, customer_name, customer_email);
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
