using dockerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dockerApp.Controllers
{
    public class SongController : Controller
    {
        // POST: SongController/Create
        // Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Song song)
        {
            return View();
        }

        // GET: SongController
        // Read
        [HttpGet]
        public ActionResult Get()
        {
            return View();
        }

        // GET: SongController/Update
        // Update
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            return View();
        }


        // POST: SongController/Delete
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
