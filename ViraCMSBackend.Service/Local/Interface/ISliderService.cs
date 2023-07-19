using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.SliderDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface ISliderService
    {
        void AddSlider(Slider slider);
        List<ShowSliders> GetAllSliders(int languageId);
        List<BOShowSliders> BOGetAllSliders(int languageId);
        void UpdateSlider(Slider slider);
        Slider GetSliderById(int Id);
        string ShowSlideImage(int Id);
        List<ShowSliders> GetAllDefaultSliders();

        //List<Slider> GetSliderByAlias(string alias, int languageId);
        //List<ShowSliders> BOGetSliderByAlias(string alias, int languageId);
        //List<Slider> BOGetSliderDLByAlias(string alias, int languageId);
    }
}
