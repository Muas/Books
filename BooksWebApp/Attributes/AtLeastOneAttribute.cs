using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BooksWebApp.Attributes
{
	public class AtLeastOneAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			var list = value as IList;
			return list != null && list.Count > 0;
		}
	}
}