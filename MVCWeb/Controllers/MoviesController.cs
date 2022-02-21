using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCWeb.Models;
using MVCWeb.Models.Interface;
using MVCWeb.Models.Repository;
using PagedList;

namespace MVCWeb.Controllers
{
    public class MoviesController : Controller
    {
        private IMoviesRepository moviesRepository;
        public MoviesController()
        {
            this.moviesRepository = new MoviesRepository();
        }

        public ActionResult Index(string movieGenre, string searchString, string sortOrder= "電影名稱", int? page=1)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var movies = this.moviesRepository.GetAll(movieGenre, searchString, sortOrder);
            var GenreLst = new List<string>();
            var GenreQry = this.moviesRepository.GenreLst();
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);                    

            return View(movies.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index"); 
            }
            else
            {
                var movies = this.moviesRepository.Get(x => x.ID == id.Value);
                return View(movies);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (movie != null && ModelState.IsValid)
            {
                this.moviesRepository.Create(movie);
                return RedirectToAction("Index");
            }
            else
            {
                return View(movie);
            }     
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var movies = this.moviesRepository.Get(x => x.ID == id.Value);
                return View(movies);
            }
        }

        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (movie != null && ModelState.IsValid)
            {
                this.moviesRepository.Update(movie);
                return View(movie);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var movies = this.moviesRepository.Get(x=>x.ID == id.Value);
                return View(movies);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var movies = this.moviesRepository.Get(x => x.ID == id);
                this.moviesRepository.Delete(movies);
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id });
            }
            return RedirectToAction("index");
        }
    }
}
