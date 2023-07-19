using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Remote.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace ViraCMSBackend.Service.Remote.Service
{
    public class IpconverterService : IIpconverterService
    {

        private readonly AppSettings _appSetting;
        public IpconverterService(IOptions<AppSettings> appSettings)
        {
            _appSetting = appSettings.Value;
        }

        public GetCountryByIPDTO IpToCountryCode(string ip)
        {
            try
            {
                string userIP = ip;
                string apiKey = _appSetting.ApiKey;
                string url = _appSetting.Url + apiKey + "&ip=" + userIP;

                WebRequest request = WebRequest.Create(url);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // We try to use the "correct" charset
                    Encoding encoding = response.CharacterSet != null ? Encoding.GetEncoding(response.CharacterSet) : null;

                    using (var sr = encoding != null ? new StreamReader(response.GetResponseStream(), encoding) :
                                                       new StreamReader(response.GetResponseStream(), true))
                    {
                        var response2 = sr.ReadToEnd();
                        var parts = response2.Split(';');

                        if (parts.Length != 5)
                        {
                            return null;
                        }
                        if (parts[3] == "ERROR")
                            return null;

                        return new GetCountryByIPDTO
                        {
                            Code = parts[3],
                            IP = parts[2],
                            Status = parts[0],
                            Message = parts[1],
                            Country = parts[4],
                        };
                    }

                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
