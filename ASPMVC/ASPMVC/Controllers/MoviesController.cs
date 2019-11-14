using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using ASPMVC.Models;
using Newtonsoft.Json;
using TMDbLib;
using TMDbLib.Client;
using EntityState = System.Data.Entity.EntityState;

namespace ASPMVC.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        public ActionResult SendMovies(IQueryable<Movie> movies, TMDbLib.Objects.Movies.Movie movie, TMDbClient client)
        {
            int id;

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            
            for (id = 0; id <= 30; id++)
            {
                try
                {
                    movie = client.GetMovieAsync(id).Result;
                }

                catch (NullReferenceException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                if (movie != null)
                {
                    if (movie.Adult == false)
                    {
                        Movie objMovie = movies.FirstOrDefault(x => x.RemoteId.Equals(movie.Id));
                        if(objMovie != null) //achei no banco local
                        {
                            objMovie.Original_title = movie.OriginalTitle;
                            objMovie.Release_date = movie.ReleaseDate;
                            objMovie.Original_Language = new CultureInfo(movie.OriginalLanguage).DisplayName;
                            objMovie.Description = movie.Overview;
                            objMovie.Budget = movie.Budget;
                            objMovie.Vote_average = movie.VoteAverage;
                        }
                        else //não achei esse movie no banco local
                        {
                            objMovie = new Movie
                            {
                                RemoteId = movie.Id,
                                Original_title = movie.OriginalTitle,
                                Release_date = movie.ReleaseDate,
                                Description = movie.Overview,
                                Original_Language = new CultureInfo(movie.OriginalLanguage).DisplayName,
                                Budget = movie.Budget,
                                Vote_average = movie.VoteAverage
                            };
                            
                            //depois de atribuir tudo
                            db.Movies.Add(objMovie);
                            db.SaveChanges();

                        }
                    }
                }
            }
            return View(movies);
        }

        // GET: Movies
        public ActionResult Index(string movieGenre, string movieName, string searchString, int? page)
        {

            TMDbClient client = new TMDbClient("f5d4250fed4b807034758b556e333c90");

            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Original_Language
                           select d.Original_Language;

            TMDbLib.Objects.Movies.Movie movie = null;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;

            SendMovies(movies.OrderBy(x => x.Original_title), movie, client);

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Original_title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Original_Language == movieGenre);
            }

            

            return View(movies.OrderBy(x => x.Original_title));
        }


        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Original_title,Release_date,Original_Language,Description,Budget,Vote_average")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Original_title,Release_date,Original_Language,Description,Budget,Vote_average")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(FormCollection fcNotUsed, int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
    }
}
