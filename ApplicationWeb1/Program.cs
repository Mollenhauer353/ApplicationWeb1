var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

 // RUTA DEFAULT
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libro}/{action=Index}/{id?}");

app.Run();


/*  NOTAS
 
* LO QUE FALTA POR HACER / ARREGLAR *
 
-- Ejercicio MVC 1 - Catálogo de Libros y Autores
++ completo ++

-- Ejercicio MVC 2 - Catálogo de Libros y Autores
++ completo ++

-- Ejercicio MVC 3 - Catálogo de Libros y Autores
++ completo ++

-- Ejercicio MVC 4 - Ejercicio MVC - Catálogo de Libros y Autores
++ completo ++

-- FIN DEL DOCUMENTO --
*/