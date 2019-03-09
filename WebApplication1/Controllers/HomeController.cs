using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly FilmContext db;

        public HomeController(FilmContext dbContext)
        {
            this.db = dbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var films = this.db.Films.ToList();
            return View(films);
        }

        [HttpGet]
        [Route("/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult Create(Film film)
        {
            if (ModelState.IsValid)
            {
                this.db.Films.Add(film);
                this.db.SaveChanges();
                return Redirect("/");
            }

            return View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            using ( db )
            {
                var film = db.Films.Find(id);
                if (film != null)
                {
                    return View(film);
                }
            }
            return Redirect("/");

        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int? id, Film filmModel)
        {
            if (!ModelState.IsValid)
            {
                return View(filmModel);
            }
            using ( db )
            {
                var taskFromDb = db.Films.Find(filmModel.Id);
                if (taskFromDb != null)
                {
                    taskFromDb.Name = filmModel.Name;
                    taskFromDb.Genre = filmModel.Genre;
                    taskFromDb.Director = filmModel.Director;
                    taskFromDb.Year = filmModel.Year;
                    db.SaveChanges();
                }
            }
            return Redirect("/");

        }

        [HttpGet]
        [Route("/delete/{id}")]
        public IActionResult Delete(int? id)
        {

            using (db)
            {
                var film = db.Films.Find(id);
                if (film == null)
                {
                    return Redirect("/");
                }
                return View(film);
            }
        }

        [HttpPost]
        [Route("/delete/{id}")]
        public IActionResult Delete(int? id, Film filmModel)
        {
            using (db)
            {
                var movie = db.Films.Find(id);
                if (movie == null)
                {
                    return Redirect("/");
                }
                db.Films.Remove(movie);
                db.SaveChanges();
                return Redirect("/");
            }


        }
    }
}
