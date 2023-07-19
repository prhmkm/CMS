using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.MenuDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IMenuRepository
    {
        int Add(Menu menu);
        void Edit(Menu menu);
        Menu GetById(int id);
        Tuple<int, Menu> GetByOrderAndParent(int order,int parent, int langId);
        bool HaveChild(int id);
        List<ShowMenu> GetAll(int parentId, int langId);
        List<BOShowMenu> BOGetAll(int parentId, int langId);

    }
}
