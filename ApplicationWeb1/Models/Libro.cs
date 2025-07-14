

using System.ComponentModel.DataAnnotations;
using System.Reflection;

using System.ComponentModel;

using System.Web.Mvc;

namespace ApplicationWeb1.Models
{
    public class Libro
    {
        // LIBRO
        public int id { get; set; }

        [Required] // REQUISITO OBLIGATORIO AL RELLENAR
        public string titulo { get; set; }

        [Required] // REQUISITO OBLIGATORIO AL RELLENAR
        [Range(1800, 2025, ErrorMessage = "El Año de Publicación debe estar entre los años 1800 y 2025.")] // RANGO DE AÑOS + MENSAJE DE ERROR
        public int anioPublicacion { get; set; }
        public Autor autor { get; set; }

        //IMAGENES

        public string urlImagen { get; set; }

        // variable AUX 
        public int autorId { get; set; }
    }
}
