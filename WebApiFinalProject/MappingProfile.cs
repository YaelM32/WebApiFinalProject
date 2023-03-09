using AutoMapper;
using BusinessLogic.Dto;
using DataAccess.DBModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuthorDto, Author>();
        CreateMap<Author, AuthorDto>();
        CreateMap<BookDTO2, Book>();
        CreateMap<Book, BookDTO2>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
        CreateMap<EditionDto, Edition>();
        CreateMap<Edition, EditionDto>();
        CreateMap<PermissionDto, Permission>();
        CreateMap<Permission, PermissionDto>();
        CreateMap<ShulDto, Shul>();
        CreateMap<Shul, ShulDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();

    }
}