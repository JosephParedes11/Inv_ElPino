using GestionContratos.Datos;
using GestionContratos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContratos.Controllers
{
    public class LotesController : Controller
    {
        LotesDatos _lotesDatos = new LotesDatos();
        private const int TamanoPagina = 10; // Número de Lotes página

        // GET: LotesController
        public async Task<IActionResult> Index(int pagina = 1)
        {
            var contratos = await _lotesDatos.GetLotesPaginados(pagina, TamanoPagina);

            var modelo = new PaginacionViewModel<LoteModel>
            {
                PaginaActual = pagina,
                TamanoPagina = TamanoPagina,
                TotalItems = await _lotesDatos.GetTotalLotes(),
                Items = contratos
            };

            return View(modelo);

            /*var olista = await _lotesDatos.GetAllLotes();
            return View(olista);*/
        }

        // GET: LotesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LotesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LotesController/Create
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

        // GET: LotesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LotesController/Edit/5
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

        // GET: LotesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LotesController/Delete/5
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
