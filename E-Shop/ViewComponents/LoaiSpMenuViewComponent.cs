using E_Shop.Models;
using E_Shop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSpRes;
        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRes)
        {
            _loaiSpRes = loaiSpRes;
        }
        public IViewComponentResult Invoke()
        {
            var loaiSp=_loaiSpRes.GetAllLoaiSp().OrderBy(x=>x.Loai);
            return View(loaiSp);
        }
    }
}
