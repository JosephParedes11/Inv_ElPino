using GestionContratos.Datos;
using GestionContratos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContratos.Controllers
{
    public class ContratosController : Controller
    {
        ContratosDatos _contratosDatos = new ContratosDatos();
        ClientesDatos _clienteDatos = new ClientesDatos();
        EmpresasDatos _empresasDatos = new EmpresasDatos();
        LotesDatos _lotesDatos = new LotesDatos();
        UrbanizacionesDatos _urbanizacionesDatos = new UrbanizacionesDatos();

        private const int TamanoPagina = 10; // Número de contratos por página

        // GET: ContratosController
        public async Task<IActionResult> Index(int pagina = 1)
        {
            var contratos = await _contratosDatos.GetContratosPaginados(pagina, TamanoPagina);

            var modelo = new PaginacionViewModel<ContratoModel>
            {
                PaginaActual = pagina,
                TamanoPagina = TamanoPagina,
                TotalItems = await _contratosDatos.GetTotalContratos(),
                Items = contratos
            };

            return View(modelo);
        }

        // GET: ContratosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContratosController/Create
        public async Task<IActionResult> Create()
        {
            var clientes = await _clienteDatos.GetAllClientes();
            ViewBag.Clientes = clientes.Select(c => new { c.ClienteID, NombreCompleto = c.Nombres + " " + c.Apellidos }).ToList();
            var empresas = await _empresasDatos.GetAllEmpresas();
            ViewBag.Empresas = empresas.Select(e => new { e.EmpresaID, RucRazonSocial = e.RUC + " - " + e.RazonSocial }).ToList();
            var urbanizaciones = await _urbanizacionesDatos.GetAllUrbanizaciones();
            ViewBag.Urbanizaciones = urbanizaciones.Select(u => new {u.UrbanizacionID, u.Nombre}).ToList();
            var lotes = await _lotesDatos.GetAllLotes();
            ViewBag.Lotes = lotes.Select(l => new {l.LoteID, MontoTotal = l.PrecioPorMetro * l.MetrosCuadrados, Descripcion = "Mz." + l.Manzana + " Letra " +l.Letra }).ToList();
            return View();
        }

        // POST: ContratosController/Create
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Create(ContratoModel model)
        {
            try
            {
                model.Fecha = DateTime.Today;
                var respuesta = await _contratosDatos.CreateContrato(model);

                if (respuesta != 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
                /*return RedirectToAction(nameof(Index));*/
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al actualizar el contrato: " + ex.Message;
                return View();
            }
        }

        // GET: ContratosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contrato = await _contratosDatos.GetContratoById(id);

            var clientes = await _clienteDatos.GetAllClientes();
            ViewBag.Clientes = clientes.Select(c => new { c.ClienteID, NombreCompleto = c.Nombres + " " + c.Apellidos }).ToList();
            var empresas = await _empresasDatos.GetAllEmpresas();
            ViewBag.Empresas = empresas.Select(e => new { e.EmpresaID, RucRazonSocial = e.RUC + " - " + e.RazonSocial }).ToList();
            var urbanizaciones = await _urbanizacionesDatos.GetAllUrbanizaciones();
            ViewBag.Urbanizaciones = urbanizaciones.Select(u => new { u.UrbanizacionID, u.Nombre }).ToList();
            var lotes = await _lotesDatos.GetAllLotes();
            ViewBag.Lotes = lotes.Select(l => new { l.LoteID, MontoTotal = l.PrecioPorMetro * l.MetrosCuadrados, Descripcion = "Mz." + l.Manzana + " Letra " + l.Letra }).ToList();

            return View(contrato);
        }

        // POST: ContratosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContratoModel oContrato)
        {
            var clientes = await _clienteDatos.GetAllClientes();
            ViewBag.Clientes = clientes.Select(c => new { c.ClienteID, NombreCompleto = c.Nombres + " " + c.Apellidos }).ToList();
            var empresas = await _empresasDatos.GetAllEmpresas();
            ViewBag.Empresas = empresas.Select(e => new { e.EmpresaID, RucRazonSocial = e.RUC + " - " + e.RazonSocial }).ToList();
            var urbanizaciones = await _urbanizacionesDatos.GetAllUrbanizaciones();
            ViewBag.Urbanizaciones = urbanizaciones.Select(u => new { u.UrbanizacionID, u.Nombre }).ToList();
            var lotes = await _lotesDatos.GetAllLotes();
            ViewBag.Lotes = lotes.Select(l => new { l.LoteID, MontoTotal = l.PrecioPorMetro * l.MetrosCuadrados, Descripcion = "Mz." + l.Manzana + " Letra " + l.Letra }).ToList();

            if (id != oContrato.ContratoID)
            {
                TempData["ErrorMessage"] = "Invalid ContratoID.";
                return View();
            }
            var newContrato = new ContratoModel();
           
            try
            {
                bool contrato = await _contratosDatos.UpdateContrato(oContrato);
                    
                if (contrato)
                {
                    newContrato = await _contratosDatos.GetContratoById(id);
                }else
                {
                    return BadRequest();
                }
                
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al actualizar el contrato: " + ex.Message;
                return View();
            }

            return View(newContrato);
        }

        // GET: ContratosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                var contrato = await _contratosDatos.GetContratoById(id);
                bool successDelete = false;
                if (contrato != null)
                {
                    successDelete = await _contratosDatos.DeleteContrato(id);

                    if (successDelete)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }else
                {
                    TempData["ErrorMessage"] = "Ocurrió un error al eliminar el contrato: " + id;
                    return RedirectToAction(nameof(Index));
                }
            } 
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar el contrato: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
