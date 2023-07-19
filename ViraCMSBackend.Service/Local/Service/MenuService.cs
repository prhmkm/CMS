using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using static ViraCMSBackend.Domain.DTOs.MenuDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class MenuService : IMenuService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public MenuService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public int Add(Menu menu)
        {
            return _repository.Menu.Add(menu);
        }

        public void Edit(Menu menu)
        {
            _repository.Menu.Edit(menu);
        }

        public List<ShowMenu> GetAll(int parentId, int langId)
        {
            return _repository.Menu.GetAll(parentId,langId);
        }

        public List<BOShowMenu> BOGetAll(int parentId, int langId)
        {
            return _repository.Menu.BOGetAll(parentId, langId);
        }

        public Menu GetById(int id)
        {
            return _repository.Menu.GetById(id);
        }

        public Tuple<int, Menu> GetByOrderAndParent(int order, int parent, int langId)
        {
            return _repository.Menu.GetByOrderAndParent(order, parent, langId);
        }

        public bool HaveChild(int id)
        {
            return _repository.Menu.HaveChild(id);
        }
    }
}
