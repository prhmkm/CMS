using static ViraCMSBackend.Domain.DTOs.ResultDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IResultService
    {
        List<ShowResult> results(string title, int pageSize, int id);
    }
}
