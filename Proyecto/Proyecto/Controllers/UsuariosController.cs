using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Context;
using Proyecto.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;


namespace Proyecto.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MiContext _context;

        public UsuariosController(MiContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
              return _context.Usuarios != null ? 
                          View(await _context.Usuarios.ToListAsync()) :
                          Problem("Entity set 'MiContext.Usuarios'  is null.");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,NombreCompleto,Password,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,NombreCompleto,Password,Rol")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'MiContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        //*
        public IActionResult DescargarPDF()
        {

            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        //var rutaImagen = Path.Combine(_context.WebRootPath, "./CRUD/wwwroot/img/logo.jpg");
                        //byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);
                        row.ConstantItem(140).Height(60).Placeholder();
                        //row.ConstantItem(150).Image(imageData);

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("DIARIO AMANECER").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("INFORMACION EN GENERAL").FontSize(9);
                           

                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text("COD - 21312312312");

                            col.Item().Background("#257272").Border(1)
                            .BorderColor("#257272").AlignCenter();
                         


                        });
                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("REPORTE DE USUARIOS").Underline().Bold();

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Nombre: ").SemiBold().FontSize(10);
                                txt.Span("DIARIO AMANCER").FontSize(10);
                            });

                           /* col2.Item().Text(txt =>
                            {
                                txt.Span("CI: ").SemiBold().FontSize(10);
                                txt.Span("7897897").FontSize(10);
                            });/*/

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Direccion URL: ").SemiBold().FontSize(10);
                                txt.Span("https://localhost:7019/Usuarios").FontSize(10);
                            });
                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(async tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(2).Text("ID").FontColor("#fff"); 
                                
                          

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Email").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Password").FontColor("#fff");

                                header.Cell().Background("#257272")
                                .Padding(2).Text("Nombre Completo").FontColor("#fff");


                                header.Cell().Background("#257272")
                               .Padding(2).Text("Rol").FontColor("#fff");


                            });

                            /*
                             *  foreach (var item in Enumerable.Range(1, 45))
                             {
                                 var cantidad = Placeholders.Random.Next(1, 10);
                                 var precio = Placeholders.Random.Next(5, 15);
                                 var total = cantidad * precio;

                                 tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                 .Padding(2).Text(Placeholders.Label()).FontSize(10);

                                 tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                          .Padding(2).Text(cantidad.ToString()).FontSize(10);

                                 tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                          .Padding(2).Text($"S/. {precio}").FontSize(10);

                                 tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                          .Padding(2).AlignRight().Text($"S/. {total}").FontSize(10);
                             }
                             */

                            //*aqui
                            ;
                            //can = _context.Usuarios.Count();

                            foreach (var item in _context.Usuarios)
                            {
                                var id = item.Id.ToString();
                                var email = item.Email.ToString();
                                var pas = item.Password.ToString();
                           
                                var nomcom = item.NombreCompleto.ToString();
                                var ro = item.Rol.ToString();




                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(id.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(email.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(pas.ToString()).FontSize(10);


                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(nomcom.ToString()).FontSize(10);


                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(ro.ToString()).FontSize(10);


                            }

                            //*fin aqui


                        });

                        

                        if (1 == 1)
                            col1.Item().Background(Colors.Grey.Lighten3).Padding(10)
                            .Column(column =>
                            {
                                column.Item().Text("Comentarios").FontSize(14);
                                column.Item().Text(Placeholders.LoremIpsum());
                                column.Spacing(5);
                            });

                        col1.Spacing(10);
                    });


                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            Stream stream = new MemoryStream(data);
            return File(stream, "application/pdf", "reporteUsuario.pdf");

        }

        //*
    }
}
