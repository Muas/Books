using System.Collections.Generic;

namespace BooksWebApp.Models
{
	public class ResultsModel
	{
		public IEnumerable<BookEntity> Books { get; set; }
		public string SortOrder { get; set; }
		public string SortTitle { get; set; }
	}
}