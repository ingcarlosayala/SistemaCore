using Microsoft.AspNetCore.Mvc;
using SistemaCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore.Models;
using SistemaCore.Utilidades;

namespace SistemaCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public BodegasController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Bodega.Agregar(bodega);
                await unidadTrabajo.Guardar();
                TempData[DS.Exitosa] = "Bodega Creada Exitoxamente";
                return RedirectToAction("Index");
            }

            TempData[DS.Error] = "Error al Crear la bodega";

            return View(bodega);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var bodegaDb = await unidadTrabajo.Bodega.Obtener(id);

            if (bodegaDb == null)
            {
                return NotFound();
            }

            return View(bodegaDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Bodega.Actualizar(bodega);
                await unidadTrabajo.Guardar();
                TempData[DS.Exitosa] = "Bodega Actualizada Exitoxamente";
                return RedirectToAction("Index");
            }

            TempData[DS.Error] = "Error al Actualizar la bodega";

            return View(bodega);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Json(new { data = await unidadTrabajo.Bodega.ObtenerTodos() });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var bodegaDb = await unidadTrabajo.Bodega.Obtener(id);

            if (bodegaDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            unidadTrabajo.Bodega.Remover(bodegaDb);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Bodega eliminada exitosamente" });
        }

        #endregion
    }
}
