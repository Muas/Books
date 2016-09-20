using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using BooksWebApp.Attributes;

namespace BooksWebApp.Models
{

	public class BookModel
	{
		[Required]
		[MaxLength(30)]
		[Display(Name="Title")]
		public string Title { get; set; }

		[AtLeastOne]
		[Display(Name = "Authors")]
		public AuthorModel[] Authors { get; set; }

		[Required]
		[Range(1, 10000)]
		[Display(Name = "Pages")]
		public int Pages { get; set; }

		[MaxLength(30)]
		[Display(Name = "Publisher")]
		public string Publisher { get; set; }

		[YearRange(minYear: 1800, useCurrentForMax: true)]
		[Display(Name = "Publiction year")]
		public int PublicationYear { get; set; }

		[Isbn]
		[Display(Name = "ISBN")]
		public string Isbn { get; set; }

		[UIHint("ImageModel")]
		[Display(Name = "Image")]
		public ImageModel Image { get; set; }

		public class ImageModel
		{
			public HttpPostedFileBase Upload { get; set; }
			public string Url { get; set; }
		}

		public class AuthorModel
		{
			[Required]
			[MaxLength(20)]
			[Display(Name = "Fistname")]
			public string FirstName { get; set; }
			[Required]
			[MaxLength(20)]
			[Display(Name = "Lastname")]
			public string LastName { get; set; }
		}
	}
}