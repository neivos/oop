using System;
using System.Collections.Generic;

namespace QuanLyNganHang
{
    public class TaiKhoanNganHang
    {
        public string SoTaiKhoan { get; }
        public string CMNDChuTaiKhoan { get; }
        public string TenChuTaiKhoan { get; }
        public decimal SoDu { get; private set; }
        public decimal LaiSuat { get; }
        private List<GiaoDich> LichSuGiaoDich { get; }

        public TaiKhoanNganHang(string soTaiKhoan, string cmndChuTaiKhoan, string tenChuTaiKhoan, decimal soDu, decimal laiSuat)
        {
            SoTaiKhoan = soTaiKhoan;
            CMNDChuTaiKhoan = cmndChuTaiKhoan;
            TenChuTaiKhoan = tenChuTaiKhoan;
            SoDu = soDu;
            LaiSuat = laiSuat;
            LichSuGiaoDich = new List<GiaoDich>();
        }

        public void NhapTien(decimal soTien, DateTime ngay)
        {
            if (soTien <= 0)
                throw new ArgumentException("So tien phai lon hon 0.");
            SoDu += soTien;
            LichSuGiaoDich.Add(new GiaoDich(ngay, "Nhap tien", soTien));
        }

        public void RutTien(decimal soTien, DateTime ngay)
        {
            if (soTien <= 0)
                throw new ArgumentException("So tien phai lon hon 0.");
            if (soTien > SoDu)
                throw new InvalidOperationException("So du khong du.");
            SoDu -= soTien;
            LichSuGiaoDich.Add(new GiaoDich(ngay, "Rut tien", soTien));
        }

        public void TinhLaiSuat()
        {
            decimal tienLai = SoDu * LaiSuat / 100;
            SoDu += tienLai;
        }

        public void InBaoCao()
        {
            Console.WriteLine($"Tai khoan: {SoTaiKhoan}, Chu tai khoan: {TenChuTaiKhoan}, So du: {SoDu:C}");
            Console.WriteLine("Lich su giao dich:");
            foreach (var giaoDich in LichSuGiaoDich)
            {
                Console.WriteLine($"  {giaoDich}");
            }
        }
    }

    public class NganHang
    {
        private List<TaiKhoanNganHang> DanhSachTaiKhoan { get; }

        public NganHang()
        {
            DanhSachTaiKhoan = new List<TaiKhoanNganHang>();
        }

        public void ThemTaiKhoan(TaiKhoanNganHang taiKhoan)
        {
            DanhSachTaiKhoan.Add(taiKhoan);
        }

        public TaiKhoanNganHang TimTaiKhoan(string soTaiKhoan)
        {
            return DanhSachTaiKhoan.Find(tk => tk.SoTaiKhoan == soTaiKhoan);
        }

        public void TinhLaiSuatChoTatCa()
        {
            foreach (var taiKhoan in DanhSachTaiKhoan)
            {
                taiKhoan.TinhLaiSuat();
            }
        }

        public void InBaoCaoTatCaTaiKhoan()
        {
            Console.WriteLine("Bao cao toan bo tai khoan:");
            foreach (var taiKhoan in DanhSachTaiKhoan)
            {
                taiKhoan.InBaoCao();
                Console.WriteLine("----------------------------");
            }
        }
    }

    public class GiaoDich
    {
        public DateTime NgayGiaoDich { get; }
        public string KieuGiaoDich { get; }
        public decimal SoTien { get; }

        public GiaoDich(DateTime ngayGiaoDich, string kieuGiaoDich, decimal soTien)
        {
            NgayGiaoDich = ngayGiaoDich;
            KieuGiaoDich = kieuGiaoDich;
            SoTien = soTien;
        }

        public override string ToString()
        {
            return $"{NgayGiaoDich:dd/MM/yyyy} - {KieuGiaoDich}: {SoTien:C}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var nganHang = new NganHang();

            nganHang.ThemTaiKhoan(new TaiKhoanNganHang("001", "901", "Trinh Phuc Anh", 100, 5));
            nganHang.ThemTaiKhoan(new TaiKhoanNganHang("002", "902", "Nguyen Van A", 50, 5));
            nganHang.ThemTaiKhoan(new TaiKhoanNganHang("003", "901", "Hoang Van B", 200, 10));
            nganHang.ThemTaiKhoan(new TaiKhoanNganHang("004", "903", "Adam C", 200, 10));

            var taiKhoan = nganHang.TimTaiKhoan("001");
            taiKhoan?.NhapTien(100, new DateTime(2005, 7, 15));
            taiKhoan?.RutTien(10, new DateTime(2005, 7, 10));

            nganHang.TinhLaiSuatChoTatCa();

            nganHang.InBaoCaoTatCaTaiKhoan();
        }
    }
}
