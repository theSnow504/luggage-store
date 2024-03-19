using E_Shop.Models;
namespace E_Shop.Repository
{
    public interface ILoaiSpRepository
    {
        TLoaiSp Add(TLoaiSp loaiSp);
        TLoaiSp Update(TLoaiSp loaiSp);
        TLoaiSp Delete(string maLoaiSp);
        TLoaiSp GetLoaiSp(string maLoaiSp);
        IEnumerable<TLoaiSp> GetAllLoaiSp();
    }
}
