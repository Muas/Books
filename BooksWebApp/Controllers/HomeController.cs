using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksWebApp.Models;
using BooksWebApp.Repoitories;

namespace BooksWebApp.Controllers
{
	public class HomeController : Controller
	{
		private static BooksRepository _booksRepository = new BooksRepository();
		
		public ActionResult Index()
		{
			return View();
		}


		public ActionResult Books(string title, string order)
		{
			var bookEntities = _booksRepository.GetBooks(title, order);
			return PartialView(model: new ResultsModel() {Books = bookEntities, SortOrder = order, SortTitle = title});
		}

		[HttpGet]
		public ActionResult Edit(Guid? id)
		{
			return Edit(id, null);
		}

		[HttpPost]
		public ActionResult Edit(Guid? id, BookModel model)
		{
			if (model == null || ModelState.IsValid == false)
			{
				if (model == null && id != null)
				{
					model = AutoMapper.Mapper.Map<BookModel>(_booksRepository.Get(id.Value));
				}
				return View(model);
			}
			else
			{
				var entity = id != null ? _booksRepository.Get(id.Value) : new BookEntity();

				AutoMapper.Mapper.Map(model, entity);

				if (model.Image.Upload != null && model.Image.Upload.ContentLength > 0)
				{
					var uploadDir = "~/uploads";
					var filename = Guid.NewGuid().ToString() + ".jpg";
					var mapPath = Server.MapPath(uploadDir);
					Directory.CreateDirectory(mapPath);
					var imagePath = Path.Combine(mapPath, filename);
					var imageUrl = Path.Combine(uploadDir, filename);
					model.Image.Upload.SaveAs(imagePath);
					entity.Image = imageUrl;
				}

				_booksRepository.AddOrUpdate(entity);

				return View("_ClosePopup");
			}
		}

		[HttpPost]
		public ActionResult Delete(Guid id)
		{
			_booksRepository.Delete(id);
			return Json(true);
		}
	}
}