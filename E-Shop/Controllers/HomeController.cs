using E_Shop.Models;
using E_Shop.Models.Authentication;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace E_Shop.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNum = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham=db.TDanhMucSps.AsNoTracking().OrderBy(x=>x.TenSp);
            PagedList<TDanhMucSp> lst=new PagedList<TDanhMucSp>(lstsanpham, pageNum, pageSize);
            return View(lst);
        }

        [Authentication]
        public IActionResult SanPhamTheoLoai(string maloai, int? page)
        {
            int pageSize = 8;
            int pageNum = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().Where(x=>x.MaLoai==maloai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNum, pageSize);
            ViewBag.maloai=maloai;
            return View(lst);
        }

        [Authentication]
        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanpham=db.TDanhMucSps.SingleOrDefault(x=>x.MaSp==maSp);
            var anhsanpham=db.TAnhSps.Where(x=>x.MaSp==maSp).ToList();
            ViewBag.anhsanpham=anhsanpham;
            return View(sanpham);
        }
        public IActionResult ChiTietSanPhamVM(string maSp)
        {
            var sanpham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhsanpham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var ChiTietSanPhamVM = new ChiTietSanPhamViewModel { danhMucSp = sanpham, AnhSp = anhsanpham };
            return View(ChiTietSanPhamVM);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}