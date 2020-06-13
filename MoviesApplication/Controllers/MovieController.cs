using MoviesApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MoviesApplication.Controllers
{
    public class MovieController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Movie
        public ActionResult Index()
        {
            var movies = db.Movie
                .Select(b => new MovieViewModel
                {
                    MovieId = b.MovieId,
                    Name = b.Name,
                    Description = b.Description,
                    ReleaseDate = b.ReleaseDate,
                    GenreId = b.GenreId,
                    GenreName = db.Genre.Where(x => x.GenreId.Equals(b.GenreId)).Select(x => x.Name).FirstOrDefault(),
                    Duration = b.Duration,
                    Rating = b.Rating
                }).ToList();
            return View(movies);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            MovieViewModel model = new MovieViewModel();
            model.MovieId = movie.MovieId;
            model.Name = movie.Name;
            model.Description = movie.Description;
            model.ReleaseDate = movie.ReleaseDate;
            model.GenreId = movie.GenreId;
            model.GenreName = db.Genre.Where(x => x.GenreId.Equals(movie.GenreId)).Select(x => x.Name).FirstOrDefault();
            model.Duration = movie.Duration;
            model.Rating = movie.Rating;

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult Create()
        {
            GetGenreList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,Name,Description,ReleaseDate,GenreId,Duration,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movie.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }


        public ActionResult Edit(int? id)
        {
            GetGenreList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Name,Description,ReleaseDate,GenreId,Duration,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);

            MovieViewModel model = new MovieViewModel();
            model.MovieId = movie.MovieId;
            model.Name = movie.Name;
            model.Description = movie.Description;
            model.ReleaseDate = movie.ReleaseDate;
            model.GenreId = movie.GenreId;
            model.GenreName = db.Genre.Where(x => x.GenreId.Equals(movie.GenreId)).Select(x => x.Name).FirstOrDefault();
            model.Duration = movie.Duration;
            model.Rating = movie.Rating;

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public void GetGenreList()
        {
            List<Genre> listGenre = db.Genre.ToList();
            ViewBag.GenreList = new SelectList(listGenre, "GenreId", "Name");
        }
    }
}