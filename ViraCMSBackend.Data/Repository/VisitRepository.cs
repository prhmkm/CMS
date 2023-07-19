using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Data.Repository
{
    public class VisitRepository : BaseRepository<Visit>, IVisitRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public VisitRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(Visit visit)
        {
            Create(visit);
            Save();
        }
    }
}
