using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class HoaDonModel
    {
        public string ma_hoa_don { get; set; }
        public string ho_ten { get; set; }
        public string dia_chi { get; set; }
        public int so_dien_thoai { get; set; }
        public int total { get; set; }
        public List<ChiTietHoaDonModel> listjson_chitiet { get; set; }
        public int matrangthai { get; set; }
        public virtual TrangThaiDonHang TrangThai { get; set; }
        public string audittrail { get; set; }
        public bool ischangequantity { get; set; }
        public DateTime NgayDat { get; set; }
    }
}
