using AutoMapper;
using BooksWebApp.Models;

namespace BooksWebApp.Profiles
{
	public class BooksProfile : Profile
	{
		public BooksProfile()
		{
			CreateMap<BookEntity, BookModel>()
			   .ForMember(dst => dst.Image, opt => opt.MapFrom(src => new BookModel.ImageModel { Url = src.Image }))
			   ;

			CreateMap<BookModel, BookEntity>()
				.ForMember(dst => dst.Id, opt => opt.Ignore())
				.ForMember(dst => dst.Image, opt => opt.Ignore())
				.ForMember(dst => dst.Isbn, opt => opt.MapFrom(src => $"ISBN-{src.Isbn.Replace("ISBN", "").Trim('-')}" ))
				;

            CreateMap<Author, BookModel.AuthorModel>()
                ;

            CreateMap<BookModel.AuthorModel, Author>()
                ;
        }
	}
}