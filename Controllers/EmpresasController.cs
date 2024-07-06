using GestionContratos.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContratos.Controllers
{
    public class EmpresasController : Controller
    {
        EmpresasDatos _empresasDatos = new EmpresasDatos();

        // GET: EmpresasController
        public async Task<IActionResult> Index()
        {
            var olista = await _empresasDatos.GetAllEmpresas();
            return View(olista);
        }

        // GET: EmpresasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmpresasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpresasController/Create
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

        // GET: EmpresasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmpresasController/Edit/5
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

        // GET: EmpresasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmpresasController/Delete/5
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
