using static ViraCMSBackend.Domain.DTOs.ResultDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IResultRepository
    {
        List<ShowResult> results(string title, int pageSize, int id);
    }
}
