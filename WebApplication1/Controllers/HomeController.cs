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
        [Route("/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            var film = this.db.Films.Find(id);

            return View();
        }

        [HttpPost]
        [Route("/edit/{id}")]
        public IActionResult Edit(Film film)
        {
            if (ModelState.IsValid)
            {
                this.db.Films.Update(film);
                this.db.SaveChanges();

                return Redirect("/");
            }

            return View(film);

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
        public IActionResult Delete(Film film)
        {
            this.db.Films.Remove(film);
            this.db.SaveChanges();
            return Redirect("/");

        }
    }
}
