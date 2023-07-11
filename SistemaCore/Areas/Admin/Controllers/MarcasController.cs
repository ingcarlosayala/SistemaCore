using Microsoft.AspNetCore.Mvc;
using SistemaCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore.Models;
using SistemaCore.Utilidades;

namespace SistemaCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public MarcasController(IUnidadTrabajo unidadTrabajo)
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
        public async Task<IActionResult> Crear(Marca marca)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Marca.Agregar(marca);
                await unidadTrabajo.Guardar();
                TempData[DS.Exitosa] = "Marca Creada Exitoxamente";
                return RedirectToAction("Index");
            }

            TempData[DS.Error] = "Error al Crear la marca";

            return View(marca);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var marcaDb = await unidadTrabajo.Marca.Obtener(id);

            if (marcaDb == null)
            {
                return NotFound();
            }

            return View(marcaDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Marca marca)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Marca.Actualizar(marca);
                await unidadTrabajo.Guardar();
                TempData[DS.Exitosa] = "Marca Actualizada Exitoxamente";
                return RedirectToAction("Index");
            }

            TempData[DS.Error] = "Error al Actualizar la marca";

            return View(marca);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Json(new { data = await unidadTrabajo.Marca.ObtenerTodos() });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var marcaDb = await unidadTrabajo.Marca.Obtener(id);

            if (marcaDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            unidadTrabajo.Marca.Remover(marcaDb);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca eliminada exitosamente" });
        }

        #endregion
    }
}
