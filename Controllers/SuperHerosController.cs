using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Superheros.Data;
using Superheros.Models;

namespace Superheros.Controllers
{
    public class SuperHerosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SuperHerosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: SuperHeros
        public ActionResult Index()
        {
            List<Models.Hero> SuperRos = _context.Superheros.ToList();
            return View(SuperRos);
        }

        // GET: SuperHeros/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var hero = _context.Superheros.FirstOrDefault(m => m.SuperheroID == id);

            if(hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        // GET: SuperHeros/Create
        public ActionResult Create()
        {
            Hero hero = new Hero();
            return View(hero);
        }

        // POST: SuperHeros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("SuperheroID,Name,AlterEgo,PrimaryAbility,SecondaryAbility,CatchPhrase")]Hero hero)
        {
            try
            {
                _context.Superheros.Add(hero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(hero);
            }
        }

        // GET: SuperHeros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = _context.Superheros.Find(id);
            if (hero == null)
            {
                return NotFound();
            }
            return View(hero);
        }

        // POST: SuperHeros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("SuperheroID,Name,AlterEgo,PrimaryAbility,SecondaryAbility,CatchPhrase")] Hero hero)
        {
            if (id != hero.SuperheroID)
            {
                return NotFound();
            }
            _context.Update(hero); 
            _context.SaveChangesAsync();
            return View(hero);
        }

        // GET: SuperHeros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hero = _context.Superheros.FirstOrDefault(m => m.SuperheroID == id);
            if (hero == null)
            {
                return NotFound();
            }
            return View(hero);
        }

        // POST: SuperHeros/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var hero = _context.Superheros.Find(id);
                _context.Superheros.Remove(hero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}