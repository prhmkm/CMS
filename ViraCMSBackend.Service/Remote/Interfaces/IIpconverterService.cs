

using ViraCMSBackend.Domain.DTOs;

namespace ViraCMSBackend.Service.Remote.Interfaces
{
    public interface IIpconverterService
    {
        GetCountryByIPDTO IpToCountryCode (string ip);
    }
}
