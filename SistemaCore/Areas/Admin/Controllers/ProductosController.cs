using Microsoft.AspNetCore.Mvc;
using SistemaCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaCore.Models;
using SistemaCore.Models.ViewsModels;
using SistemaCore.Utilidades;

namespace SistemaCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductosController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            this.unidadTrabajo = unidadTrabajo;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public  IActionResult Crear()
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = unidadTrabajo.Categoria.CategoriaLista(),
                MarcaLista = unidadTrabajo.Marca.MarcaLista()
            };

            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = webHostEnvironment.WebRootPath;
                var archivo = HttpContext.Request.Form.Files;

                if (productoVM.Producto.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    string subida = Path.Combine(rutaPrincipal,@"imagenes\producto");
                    var extension = Path.GetExtension(archivo[0].FileName);

                    using (var fileStrems = new FileStream(Path.Combine(subida, nombreArchivo + extension), FileMode.Create))
                    {
                        archivo[0].CopyTo(fileStrems);
                    }

                    productoVM.Producto.ImagenUrl = @"\imagenes\producto\" + nombreArchivo + extension;

                    await unidadTrabajo.Producto.Agregar(productoVM.Producto);
                    await unidadTrabajo.Guardar();
                    TempData[DS.Exitosa] = "Producto Creado Exitoxamente";
                    return RedirectToAction("Index");
                }
            }

            productoVM.CategoriaLista = unidadTrabajo.Categoria.CategoriaLista();
            productoVM.MarcaLista = unidadTrabajo.Marca.MarcaLista();

            return View(productoVM);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = unidadTrabajo.Categoria.CategoriaLista(),
                MarcaLista = unidadTrabajo.Marca.MarcaLista()
            };

            productoVM.Producto = await unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());

            if (productoVM.Producto == null)
            {
                return NotFound();
            }

            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = webHostEnvironment.WebRootPath;
                var archivo = HttpContext.Request.Form.Files;

                var imagenDb = await unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == productoVM.Producto.Id);

                if (archivo.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    string subida = Path.Combine(rutaPrincipal,@"imagenes\producto");
                    var extension = Path.GetExtension(archivo[0].FileName);

                    var imagenRuta = Path.Combine(rutaPrincipal,imagenDb.ImagenUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(imagenRuta))
                    {
                        System.IO.File.Delete(imagenRuta);
                    }

                    using (var fileStrems = new FileStream(Path.Combine(subida, nombreArchivo + extension),FileMode.Create))
                    {
                        archivo[0].CopyTo(fileStrems);
                    }

                    productoVM.Producto.ImagenUrl = @"\imagenes\producto\" + nombreArchivo + extension;

                    await unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                    await unidadTrabajo.Guardar();
                    TempData[DS.Exitosa] = "Producto Actualizado Exitoxamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    productoVM.Producto.ImagenUrl = imagenDb.ImagenUrl;
                }

                await unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                await unidadTrabajo.Guardar();
                TempData[DS.Exitosa] = "Producto Actualizado Exitoxamente";
                return RedirectToAction("Index");
            }

            productoVM.CategoriaLista = unidadTrabajo.Categoria.CategoriaLista();
            productoVM.MarcaLista = unidadTrabajo.Marca.MarcaLista();

            return View(productoVM);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var categoriaLista = await unidadTrabajo.Producto.ObtenerTodos(incluirdPropiedad:"Categoria,Marca");
            return Json(new { data = categoriaLista });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int? id)
        {
            string rutaPrincincipal = webHostEnvironment.WebRootPath;
            var productoDb = await unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());

            if (productoDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar el producto" });
            }

            var imagenRutal = Path.Combine(rutaPrincincipal,productoDb.ImagenUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagenRutal))
            {
                System.IO.File.Delete(imagenRutal);
            }

            unidadTrabajo.Producto.Remover(productoDb);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Producto eliminado exitosamente" });
        }

        #endregion
    }
}
