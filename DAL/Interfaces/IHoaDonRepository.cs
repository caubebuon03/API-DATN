using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface IHoaDonRepository
    {
        bool Create(HoaDonModel model);
        List<HoaDonModel> GetDataAll();
        HoaDonModel GetDatabyID(string id);
        List<HoaDonModel> Search(int pageIndex, int pageSize, out long total, string ho_ten);
        List<ChiTietHoaDonModel> GetChitietbyhoadon(string id);
        void ThayDoiTrangThaiDonHang(string orderid, int matrangthai, bool ischangequantity, string audittrail);
        TrangThaiDonHang GetTrangThaiByHoaDon(string mahoadon);
        List<TrangThaiDonHang> GetAll();
    }
}
