using GestionContratos.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContratos.Controllers
{
    public class UrbanizacionesController : Controller
    {
        UrbanizacionesDatos _urbanizacionesDatos = new UrbanizacionesDatos();
        // GET: UrbanizacionesController
        public async Task<IActionResult> Index()
        {
            var olista = await _urbanizacionesDatos.GetAllUrbanizaciones();
            return View(olista);
        }

        // GET: UrbanizacionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UrbanizacionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrbanizacionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UrbanizacionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UrbanizacionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UrbanizacionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UrbanizacionesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
