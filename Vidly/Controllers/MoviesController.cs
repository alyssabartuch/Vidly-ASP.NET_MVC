using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        // ActionResult is the base class for all action results in ASP.NET MVC
        public ActionResult Random()
        {
            // Aplied to the advisory we would want to call to the DB using the argument passed in to the action (article ID)
            // and return that to the view for parsing out there
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return Content("hello world");
            //return HttpNotFound();
            //return new EmptyResult();

            // we pass the action and then the controller; sometimes we need to pass args to the target option we use an anon object 
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });

        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);

            // http://localhost:49275/Movies/edit?id=2
            //http://localhost:49275/Movies/edit/1
        }

        // movies/
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }


        // attribute routing example
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month); 
        }        
    }
}