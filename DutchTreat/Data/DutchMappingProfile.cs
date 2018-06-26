using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
  // AutoMapper needs a mapping profile in order to
  // know how to map from one type to another.
  public class DutchMappingProfile : Profile
  {
    public DutchMappingProfile()
    {
      // If property names don't match then 
      // clarifications are needed.
      CreateMap<Order, OrderViewModel>()
               .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
               .ReverseMap();
    }
  }
}
