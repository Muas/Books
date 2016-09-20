using System;
using System.ComponentModel.DataAnnotations;

namespace BooksWebApp.Attributes
{
	public class YearRangeAttribute : ValidationAttribute
	{
		private int MinYear { get; set; }
		private int MaxYear { get; set; }

		public YearRangeAttribute(int minYear = 1, int maxYear = 9999, bool useCurrentForMin = false, bool useCurrentForMax = false)
		{
			var currentYear = DateTime.Now.Year;
			this.MinYear = useCurrentForMin ? currentYear : minYear;
			MaxYear = useCurrentForMax ? currentYear : maxYear;
			ErrorMessage = $"Value should be greater than {minYear} and less than {maxYear}";
		}

		public override bool IsValid(object value)
		{
			if (value == null) return true;

			int yearToValidate;
			if (int.TryParse(value.ToString(), out yearToValidate))
			{
				return yearToValidate >= MinYear && yearToValidate <= MaxYear;
			}
			throw new Exception();
		}
	}
}