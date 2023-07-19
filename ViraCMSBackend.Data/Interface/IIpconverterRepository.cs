namespace ViraCMSBackend.Data.Interface
{
    public interface IIpconverterRepository
    {
        string CountryNameToCountryCode(string countryname,string defaultLang);
    }
}
