using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class HoaDonBusiness : IHoaDonBusiness
    {
        private IHoaDonRepository _res;
        private IProductBusiness _rsp;

        public HoaDonBusiness(IHoaDonRepository res, IProductBusiness rsp)
        {
            _res = res;
            _rsp = rsp;
        }

        public bool Create(HoaDonModel model)
        {
            return _res.Create(model);
        }

        public HoaDonModel GetDatabyID(string id)
        {
            var kq = _res.GetDatabyID(id);
            kq.TrangThai = GetTrangThaiByHoaDon(kq.ma_hoa_don);

            return kq;
        }

        public void ThayDoiTrangThaiDonHang(string orderid, int matrangthai, bool ischangequantity, string audittrail)
        {
            _res.ThayDoiTrangThaiDonHang(orderid, matrangthai, ischangequantity, audittrail);
        }

        public HoaDonModel GetChiTietByHoaDon(string id)
        {
            var kq = _res.GetDatabyID(id);

            kq.listjson_chitiet = _res.GetChitietbyhoadon(id);
            foreach (var item in kq.listjson_chitiet)
            {
                item.product_name = _rsp.GetDatabyID(item.product_id).product_name;
                item.product_price = _rsp.GetDatabyID(item.product_id).product_price;
            }

            return kq;
        }

        public TrangThaiDonHang GetTrangThaiByHoaDon(string mahoadon)
        {
            return _res.GetTrangThaiByHoaDon(mahoadon);
        }

        public List<HoaDonModel> GetDataAll()
        {
            var kq = _res.GetDataAll();
            foreach (var item in kq)
            {
                item.TrangThai = GetTrangThaiByHoaDon(item.ma_hoa_don);
            }

            return kq;
        }

        public List<HoaDonModel> Search(int pageIndex, int pageSize, out long total, string ho_ten)
        {
            var kq = _res.Search(pageIndex, pageSize, out total, ho_ten);
            foreach (var item in kq)
            {
                item.TrangThai = GetTrangThaiByHoaDon(item.ma_hoa_don);
            }

            return kq;

        }

        public List<TrangThaiDonHang> GetAll()
        {
            return _res.GetAll();
        }
    }
}
