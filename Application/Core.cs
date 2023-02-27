using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain;

namespace Application
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }

        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
    }

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Location, Location>();
            CreateMap<Engineer, Engineer>();
            //CreateMap<Activity, ActivityDto>()
            //    .ForMember(d => d.HostUserName, o => o.MapFrom(s => s.Attendees
            //        .FirstOrDefault(x => x.IsHost).AppUser.UserName));

            //CreateMap<ActivityAttendee, AttendeeDto>()
            //    .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
            //    .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
            //    .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
            //    .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));

            //CreateMap<AppUser, Profiles.Profile>()
            //    .ForMember(d => d.Image, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

            //CreateMap<Comment, CommentDto>()
            //    .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
            //    .ForMember(d => d.UserName, o => o.MapFrom(s => s.Author.UserName))
            //    .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
