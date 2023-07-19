using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.ResultDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class ResultRepository : IResultRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public ResultRepository(ViraCMS_DBContext RepositoryContext) 
        {
            _repositoryContext = RepositoryContext;
        }

        public List<ShowResult> results(string title, int pageSize, int id)
        {
            List<ShowResult> response = new List<ShowResult>();
            List<string> Types = new List<string>() { "News", "Blogs", "Pages", };
            var res1 = _repositoryContext.News.Where(s => s.IsDelete == false && s.IsActive == true &&
            (s.Title.Contains(title) || s.CustomHTML.Contains(title) || s.ShortText.Contains(title)) &&
            (id != 0 ? s.LanguageId == id : true)
            ).ToList();
            foreach (var item in res1)
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive == true)
                {
                    ShowResult result = new ShowResult
                    {
                        LanguageId = item.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title + "(غیرفعال)",
                        Title = item.Title,
                        Alias = item.Alias,
                        Type = Types[0]
                    };
                    response.Add(result);
                }
            }
            var res2 = _repositoryContext.Blogs.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (w.Title.Contains(title) ||
            w.CustomHTML.Contains(title) ||
            w.ShortText.Contains(title)) &&
            (id != 0 ? w.LanguageId == id : true) 
            ).ToList();
            foreach (var item in res2)
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive == true)
                {
                    ShowResult result = new ShowResult
                    {
                        LanguageId = item.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title + "(غیرفعال)",
                        Title = item.Title,
                        Alias = item.Alias,
                        Type = Types[1]
                    };
                    response.Add(result);
                }
            }
            var res3 = _repositoryContext.Pages.Where(s => s.IsDelete == false &&
            s.IsActive == true &&
            (s.Name.Contains(title) ||
            s.CustomHTML.Contains(title)) &&
            (id != 0 ? s.LanguageId == id : true)
            ).ToList();
            foreach (var item in res3)
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive == true)
                {
                    ShowResult result = new ShowResult
                    {
                        LanguageId = item.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == item.LanguageId).Title + "(غیرفعال)",
                        Title = item.Name,
                        Alias = item.Alias,
                        Type = Types[2]
                    };
                    response.Add(result);
                }
            }
            if(pageSize == 0)
            {
                return response;
            }
            else
            {
                return response.Take(pageSize).ToList();
            }      
        }
    }
}
