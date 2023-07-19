using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Data.Interface
{
    public interface IColorRepository
    {
        void Add(Color color);
        void Edit(Color color);
        Color GetById (int id);
        List<Color> GetAll();
    }
}
