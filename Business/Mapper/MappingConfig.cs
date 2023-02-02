using AutoMapper;
using ProjectASP.Data.Entities;
using ProjectASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectASP.Business.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Train, TrainDTO>().ReverseMap();
            CreateMap<Train, TrainCreateDTO>().ReverseMap();
            CreateMap<Train, TrainCreateDTO>().ReverseMap();

        }
    }
}
