using ApplicationWeb1.Models;
using Microsoft.AspNetCore.Mvc;


namespace ApplicationWeb1.Controllers
{
    public class LibroController : Controller
    {

        public static List<Libro> libros = new List<Libro>
        {
            new Libro { id = 1, titulo = "1984", anioPublicacion = 1949, autor = new Autor { id = 1, nombre = "George Orwell" }, autorId = 1, urlImagen = "https://upload.wikimedia.org/wikipedia/commons/5/51/1984_first_edition_cover.jpg" },
            new Libro { id = 2, titulo = "Fahrenheit 451", anioPublicacion = 1953, autor = new Autor { id = 2, nombre = "Ray Bradbury" }, autorId = 2, urlImagen = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg" },
            new Libro { id = 3, titulo = "Rebelión en la granja", anioPublicacion = 1945, autor = new Autor { id = 1, nombre = "George Orwell" }, autorId = 1, urlImagen = "https://upload.wikimedia.org/wikipedia/commons/f/fb/Animal_Farm_-_1st_edition.jpg" },
            new Libro { id = 4, titulo = "Desakta2", anioPublicacion = 2025, autor = new Autor { id = 4, nombre = "Seba" }, autorId = 4, urlImagen = "https://www.tuentrada.com/images/673b6137ea240.webp" }
        };

        public static List<Libro> ObtenerLibros()
        {
            return libros;
        }
        public List<Autor> ObtenerAutores()
        {
            return new List<Autor>
            {
                new Autor { id = 1, nombre = "George Orwell" },
                new Autor { id = 2, nombre = "Ray Bradbury" },
                new Autor { id = 4, nombre = "Seba" },
            };
        }

        public IActionResult Index()
        {
            return View(ObtenerLibros());
        }

        // CREACION DEL GET 
        [HttpGet]
        public IActionResult Crear()
        {
            // metodo que devuelve lista de autores
            ViewBag.Autores = ObtenerAutores(); 
            return View();
        }

        // CREACION DEL POST
        [HttpPost]
       public IActionResult Crear(Libro libro)
        {
            ModelState.Remove("autor");
            ModelState.Remove("urlImagen");
            if (!ModelState.IsValid)
            {
                ViewBag.Autores = ObtenerAutores();
                return View(libro);
            }

            var autorseleccionado = ObtenerAutores().FirstOrDefault(a => a.id == libro.autorId);
            if(autorseleccionado == null)
            {
                ModelState.AddModelError("autorId", "Autor NO Valido");
                ViewBag.Autores = ObtenerAutores();
                return View(libro);
            }

            libro.autor = autorseleccionado;
            libro.id = ObtenerLibros().Any() ? ObtenerLibros().Max(l => l.id) + 1 : 1;
            ObtenerLibros().Add(libro);

            TempData["Mensaje"] = $"Libro '{libro.titulo}' Creado correctamente";


            // REDIRECCION AL NUEVO LIBRO 
            return RedirectToAction("Detalle", new { id = libro.id });
        }

        public IActionResult Detalle(int id)
        { 

            var libroEncontrado = ObtenerLibros().FirstOrDefault(l => l.id == id);
            if (libroEncontrado == null)
            {
                return RedirectToAction("Index");

            }
            return View(libroEncontrado);
        }

        // EDITAR //
        [HttpGet]
        public IActionResult Editar(int id)
        {

            var libro = libros.FirstOrDefault(l => l.id == id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewBag.Autores = ObtenerAutores();
            return View(libro);
        }

        // POST: VALIDAR Y GUARDAR CAMBIOS //
        [HttpPost]
        public IActionResult Editar(Libro libro)
        {
            ModelState.Remove("autor");
            ModelState.Remove("urlImagen");
            if (!ModelState.IsValid)
            {
                ViewBag.Autores = ObtenerAutores();
                return View(libro);
            }
            var libroExistente = libros.FirstOrDefault(l => l.id == libro.id);
            if (libroExistente == null)
            {
                return NotFound();
            }
            // ACTUALIZAR
            libroExistente.titulo = libro.titulo;
            libroExistente.anioPublicacion = libro.anioPublicacion;
            libroExistente.urlImagen = libro.urlImagen;

            var autorSeleccionado = ObtenerAutores().FirstOrDefault(a => a.id == libro.autorId);
            if (autorSeleccionado != null)
            {
                libroExistente.autor = autorSeleccionado;
            }
            libroExistente.urlImagen = libro.urlImagen;
            TempData["Mensaje"] = "Libro editado correctamente";

            // REDIRECCION AL NUEVO LIBRO
            return RedirectToAction("Detalle", new { id = libro.id });
            //return RedirectToAction("Index", new { id = libro.id });


        }

    }
}
