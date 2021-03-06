﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BooksWebApp.Attributes
{
	public class IsbnAttribute : ValidationAttribute
	{
		//is taken from here http://regexlib.com/REDetails.aspx?regexp_id=4917
		private static readonly Regex IsbnRegex = new Regex(@"((ISBN[-]?)?(1[03])*[ ]*(: ){0,1})*(([0-9X][- ]*){13}|([0-9X][- ]*){10})", RegexOptions.Compiled);

		public override bool IsValid(object value)
		{
			if (value == null) return true;

			var codeToValidate = value.ToString().ToUpper().Replace(" ", "");
			
			return IsbnRegex.IsMatch(codeToValidate) && TryValidate(codeToValidate);
		}

		private static bool TryValidate(string isbn)
		{
			bool result = false;

			if (!string.IsNullOrEmpty(isbn))
			{
				isbn = isbn.Replace("ISBN", "");
				if (isbn.Contains("-")) isbn = isbn.Replace("-", "");

				switch (isbn.Length)
				{
					case 10: result = IsValidIsbn10(isbn); break;
					case 13: result = IsValidIsbn13(isbn); break;
				}
			}

			return result;
		}

		//Is taken from http://www.fablecode.com/view?s=nE5epqhvLV
		private static bool IsValidIsbn10(string isbn10)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(isbn10))
			{
				if (isbn10.Contains("-")) isbn10 = isbn10.Replace("-", "");

				long j;

				// Length must be 10 and only the last character could be a char('X') or a numeric value,
				// otherwise it's not valid.
				if (isbn10.Length != 10 || !long.TryParse(isbn10.Substring(0, isbn10.Length - 1), out j))
					return false;

				char lastChar = isbn10[isbn10.Length - 1];

				// Using the alternative way of calculation
				int sum = 0;
				for (int i = 0; i < 9; i++)
					sum += int.Parse(isbn10[i].ToString()) * (i + 1);

				// Getting the remainder or the checkdigit
				int remainder = sum % 11;

				// If the last character is 'X', then we should check if the checkdigit is equal to 10
				if (lastChar == 'X')
				{
					result = (remainder == 10);
				}
				// Otherwise check if the lastChar is numeric
				// Note: I'm passing sum to the TryParse method to not create a new variable again
				else if (int.TryParse(lastChar.ToString(), out sum))
				{
					// lastChar is numeric, so let's compare it to remainder
					result = (remainder == int.Parse(lastChar.ToString()));
				}
			}

			return result;
		}


		/// <summary>
		/// Validates ISBN13 codes
		/// </summary>
		/// <param name="isbn13">code to validate</param>
		/// <returns>true, if valid</returns>
		private static bool IsValidIsbn13(string isbn13)
		{
			bool result = false;

			if (!string.IsNullOrEmpty(isbn13))
			{
				if (isbn13.Contains("-")) isbn13 = isbn13.Replace("-", "");

				// If the length is not 13 or if it contains any non numeric chars, return false
				long temp;
				if (isbn13.Length != 13 || !long.TryParse(isbn13, out temp)) return false;

				// Comment Source: Wikipedia
				// The calculation of an ISBN-13 check digit begins with the first
				// 12 digits of the thirteen-digit ISBN (thus excluding the check digit itself).
				// Each digit, from left to right, is alternately multiplied by 1 or 3,
				// then those products are summed modulo 10 to give a value ranging from 0 to 9.
				// Subtracted from 10, that leaves a result from 1 to 10. A zero (0) replaces a
				// ten (10), so, in all cases, a single check digit results.
				int sum = 0;
				for (int i = 0; i < 12; i++)
				{
					sum += int.Parse(isbn13[i].ToString()) * (i % 2 == 1 ? 3 : 1);
				}

				int remainder = sum % 10;
				int checkDigit = 10 - remainder;
				if (checkDigit == 10) checkDigit = 0;

				result = (checkDigit == int.Parse(isbn13[12].ToString()));
			}

			return result;
		}

	}

}