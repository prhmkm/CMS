using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IColorService
    {
        void Add(Color color);
        void Edit(Color color);
        Color GetById(int id);
        List<Color> GetAll();
    }
}
