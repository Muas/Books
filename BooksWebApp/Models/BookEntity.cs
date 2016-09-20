using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksWebApp.Models
{
	public class BookEntity
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public IEnumerable<Author> Authors { get; set; }
		public int Pages { get; set; }
		public string Publisher { get; set; }
		public int PublicationYear { get; set; }
		public string Isbn { get; set; }
		public string Image { get; set; }
	}
	public class Author
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}