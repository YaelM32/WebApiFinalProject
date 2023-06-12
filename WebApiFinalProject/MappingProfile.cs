using AutoMapper;
using BusinessLogic.Dto;
using DataAccess.DBModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuthorDto, Author>();
        CreateMap<Author, AuthorDto>();
        CreateMap<BookDTOWithImage, Book>();
        CreateMap<Book, BookDTOWithImage>();
        CreateMap<BookDTOInt, Book>();
        CreateMap<Book, BookDTOInt>();
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