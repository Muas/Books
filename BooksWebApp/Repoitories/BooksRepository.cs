using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BooksWebApp.Models;
using System.Web;
using System.Net;

namespace BooksWebApp.Repoitories
{
	public class BooksRepository
	{
		private static ConcurrentDictionary<Guid, BookEntity> _books = new ConcurrentDictionary<Guid, BookEntity>();

		public BooksRepository()
		{
			_books.TryAdd(Guid.Parse("ba928975-9ff5-4a55-b881-6373441cd0eb"), new BookEntity()
			{
				Title = "Jack London: A Life",
				Authors = new List<Author>()
				{
					new Author() {FirstName = "Alex", LastName = "Kershaw"}
				},
				Id = Guid.Parse("ba928975-9ff5-4a55-b881-6373441cd0eb"),
				Isbn = "ISBN-031219904X",
				PublicationYear = 1999,
				Publisher = "St. Martins Griffin",
				Pages = 300,
				Image = "/uploads/jack-london.jpg"
			});

			_books.TryAdd(Guid.Parse("9b94df9e-554f-4ea3-8efd-a73617e0ad2b"), new BookEntity()
			{
				Title = "The terrifying tales",
				Authors = new List<Author>()
				{
					new Author() {FirstName = "Edgar Allan", LastName = "Poe"}
				},
				Id = Guid.Parse("9b94df9e-554f-4ea3-8efd-a73617e0ad2b"),
				Isbn = "ISBN-978-1537505992",
				PublicationYear = 2016,
				Publisher = "CreateSpace Independent Publishing Platform",
				Pages = 378,
				Image = "/uploads/51oflpU4F0L._AC_UL115_.jpg"
			});

			_books.TryAdd(Guid.Parse("b0ef9085-3f52-4b28-a858-a0683baedcf3"), new BookEntity()
			{
				Title = "Some book",
				Authors = new List<Author>()
				{
					new Author() {FirstName = "First", LastName = "Author"},
					new Author() {FirstName = "Second", LastName = "Author"},
					new Author() {FirstName = "Third", LastName = "Author"}
				},
				Id = Guid.Parse("b0ef9085-3f52-4b28-a858-a0683baedcf3"),
				Isbn = "ISBN-978-1-4028-9462-6",
				PublicationYear = 2013,
				Publisher = "CreateSpace Independent Publishing Platform",
				Pages = 378
			});
		}

		public IEnumerable<BookEntity> GetBooks(string title, string order)
		{
			if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(order))
			{
				return _books.Values;
			}
			switch (order)
			{
				case "asc":
				{
						if (title.Equals("Title"))
						{
							return _books.Values.OrderBy(x => x.Title);
						}
						else
						{
							return _books.Values.OrderBy(x => x.PublicationYear);
						}
					}
					break;
				case "desc":
					{
						if (title.Equals("Title"))
						{
							return _books.Values.OrderByDescending(x => x.Title);
						}
						else
						{
							return _books.Values.OrderByDescending(x => x.PublicationYear);
						}
					}
					break;
			}
			return _books.Values;
		}

		public bool Delete(Guid id)
		{
			BookEntity t;
			return _books.TryRemove(id, out t);
		}

		public void AddOrUpdate(BookEntity bookEntity)
		{
			_books.AddOrUpdate(bookEntity.Id, bookEntity, (key, oldValue) => bookEntity);
		}

		public BookEntity Get(Guid id)
		{
			BookEntity existingEntity;
			if (_books.TryGetValue(id, out existingEntity))
			{
				return existingEntity;
			}
			else
			{
				throw new KeyNotFoundException();
			}
		}
	}
}