using AutoMapper;
using Microsoft.Extensions.Configuration;
using Redmap.Events.DTO;
using Redmap.Events.Model;
using System;

namespace Redmap.Events.Services.AutoMapperProfile
{
    /// <summary>
    ///Mapping Profile Class
    /// </summary>
    public class MappingProfile : Profile
    {
        private readonly string bucketName;
        private readonly string awsUrl;
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingProfile(IConfiguration configuration)
        {
            bucketName = configuration.GetValue<string>("S3ConfigInfo:BucketName");
            awsUrl = configuration.GetValue<string>("S3ConfigInfo:AWSUrl");


            // Add as many of these lines as you need to map your objects
            CreateMap<LogMessageDto, LogMessageModel>().ReverseMap();
            CreateMap<MasterCategoriesDto, MasterCategoriesModel>().ReverseMap();
            CreateMap<LogMessageModel, EventsDto>()
            .ForMember(x => x.Attachment, opt => opt.MapFrom(o => !string.IsNullOrEmpty(o.Attachment) ? awsUrl + bucketName + "/" + o.Attachment : o.Attachment));               
           

        }
    }
}
