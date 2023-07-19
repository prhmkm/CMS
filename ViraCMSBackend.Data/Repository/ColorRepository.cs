using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Data.Repository
{
    public class ColorRepository : BaseRepository<Color>, IColorRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public ColorRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(Color color)
        {
            Create(color);
            Save();
        }

        public void Edit(Color color)
        {
            Update(color);
            Save();
        }

        public List<Color> GetAll()
        {
            return FindAll().Where(w=>w.IsDelete == false).ToList();
        }

        public Color GetById(int id)
        {
            return FindByCondition(w => w.Id == id).FirstOrDefault();
        }
    }
}
