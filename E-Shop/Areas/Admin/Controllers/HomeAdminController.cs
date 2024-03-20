using E_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]

    public class HomeAdminController : Controller
    {
        QlbanVaLiContext qlbanVaLiContext=new QlbanVaLiContext();
        [Route("")]
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        public IActionResult danhmucsanpham(int? page)
        {
            int pageSize = 20;
            int pageNum = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = qlbanVaLiContext.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNum, pageSize);
            return View(lst);
        }
        [Route("themsanpham")]
        [HttpGet]
        public IActionResult themsanpham()
        {
            ViewBag.MaChatLieu = new SelectList(qlbanVaLiContext.TChatLieus.ToList(),"MaChatLieu","ChatLieu");
            ViewBag.MaHangSx = new SelectList(qlbanVaLiContext.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(qlbanVaLiContext.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(qlbanVaLiContext.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(qlbanVaLiContext.TLoaiDts.ToList(), "MaDt", "TenLoai");

            return View();
        }

        [Route("themsanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult themsanpham(TDanhMucSp sanpham)
        {
            if(ModelState.IsValid)
            {
                qlbanVaLiContext.TDanhMucSps.Add(sanpham);
                qlbanVaLiContext.SaveChanges();
                return RedirectToAction("danhmucsanpham");
            }
            return View(sanpham);
        }
    }
}
