using AutoMapper;
using Backend.DTOs.Reportes;
using Backend.DTOs.Reportes.Apertura;
using Backend.Utilidades.PageEventHelpers;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Repositorios.Reportes.ReportesApertura
{
    
    public class RepositorioReportesApertura : IRepositorioReportesApertura
    {
        private readonly ILogger<RepositorioReportesApertura> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        //variables para los reportes
        Document doc = new Document();
        MemoryStream ms = new MemoryStream();
        int _maxColumn = 7;
        Document _document;
        PdfPTable _pdfPTable = new PdfPTable(7);
        PdfPCell _pdfCell;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();


        public RepositorioReportesApertura(ILogger<RepositorioReportesApertura> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<ReportesGenerales>> obteneractivosapertura(int apertura, int tiporeporte, int usuario)
        {
            try
            {
                ReportesGenerales resultado = new ReportesGenerales();
                ////consultas generales para el reporte
                //var DatosApertura = await context.AperturaInventarios
                //.Where(x => x.Codigo == apertura)
                //.FirstOrDefaultAsync();

                //var DatosSucursal = await context.Sucursales
                //.Where(x => x.Codigo == DatosApertura.Sucursal)
                //.FirstOrDefaultAsync();

                //var lista = await context.DetalleAperturaActivos
                //.Where(x => x.Apertura == apertura)
                //.ToListAsync();

                //var datosUsuario = await context.Usuarios
                //.Where(x => x.Codigo == usuario)
                //.FirstOrDefaultAsync();

                //var listaProductos = await (from detalle in context.DetalleAperturas
                //                            join productos in context.Productos on detalle.Producto equals productos.Codigo
                //                            join marca in context.Marcas on productos.Marca equals marca.Codigo
                //                            join unidadmedida in context.UnidadMedida on productos.UnidadMedida equals unidadmedida.Codigo
                //                            where detalle.Apertura == apertura
                //                            orderby productos.Codigo descending
                //                            select new ListaProductosAperturaDTO
                //                            {
                //                                Producto = Convert.ToInt32(detalle.Producto),
                //                                CodigoProducto = productos.CodBusqueda,
                //                                NombreProducto = productos.Descripcion,
                //                                Marca = marca.Descripcion,
                //                                UnidadMedida = unidadmedida.Descripcion,
                //                                Apertura = detalle.Apertura,
                //                                Cantidad = detalle.Cantidad,
                //                                PrecioCosto = detalle.PrecioCosto,
                //                                PrecioVenta = detalle.PrecioVenta,
                //                                TotalCosto = detalle.Cantidad * detalle.PrecioCosto,
                //                                TotalVenta = detalle.Cantidad * detalle.PrecioVenta
                //                            }).ToListAsync();

                //var listaCreditosCompras = await (from p in context.AperturaCreditoCompras
                //               join pro in context.Proveedores on p.Proveedor equals pro.Codigo
                //               join entidad in context.Empresas on p.Entidad equals entidad.Codigo
                //               join sucursal in context.Sucursales on p.Sucursal equals sucursal.Codigo
                //               where p.Apertura == apertura

                //               select new
                //               {
                //                   Codigo = p.Codigo,
                //                   Proveedor = pro.Nombre,
                //                   Motivo = p.Motivo,
                //                   Monto = p.Monto,
                //                   Entidad = entidad.Nombre,
                //                   Sucursal = sucursal.Descripcion

                //               }).Select(concepto => new ListaAperturaCreditosGeneral
                //               {
                //                   Codigo = concepto.Codigo,
                //                   Persona = concepto.Proveedor,
                //                   Motivo = concepto.Motivo,
                //                   Monto = concepto.Monto,
                //                   Entidad = concepto.Entidad,
                //                   Sucursal = concepto.Sucursal

                //               }).ToListAsync();

                //var listaCreditosVentas = await (from p in context.AperturaCreditoVenta
                //                                 join cliente in context.Clientes on p.Cliente equals cliente.Codigo
                //                                 join entidad in context.Empresas on p.Entidad equals entidad.Codigo
                //                                 join sucursal in context.Sucursales on p.Sucursal equals sucursal.Codigo

                //                                 select new
                //                                 {
                //                                     Codigo = p.Codigo,
                //                                     Proveedor = cliente.Nombre,
                //                                     Motivo = p.Motivo,
                //                                     Monto = p.Monto,
                //                                     Entidad = entidad.Nombre,
                //                                     Sucursal = sucursal.Descripcion

                //                                 }).Select(concepto => new ListaAperturaCreditosGeneral
                //                                 {
                //                                     Codigo = concepto.Codigo,
                //                                     Persona = concepto.Proveedor,
                //                                     Motivo = concepto.Motivo,
                //                                     Monto = concepto.Monto,
                //                                     Entidad = concepto.Entidad,
                //                                     Sucursal = concepto.Sucursal

                //                                 }).ToListAsync();

                //if (tiporeporte == 1)
                //{
                   
                //    doc.SetPageSize(new Rectangle(935.43f, 612.28f));
                //    doc.SetMargins(28.34f, 28.34f, 28.34f, 44.51f);

                //    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                //    doc.AddAuthor("DataSistemas");
                //    doc.AddTitle("Reporte Apertura " + DatosSucursal.Descripcion);

                //    var pe = new PageEventHelper();
                //    pe.Title = "Reporte Apertura";
                //    pe.Usuario = datosUsuario.NombreUsuario;
                //    pe.Nombre = DatosSucursal.Descripcion;
                //    pe.Direccion = DatosSucursal.Direccion;
                //    pe.Subtitulo = "Productos";
                //    writer.PageEvent = pe;

                //    //tipos de letra

                //    BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                //    iTextSharp.text.Font titulo = new iTextSharp.text.Font(_titulo, 10f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

                //    BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                //    iTextSharp.text.Font subtitulo = new iTextSharp.text.Font(_subtitulo, 10f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

                //    BaseFont _parrafo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                //    iTextSharp.text.Font parrafo = new iTextSharp.text.Font(_parrafo, 10f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

                //    BaseFont _igss = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                //    iTextSharp.text.Font igss = new iTextSharp.text.Font(_igss, 10f, iTextSharp.text.Font.NORMAL, new BaseColor(255, 255, 255));

                //    iTextSharp.text.Font negrita = new iTextSharp.text.Font(_igss, 10f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

                //    //se abre el documento
                //    doc.Open();

                //    //se generan las columnas 
                //    var tbl = new PdfPTable(new float[] { 10f, 20f, 10f, 10f, 10f, 10f, 10f, 10f, 10f }) { WidthPercentage = 100f };

                //    var c1 = new PdfPCell(new Phrase("Código", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    var c2 = new PdfPCell(new Phrase("Producto", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    var c3 = new PdfPCell(new Phrase("Marca", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };

                //    var c4 = new PdfPCell(new Phrase("Unidad de Medida", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    var c5 = new PdfPCell(new Phrase("Cantidad", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    var c6 = new PdfPCell(new Phrase("Precio Costo", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    var c7 = new PdfPCell(new Phrase("Total Costo", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    var c8 = new PdfPCell(new Phrase("Precio Venta", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    var c9 = new PdfPCell(new Phrase("Total Venta", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);
                //    tbl.AddCell(c4);
                //    tbl.AddCell(c5);
                //    tbl.AddCell(c6);
                //    tbl.AddCell(c7);
                //    tbl.AddCell(c8);
                //    tbl.AddCell(c9);

                //    tbl.HeaderRows = 1;

                //    decimal sumatoriaCosto = 0;
                //    decimal sumatoriaVenta = 0;

                //    foreach (var item in listaProductos)
                //    {
                //        c1.Phrase = new Phrase(item.CodigoProducto, parrafo);
                //        c2.Phrase = new Phrase(item.NombreProducto.ToString(), parrafo);
                //        c3.Phrase = new Phrase(item.Marca.ToString(), parrafo);
                //        c4.Phrase = new Phrase(item.UnidadMedida.ToString(), parrafo);
                //        c5.Phrase = new Phrase(item.Cantidad.ToString(), parrafo);
                //        c6.Phrase = new Phrase("Q" + item.PrecioCosto.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //        c7.Phrase = new Phrase("Q" + (item.PrecioCosto * item.Cantidad).ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //        c8.Phrase = new Phrase("Q" + item.PrecioVenta.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //        c9.Phrase = new Phrase("Q" + (item.PrecioVenta * item.Cantidad).ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);


                //        tbl.AddCell(c1);
                //        tbl.AddCell(c2);
                //        tbl.AddCell(c3);
                //        tbl.AddCell(c4);
                //        tbl.AddCell(c5);
                //        tbl.AddCell(c6);
                //        tbl.AddCell(c7);
                //        tbl.AddCell(c8);
                //        tbl.AddCell(c9);

                //        sumatoriaCosto = sumatoriaCosto + (item.PrecioCosto * item.Cantidad);
                //        sumatoriaVenta = sumatoriaVenta + (item.PrecioVenta * item.Cantidad);

                //    }

                //    c1.Phrase = new Phrase("Sumatoria: ", parrafo);
                //    c2.Phrase = new Phrase("", parrafo);
                //    c3.Phrase = new Phrase("", parrafo);
                //    c4.Phrase = new Phrase("", parrafo);
                //    c5.Phrase = new Phrase("", parrafo);
                //    c6.Phrase = new Phrase("", parrafo);
                //    c7.Phrase = new Phrase("Q" + sumatoriaCosto.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //    c8.Phrase = new Phrase("", parrafo);
                //    c9.Phrase = new Phrase("Q" + sumatoriaVenta.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);

                //    PdfPCell[] celdas = { c1, c2, c3, c4, c5, c6, c8 };

                //    foreach (var celda in celdas)
                //    {
                //        if (string.IsNullOrEmpty(celda.Phrase.Content))
                //            celda.Border = 0;
                //        if (celda.Phrase.Content.Contains("Sumatoria"))
                //            celda.Border = 0;
                //    }

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);
                //    tbl.AddCell(c4);
                //    tbl.AddCell(c5);
                //    tbl.AddCell(c6);
                //    tbl.AddCell(c7);
                //    tbl.AddCell(c8);
                //    tbl.AddCell(c9);

                //    doc.Add(tbl);

                //    //hasta aca seria lo del reporte general luego saltarse doc.close() ahora se agregaran nuevas paginas y nuevos titulos 


                //    pe.Subtitulo = "Activos";

                //    doc.NewPage();

                //    tbl = new PdfPTable(new float[] { 10f, 10f, 10f, 20f, 10f, 10f, 10f, 10f, 10f }) { WidthPercentage = 100f };

                //    var _Ordenada = lista.OrderBy(x => x.Codigo.ToString().PadLeft(10, '0')).ToList();
                //    //titulo
                //    c1 = new PdfPCell(new Phrase("Fecha Compra", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c2 = new PdfPCell(new Phrase("No. de Factura", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c3 = new PdfPCell(new Phrase("Serie de Factura", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };

                //    c4 = new PdfPCell(new Phrase("Descripción", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c5 = new PdfPCell(new Phrase("Tasa Depreciación", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c6 = new PdfPCell(new Phrase("Valor Activo", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c7 = new PdfPCell(new Phrase("Valor Inicial", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c8 = new PdfPCell(new Phrase("Vida Útil", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c9 = new PdfPCell(new Phrase("Valor Libros", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);
                //    tbl.AddCell(c4);
                //    tbl.AddCell(c5);
                //    tbl.AddCell(c6);
                //    tbl.AddCell(c7);
                //    tbl.AddCell(c8);
                //    tbl.AddCell(c9);

                //    tbl.HeaderRows = 1;

                //    decimal sumatorioActivo = 0;
                //    decimal sumatorioInicial = 0;
                //    decimal sumatorioLibros = 0;

                //    foreach (var item in _Ordenada)
                //    {
                //        c1.Phrase = new Phrase(item.FechaCompra.ToShortDateString(), parrafo);
                //        c2.Phrase = new Phrase(item.NoFactura.ToString(), parrafo);
                //        c3.Phrase = new Phrase(item.SerieFactura.ToString(), parrafo);
                //        c4.Phrase = new Phrase(item.DescripcionBien.ToString(), parrafo);
                //        c5.Phrase = new Phrase(item.TasaDepreciacion.ToString(), parrafo);
                //        c6.Phrase = new Phrase("Q" + item.ValorActivo.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //        c7.Phrase = new Phrase("Q" + item.ValorInicial.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //        c8.Phrase = new Phrase(item.VidaUtil.ToString(), parrafo);
                //        c9.Phrase = new Phrase("Q" + item.ValorLibros.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);


                //        tbl.AddCell(c1);
                //        tbl.AddCell(c2);
                //        tbl.AddCell(c3);
                //        tbl.AddCell(c4);
                //        tbl.AddCell(c5);
                //        tbl.AddCell(c6);
                //        tbl.AddCell(c7);
                //        tbl.AddCell(c8);
                //        tbl.AddCell(c9);

                //        sumatorioActivo = sumatorioActivo + item.ValorActivo;
                //        sumatorioInicial = sumatorioInicial + item.ValorInicial;
                //        sumatorioLibros = sumatorioLibros + item.ValorLibros;

                //    }

                //    c1.Phrase = new Phrase("Sumatoria: ", parrafo);
                //    c2.Phrase = new Phrase("", parrafo);
                //    c3.Phrase = new Phrase("", parrafo);
                //    c4.Phrase = new Phrase("", parrafo);
                //    c5.Phrase = new Phrase("", parrafo);
                //    c6.Phrase = new Phrase("Q" + sumatorioActivo.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //    c7.Phrase = new Phrase("Q" + sumatorioInicial.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                //    c8.Phrase = new Phrase("", parrafo);
                //    c9.Phrase = new Phrase("Q" + sumatorioLibros.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);

                //    PdfPCell[] celdas2 = { c1, c2, c3, c4, c5, c8 };

                //    foreach (var celda in celdas2)
                //    {
                //        if (string.IsNullOrEmpty(celda.Phrase.Content))
                //            celda.Border = 0;
                //        if (celda.Phrase.Content.Contains("Sumatoria"))
                //            celda.Border = 0;
                //    }

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);
                //    tbl.AddCell(c4);
                //    tbl.AddCell(c5);
                //    tbl.AddCell(c6);
                //    tbl.AddCell(c7);
                //    tbl.AddCell(c8);
                //    tbl.AddCell(c9);

                //    doc.Add(tbl);

                //    pe.Subtitulo = "Créditos de Compras";

                //    doc.NewPage();

                //    tbl = new PdfPTable(new float[] { 33f, 34f, 33f }) { WidthPercentage = 100f };

                    
                //    //titulo
                //    c1 = new PdfPCell(new Phrase("Proveedor", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c2 = new PdfPCell(new Phrase("Motivo", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c3 = new PdfPCell(new Phrase("Monto", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);

                //    tbl.HeaderRows = 1;
                //    decimal sumatorioMontoCompra = 0;
                //    foreach (var item in listaCreditosCompras)
                //    {
                //        c1.Phrase = new Phrase(item.Persona, parrafo);
                //        c2.Phrase = new Phrase(item.Motivo, parrafo);
                //        c3.Phrase = new Phrase("Q" + item.Monto.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);

                //        tbl.AddCell(c1);
                //        tbl.AddCell(c2);
                //        tbl.AddCell(c3);

                //        sumatorioMontoCompra = sumatorioMontoCompra + item.Monto;

                //    }

                //    c1.Phrase = new Phrase("Sumatoria: ", parrafo);
                //    c2.Phrase = new Phrase("", parrafo);
                //    c3.Phrase = new Phrase("Q" + sumatorioMontoCompra.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);
                    

                //    PdfPCell[] celdas3 = { c1, c2 };

                //    foreach (var celda in celdas3)
                //    {
                //        if (string.IsNullOrEmpty(celda.Phrase.Content))
                //            celda.Border = 0;
                //        if (celda.Phrase.Content.Contains("Sumatoria"))
                //            celda.Border = 0;
                //    }

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);

                //    doc.Add(tbl);

                //    pe.Subtitulo = "Créditos de Ventas";

                //    doc.NewPage();

                //    tbl = new PdfPTable(new float[] { 33f, 34f, 33f }) { WidthPercentage = 100f };


                //    //titulo
                //    c1 = new PdfPCell(new Phrase("Cliente", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c2 = new PdfPCell(new Phrase("Motivo", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };
                //    c3 = new PdfPCell(new Phrase("Monto", negrita)) { Border = 0, BorderWidthBottom = 2f, BorderColor = new BaseColor(0, 0, 0), Padding = 5f };

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);

                //    tbl.HeaderRows = 1;
                //    decimal sumatoriaMontoVentas = 0;
                //    foreach (var item in listaCreditosVentas)
                //    {
                //        c1.Phrase = new Phrase(item.Persona, parrafo);
                //        c2.Phrase = new Phrase(item.Motivo, parrafo);
                //        c3.Phrase = new Phrase("Q" + item.Monto.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);

                //        tbl.AddCell(c1);
                //        tbl.AddCell(c2);
                //        tbl.AddCell(c3);

                //        sumatoriaMontoVentas = sumatoriaMontoVentas + item.Monto;
                //    }

                //    c1.Phrase = new Phrase("Sumatoria: ", parrafo);
                //    c2.Phrase = new Phrase("", parrafo);
                //    c3.Phrase = new Phrase("Q" + sumatoriaMontoVentas.ToString("N2", System.Globalization.CultureInfo.InvariantCulture), parrafo);


                //    PdfPCell[] celdas4 = { c1, c2 };

                //    foreach (var celda in celdas4)
                //    {
                //        if (string.IsNullOrEmpty(celda.Phrase.Content))
                //            celda.Border = 0;
                //        if (celda.Phrase.Content.Contains("Sumatoria"))
                //            celda.Border = 0;
                //    }

                //    tbl.AddCell(c1);
                //    tbl.AddCell(c2);
                //    tbl.AddCell(c3);

                //    doc.Add(tbl);

                //    doc.Close();

                //    writer.Close();

                //    byte[] byteArray = ms.ToArray();

                //    string base64String = Convert.ToBase64String(byteArray);

                    resultado.Validacion = 1;
                    resultado.Base64reporte = "";//base64String;

                    return resultado;
                //}
                //else
                //{
                //    var _Ordenada = lista.OrderBy(x => x.Codigo.ToString().PadLeft(10, '0')).ToList();
                //    using (var workbook = new XLWorkbook())
                //    {
                //        var worksheet = workbook.Worksheets.Add("Reporte de activos ");
                //        var currentRow = 1;
                //        //headers
                //        worksheet.Cell(currentRow, 1).Value = "Fecha Compra";
                //        worksheet.Cell(currentRow, 2).Value = "No Factura";
                //        worksheet.Cell(currentRow, 3).Value = "Serie Factura";
                //        worksheet.Cell(currentRow, 4).Value = "Descripción";
                //        worksheet.Cell(currentRow, 5).Value = "Tasa Depreciación";
                //        worksheet.Cell(currentRow, 6).Value = "Valor Activo";
                //        worksheet.Cell(currentRow, 7).Value = "Valor Inicial";
                //        worksheet.Cell(currentRow, 8).Value = "Vida Útil";
                //        worksheet.Cell(currentRow, 9).Value = "Valor Libros";

                //        foreach(var item in _Ordenada)
                //        {
                //            currentRow++;
                //            worksheet.Cell(currentRow, 1).Value = item.FechaCompra.ToShortDateString();
                //            worksheet.Cell(currentRow, 2).Value = item.NoFactura;
                //            worksheet.Cell(currentRow, 3).Value = item.SerieFactura;
                //            worksheet.Cell(currentRow, 4).Value = item.DescripcionBien;
                //            worksheet.Cell(currentRow, 5).Value = item.TasaDepreciacion;
                //            worksheet.Cell(currentRow, 6).Value = item.ValorActivo;
                //            worksheet.Cell(currentRow, 7).Value = item.ValorInicial;
                //            worksheet.Cell(currentRow, 8).Value = item.VidaUtil;
                //            worksheet.Cell(currentRow, 9).Value = item.ValorLibros;

                //        }

                //        using (var stream = new MemoryStream())
                //        {
                //            workbook.SaveAs(stream);
                //            byte[] byteArray = stream.ToArray();

                //            string base64String = Convert.ToBase64String(byteArray);
                //            resultado.Validacion = 1;
                //            resultado.Base64reporte = base64String;

                //            return resultado;

                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                if (ex.InnerException?.Message.Length == 0)
                {
                    return new ObjectResult(new { message = ex.Message });

                }
                else
                {
                    return new ObjectResult(new { message = ex.InnerException?.Message });
                }
            }
        }

    }
}
