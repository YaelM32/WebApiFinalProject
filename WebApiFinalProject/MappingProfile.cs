using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.DTO;
using DataAccess.DBModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuthorDTO, Author>();
        CreateMap<Author, AuthorDTO>();
        CreateMap<BookDTOInt, Book>();
        CreateMap<Book, BookDTOInt>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<EditionDTO, Edition>();
        CreateMap<Edition, EditionDTO>();
        CreateMap<PermissionDTO, Permission>();
        CreateMap<Permission, PermissionDTO>();
        CreateMap<ShulDTO, Shul>();
        CreateMap<Shul, ShulDTO>();
        CreateMap<ShulDTO2, Shul>();
        CreateMap<Shul, ShulDTO2>();
        CreateMap<UserDTO, User>();
        CreateMap<User, UserDTO>();

    }
}