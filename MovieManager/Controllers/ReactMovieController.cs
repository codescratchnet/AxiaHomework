using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace MovieManager.Controllers
{
    public class ReactMovieController : Controller
    {
        // GET: ReactMovie
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReactMovie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReactMovie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReactMovie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReactMovie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReactMovie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReactMovie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReactMovie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}