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
    public class NoticiasController : Controller
    {
        private readonly MiContext _context;

        public NoticiasController(MiContext context)
        {
            _context = context;
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
              return _context.Noticiass != null ? 
                          View(await _context.Noticiass.ToListAsync()) :
                          Problem("Entity set 'MiContext.Noticiass'  is null.");
        }

        // GET: Noticias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Noticiass == null)
            {
                return NotFound();
            }

            var noticias = await _context.Noticiass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticias == null)
            {
                return NotFound();
            }

            return View(noticias);
        }

        // GET: Noticias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContenidoNoticia,Dedicatoria,FechaPublicado,Imagen,Titulo")] Noticias noticias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(noticias);
        }

        // GET: Noticias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Noticiass == null)
            {
                return NotFound();
            }

            var noticias = await _context.Noticiass.FindAsync(id);
            if (noticias == null)
            {
                return NotFound();
            }
            return View(noticias);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContenidoNoticia,Dedicatoria,FechaPublicado,Imagen,Titulo")] Noticias noticias)
        {
            if (id != noticias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiasExists(noticias.Id))
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
            return View(noticias);
        }

        // GET: Noticias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Noticiass == null)
            {
                return NotFound();
            }

            var noticias = await _context.Noticiass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticias == null)
            {
                return NotFound();
            }

            return View(noticias);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Noticiass == null)
            {
                return Problem("Entity set 'MiContext.Noticiass'  is null.");
            }
            var noticias = await _context.Noticiass.FindAsync(id);
            if (noticias != null)
            {
                _context.Noticiass.Remove(noticias);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticiasExists(int id)
        {
          return (_context.Noticiass?.Any(e => e.Id == id)).GetValueOrDefault();
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
                            // col.Item().AlignCenter().Text("SIS 2420 'B' / 2-2023").FontSize(9);
                            // col.Item().AlignCenter().Text("Actualizacion - Tecnologica").FontSize(9);

                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text("COD - 21312312312");

                            col.Item().Background("#257272").Border(1)
                            .BorderColor("#257272").AlignCenter();
                            //.Text("MATRICULA ESTUDIANTIL").FontColor("#fff");

                            // col.Item().Border(1).BorderColor("#257272").
                            // AlignCenter().Text("MATRICULA - 234 - OR");

                        });
                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("DATOS DE LA NOTICIA").Underline().Bold();

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
                                txt.Span("https://localhost:7019/Noticias").FontSize(10);
                            });
                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(async tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(2).Text("ID").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Contenido noticia").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Dedicatoria").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Fecha publicado").FontColor("#fff");

                                header.Cell().Background("#257272")
                                .Padding(2).Text("Imagen").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Titulo").FontColor("#fff");
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

                            foreach (var item in _context.Noticiass)
                            {
                                var id = item.Id.ToString();
                                var con = item.ContenidoNoticia.ToString();
                                var ded = item.Dedicatoria.ToString();
                                var fecha = item.FechaPublicado.ToString();
                                var im = item.Imagen.ToString();
                                var titu = item.Titulo.ToString();




                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(id.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(con.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(ded.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(fecha.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(im.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(titu.ToString()).FontSize(10);


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
            return File(stream, "application/pdf", "reporteNoticia.pdf");

        }

        //*
    }
}
