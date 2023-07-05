using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;

namespace FarmerApp.MapperProfiles
{
    public class InvestmentProfile : Profile
    {
        public InvestmentProfile()
        {
            CreateMap<InvestmentRequestModel, Investment>()
                .ForMember(x => x.Date, opts => opts.MapFrom(y => DateTime.Now));

            CreateMap<InvestmentUpdateRequestModel, Investment>();
        }
    }
}
