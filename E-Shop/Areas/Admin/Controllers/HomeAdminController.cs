using E_Shop.Models;
using E_Shop.Models.Authentication;
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

        [Authentication]
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

        [Route("suasanpham")]
        [HttpGet]
        public IActionResult suasanpham(string maSp)
        {
            ViewBag.MaChatLieu = new SelectList(qlbanVaLiContext.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(qlbanVaLiContext.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(qlbanVaLiContext.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(qlbanVaLiContext.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(qlbanVaLiContext.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var sanpham = qlbanVaLiContext.TDanhMucSps.Find(maSp);
            return View(sanpham);
        }

        [Route("suasanpham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult suasanpham(TDanhMucSp sanpham)
        {
            if (ModelState.IsValid)
            {
                qlbanVaLiContext.Entry(sanpham).State = EntityState.Modified;
                qlbanVaLiContext.SaveChanges();
                return RedirectToAction("danhmucsanpham","HomeAdmin");
            }
            return View(sanpham);
        }

        [Route("xoasanpham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSp)
        {
            TempData["message"]="";
            var chitietSp = qlbanVaLiContext.TChiTietSanPhams.Where(x=>x.MaSp==maSp).ToList();
            if(chitietSp.Count() > 0)
            {
                TempData["message"] = "unable to delete";
                return RedirectToAction("danhmucsanpham", "HomeAdmin");
            }
            var anhSp= qlbanVaLiContext.TAnhSps.Where(x => x.MaSp == maSp);
            if(anhSp.Any()) qlbanVaLiContext.RemoveRange(anhSp);
            qlbanVaLiContext.Remove(qlbanVaLiContext.TDanhMucSps.Find(maSp));
            qlbanVaLiContext.SaveChanges();
            TempData["message"] = "delete success";
            
            return RedirectToAction("danhmucsanpham", "HomeAdmin");
        }

    }
}
