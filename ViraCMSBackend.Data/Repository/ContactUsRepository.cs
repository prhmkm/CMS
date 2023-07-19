using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.ContactUsDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class ContactUsRepository : BaseRepository<ContactU>, IContactUsRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public ContactUsRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(ContactU contactUs)
        {
            Create(contactUs);
            Save();
        }

        public List<ShowMessages> BOGetAll(int id, int type)
        {
            List<ShowMessages> res = new List<ShowMessages>();
            foreach (var r in _repositoryContext.ContactUs.Where(w =>
            (id != 0 ? w.LanguageId == id : true &&
            type != 0 ? w.StateId == type : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowMessages
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        FullName = r.FullName,
                        Email = r.Email,
                        MobileNumber = r.MobileNumber,
                        Subject = r.Subject,
                        Description = r.Description,
                        StateId = r.StateId,
                        StateTitle = _repositoryContext.ContactStates.FirstOrDefault(w => w.Id == r.StateId && w.LanguageId == r.LanguageId).Title,
                        CreationDateTime = DateHelpers.ToPersianDate(r.CreationDateTime),
                    });
                }
            }
            return res;
        }

        public void EditReadSate(ContactU contactUs)
        {
            contactUs.StateId = _repositoryContext.ContactStates.FirstOrDefault(w => w.Id == 2 && w.LanguageId == contactUs.LanguageId).Id;
            Update(contactUs);
            Save();
        }

        public ContactU GetById(int id)
        {
            return FindByCondition(w => w.Id == id).FirstOrDefault();
        }
    }
}
