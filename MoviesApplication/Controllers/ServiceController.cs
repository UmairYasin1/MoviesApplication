using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoviesApplication.Controllers
{
    [TestClass]
    public class ServiceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region genre 

        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult indexGenre()
        {

            try
            {
                var genre = db.Genre.ToList();

                if (genre != null)
                {
                    return Json(genre);
                }
                else
                {
                    return Json(createJson("0", "No Data Found"));
                }
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }

        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult showGenre(int? Id)
        {

            try
            {
                if (Id == null)
                {
                    return Json(createJson("0", "No Data Found"));
                }
                Genre genre = db.Genre.Find(Id);
                if (genre == null)
                {
                    return Json(createJson("0", "No Data Found"));
                }

                return Json(genre);
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }


        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public IHttpActionResult insertGenre([FromBody] dynamic genre)
        {

            try
            {
                if (genre != null)
                {
                    Genre genreAdd = new Genre();
                    genreAdd.Name = genre.Name;
                    genreAdd.Description = genre.Description;
                    db.Genre.Add(genreAdd);
                    db.SaveChanges();

                    return Json(createJson("1", "Insert successfully"));
                }
                else
                {
                    return Json(createJson("0", "Error! Kindly try again"));
                }
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }


        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public IHttpActionResult deleteGenre(int? id)
        {

            try
            {
                if (id != null)
                {
                    Genre genre = db.Genre.Find(id);
                    db.Genre.Remove(genre);
                    db.SaveChanges();

                    return Json(createJson("1", "Delete successfully"));
                }
                else
                {
                    return Json(createJson("0", "Error! Kindly try again"));
                }
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }



        #endregion


        #region movies


        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult indexMovie()
        {

            try
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

                if (movies != null)
                {
                    return Json(movies);
                }
                else
                {
                    return Json(createJson("0", "No Data Found"));
                }
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }


        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult showMovie(int? Id)
        {

            try
            {
                if (Id == null)
                {
                    return Json(createJson("0", "No Data Found"));
                }
                Movie movie = db.Movie.Find(Id);
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
                    return Json(createJson("0", "No Data Found"));
                }

                return Json(model);
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }


        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public IHttpActionResult insertMovie([FromBody] dynamic movie)
        {

            try
            {
                if (movie != null)
                {
                    Movie movieAdd = new Movie();
                    movieAdd.Name = movie.Name;
                    movieAdd.Description = movie.Description;
                    movieAdd.ReleaseDate = movie.ReleaseDate;
                    movieAdd.GenreId = movie.GenreId;
                    movieAdd.Duration = movie.Duration;
                    movieAdd.Rating = movie.Rating;

                    db.Movie.Add(movieAdd);
                    db.SaveChanges();

                    return Json(createJson("1", "Insert successfully"));
                }
                else
                {
                    return Json(createJson("0", "Error! Kindly try again"));
                }
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }

        [TestMethod]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public IHttpActionResult deleteMovie(int? id)
        {

            try
            {
                if (id != null)
                {
                    Movie movie = db.Movie.Find(id);
                    db.Movie.Remove(movie);
                    db.SaveChanges();

                    return Json(createJson("1", "Delete successfully"));
                }
                else
                {
                    return Json(createJson("0", "Error! Kindly try again"));
                }
            }
            catch (Exception ex)
            {
                return Json(createJson("0", ex.Message));
            }
        }


        #endregion


        public IHttpActionResult GetGenreList()
        {
            var genreList = db.Genre.ToList();

            if (genreList.Count > 0)
            {
                return Json(genreList);
            }
            else
            {
                System.Web.HttpContext.Current.Response.AppendHeader("Error-Header", "No Data Available");
                return Content((HttpStatusCode)1, "No Data Available");
            }
        }


        public List<ReturnResult> createJson(string code, string msg)
        {

            ReturnResult result = new ReturnResult(code, msg);

            List<ReturnResult> list = new List<ReturnResult>();
            list.Add(result);

            return list;
        }

        public List<ReturnResult> createJson(string code, string msg, dynamic content)
        {

            ReturnResult results = new ReturnResult(code, msg, content);


            List<ReturnResult> list = new List<ReturnResult>();
            list.Add(results);


            return list;
        }

        public class ReturnResult
        {
            public string statuscode;
            public string message;
            public dynamic content;
            //   public dynamic result;

            public ReturnResult(string code, string msg)
            {
                this.statuscode = code;
                this.message = msg;

            }
            public ReturnResult(string code, string msg, dynamic content)
            {
                this.statuscode = code;
                this.message = msg;
                this.content = content;
                //   this.result = result;
            }
        }
    }
}
