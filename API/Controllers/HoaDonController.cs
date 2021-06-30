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
    public class HoaDonController : ControllerBase
    {
        private IHoaDonBusiness _hoaDonBusiness;

        public HoaDonController(IHoaDonBusiness hoaDonBusiness)
        {
            _hoaDonBusiness = hoaDonBusiness;
        }

        [Route("themHD")]
        [HttpPost]
        public HoaDonModel CreateItem([FromBody] HoaDonModel model)
        {
            model.ma_hoa_don = Guid.NewGuid().ToString();
            if (model.listjson_chitiet != null)
            {
                foreach (var item in model.listjson_chitiet)
                    item.ma_chi_tiet = Guid.NewGuid().ToString();
            }
            _hoaDonBusiness.Create(model);
            return model;
        }

        [Route("get-all")]
        [HttpGet]
        public IEnumerable<HoaDonModel> GetDatabAll()
        {
            return _hoaDonBusiness.GetDataAll();
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public HoaDonModel GetDatabyID(string id)
        {
            return _hoaDonBusiness.GetDatabyID(id);
        }

        [Route("change-status")]
        [HttpPost]
        public IActionResult ThayDoiTrangThai(HoaDonModel hoaDon)
        {
            _hoaDonBusiness.ThayDoiTrangThaiDonHang(hoaDon.ma_hoa_don, hoaDon.matrangthai, hoaDon.ischangequantity, hoaDon.audittrail);
            return Ok();
        }

        [Route("get-chi-tiet-by-hoa-don/{id}")]
        [HttpGet]
        public HoaDonModel GetCHitietByHoaDon(string id)
        {
            var kq = _hoaDonBusiness.GetChiTietByHoaDon(id);
            return kq;
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
                string ho_ten = "";
                if (formData.Keys.Contains("ho_ten") && !string.IsNullOrEmpty(Convert.ToString(formData["ho_ten"]))) { ho_ten = Convert.ToString(formData["ho_ten"]); }

                long total = 0;
                var data = _hoaDonBusiness.Search(page, pageSize, out total,ho_ten);
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

        [Route("all-trang-thai")]
        public IActionResult GetAllTrangThai()
        {
            return Ok(_hoaDonBusiness.GetAll());
        }
    }
}
