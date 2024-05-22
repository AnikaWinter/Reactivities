using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {//Automapper will look inside properties of Activity class
        //automapper matches the property names and maps from one to the other
            CreateMap<Activity, Activity>(); //from Activity to Activity
        }
    }
}