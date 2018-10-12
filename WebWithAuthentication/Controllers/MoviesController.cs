using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWithAuthentication.Models;
using WebWithAuthentication.ViewModels;

namespace WebWithAuthentication.Controllers
{
    //[Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
//        [Authorize]
        public ActionResult Index()
        {
//            var movies = _context.Movies.Include(m => m.Genre).ToList();
//            return View(movies);

            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)] //this will override the global authorize filter
        public ActionResult MovieForm()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View(viewModel);
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewMovie = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewMovie);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var genres = _context.Genres.ToList();

                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = genres
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index","Movies");
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

    }
}