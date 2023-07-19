using System;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.MenuDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public MenuRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public int Add(Menu menu)
        {
            Create(menu);
            Save();
            return menu.Id;
        }

        //public List<BOShowMenu> BOGetAll(int id)
        //{
        //    List<BOShowMenu> res = new List<BOShowMenu>();
        //    foreach (var r in _repositoryContext.Menus.Where(w => w.IsDelete == false &&
        //    (id != 0 ? w.LanguageId == id : true)
        //    ).ToList())
        //    {
        //        res.Add(new BOShowMenu
        //        {
        //            Id = r.Id,
        //            LanguageId = r.LanguageId,
        //            LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title,
        //            Title = r.Title,
        //            ParentId = r.ParentId,
        //            //Page = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(s => new ShowPages
        //            //{
        //            //    Id = s.Id,
        //            //    LanguageId = s.LanguageId,
        //            //    LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
        //            //    Name = s.Name,
        //            //    CustomHTML = s.CustomHTML,
        //            //}).FirstOrDefault(),
        //            HLink = r.HLink,
        //            PageId = r.PageId,
        //            Ordering = r.Ordering,
        //            IsActive = r.IsActive.GetValueOrDefault()
        //        });
        //    }
        //    return res.OrderBy(o => o.ParentId).ThenBy(o => o.Ordering).ToList();
        //}

        public void Edit(Menu menu)
        {
            Update(menu);
            Save();
        }

        //public List<ShowMenu> GetAll(int id)
        //{
        //    List<ShowMenu> res = new List<ShowMenu>();
        //    foreach (var r in _repositoryContext.Menus.Where(w => w.IsDelete == false &&
        //    w.IsActive == true &&
        //    (id != 0 ? w.LanguageId == id : true)
        //    ).ToList())
        //    {
        //        res.Add(new ShowMenu
        //        {
        //            Id = r.Id,
        //            LanguageId = r.LanguageId,
        //            LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title,
        //            Title = r.Title,
        //            ParentId = r.ParentId,
        //            //Page = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(s => new ShowPages
        //            //{
        //            //    Id = s.Id,
        //            //    LanguageId = s.LanguageId,
        //            //    LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
        //            //    Name = s.Name,
        //            //    CustomHTML = s.CustomHTML,
        //            //}).FirstOrDefault(),
        //            HLink = r.HLink,
        //            PageId = r.PageId,
        //            Ordering = r.Ordering,
        //        });
        //    }
        //    return res.OrderBy(o => o.ParentId).ThenBy(o => o.Ordering).ToList();
        //}

        public List<ShowMenu> GetAll(int parentId, int langId)
        {
            List<ShowMenu> response = new List<ShowMenu>();
            var res = _repositoryContext.Menus.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            w.ParentId == parentId &&
            (langId != 0 ? w.LanguageId == langId : true)
            ).OrderBy(o => o.ParentId).ThenBy(o => o.Ordering).ToList();

            foreach (var item in res)
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive == true)
                {
                    ShowMenu menu = new ShowMenu
                    {
                        Id = item.Id,
                        LanguageId = item.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title + "(غیرفعال)",
                        Title = item.Title,
                        ParentId = item.ParentId,
                        HLink = item.HLink,
                        PageId = item.PageId,
                        PageAlias = _repositoryContext.Pages.Where(w => w.Id == item.PageId).Select(w => w.Alias).FirstOrDefault(),
                        PageTitle = _repositoryContext.Pages.Where(w => w.Id == item.PageId).Select(w => w.Name).FirstOrDefault(),
                        Ordering = item.Ordering,
                        Childs = new List<ShowMenu>()
                    };
                    if (_repositoryContext.Menus.Any(w => w.IsDelete == false && w.IsActive == true && w.ParentId == item.Id && (langId != 0 ? w.LanguageId == langId : true)))
                    {
                        menu.Childs.AddRange(GetAll(item.Id, langId));
                    }
                    response.Add(menu);
                }
            }
            return response;
        }
        public List<BOShowMenu> BOGetAll(int parentId, int langId)
        {
            List<BOShowMenu> response = new List<BOShowMenu>();
            var res = _repositoryContext.Menus.Where(w => w.IsDelete == false &&
            w.ParentId == parentId &&
            (langId != 0 ? w.LanguageId == langId : true)
            ).OrderBy(o => o.ParentId).ThenBy(o => o.Ordering).ToList();

            foreach (var item in res)
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive == true)
                {
                    BOShowMenu menu = new BOShowMenu
                    {
                        Id = item.Id,
                        LanguageId = item.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title + "(غیرفعال)",
                        Title = item.Title,
                        ParentId = item.ParentId,
                        //Page = _repositoryContext.Pages.Where(w => w.Id == item.PageId).Select(s => new ShowPages
                        //{
                        //    Id = s.Id,
                        //    LanguageId = s.LanguageId,
                        //    LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == s.LanguageId).Title,
                        //    Name = s.Name,
                        //    CustomHTML = s.CustomHTML,
                        //}).FirstOrDefault(),
                        HLink = item.HLink,
                        PageId = item.PageId,
                        PageAlias = _repositoryContext.Pages.Where(w => w.Id == item.PageId).Select(w => w.Alias).FirstOrDefault(),
                        PageTitle = _repositoryContext.Pages.Where(w => w.Id == item.PageId).Select(w => w.Name).FirstOrDefault(),
                        Ordering = item.Ordering,
                        IsActive = item.IsActive.GetValueOrDefault(),
                        Childs = new List<BOShowMenu>()
                    };
                    if (_repositoryContext.Menus.Any(w => w.IsDelete == false && w.ParentId == item.Id && (langId != 0 ? w.LanguageId == langId : true)))
                    {
                        menu.Childs.AddRange(BOGetAll(item.Id, langId));
                    }
                    response.Add(menu);
                }
            }
            return response;
        }

        public Menu GetById(int id)
        {
            return FindByCondition(w => w.Id == id && w.IsDelete == false).FirstOrDefault();
        }

        public Tuple<int, Menu> GetByOrderAndParent(int order, int parent, int langId)
        {
            Menu menu = FindByCondition(w => w.Ordering == order && w.ParentId == parent && w.LanguageId == langId && w.IsDelete == false).FirstOrDefault();
            if (menu == null)
            {
                return Tuple.Create(0, menu);
            }
            else
            {
                return Tuple.Create(1, menu);
            }
        }

        public bool HaveChild(int id)
        {
            var child = FindByCondition(w => w.ParentId == id).ToList();
            if (child == null)
                return false;
            return true;
        }
    }
}
