using AutoMapper;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Web.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PostDTO,Post>().ReverseMap();
                config.CreateMap<CommentDTO,Comment>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
