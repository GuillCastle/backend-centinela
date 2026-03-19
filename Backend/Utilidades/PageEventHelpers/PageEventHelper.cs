using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Backend.Utilidades.PageEventHelpers
{

    public class PageEventHelper : PdfPageEventHelper
    {
        PdfContentByte cb;
        // we will put the final number of pages in a template
        PdfTemplate template;
        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;
        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        private string _Title;
        private string _Usuario;
        private string _Nombre;
        private string _Fecha;
        private string _Direccion;
        private string _Subtitulo;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public string Subtitulo
        {
            get { return _Subtitulo; }
            set { _Subtitulo = value; }
        }


        private string _HeaderLeft;
        public string HeaderLeft
        {
            get { return _HeaderLeft; }
            set { _HeaderLeft = value; }
        }
        private string _HeaderRight;
        public string HeaderRight
        {
            get { return _HeaderRight; }
            set { _HeaderRight = value; }
        }
        private Font _HeaderFont;
        public Font HeaderFont
        {
            get { return _HeaderFont; }
            set { _HeaderFont = value; }
        }
        private Font _FooterFont;
        public Font FooterFont
        {
            get { return _FooterFont; }
            set { _FooterFont = value; }
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(document.PageSize.Width, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }


        public override void OnStartPage(PdfWriter writer, Document document)
        {

            base.OnStartPage(writer, document);

            Rectangle pageSize = document.PageSize;

            BaseFont fuente = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);

            iTextSharp.text.Font titulo = new iTextSharp.text.Font(fuente, 12f, iTextSharp.text.Font.ITALIC, BaseColor.Black);

            BaseFont _titulo2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            iTextSharp.text.Font titulo2 = new iTextSharp.text.Font(_titulo2, 15f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, true);
            iTextSharp.text.Font tituloResolucion = new iTextSharp.text.Font(_titulo, 10f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Path.Combine(Environment.CurrentDirectory, "wwwroot/Logo.jpg"));
            logo.ScaleAbsolute(75, 75);
            var tbl = new PdfPTable(new float[] { 15f, 85f }) { WidthPercentage = 100f };

            PdfPCell imagencell = new PdfPCell(logo);
            imagencell.Border = 0;
            imagencell.Rowspan = 5;
            imagencell.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl.AddCell(imagencell);



            tbl.AddCell(new PdfPCell(new Phrase(Title, titulo2)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
            //tbl.AddCell(new PdfPCell(new Phrase("", titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
            iTextSharp.text.pdf.draw.LineSeparator line = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.Black, Element.ALIGN_LEFT, 1);
            Paragraph p = new Paragraph();
            p.Add(new Chunk(line));

            PdfPCell cell = new PdfPCell(p);
            cell.Border = 0;
            tbl.AddCell(cell);
            tbl.AddCell(new PdfPCell(new Phrase("Sucursal: " + Nombre, titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
            tbl.AddCell(new PdfPCell(new Phrase("Dirección: " + Direccion, titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
            tbl.AddCell(new PdfPCell(new Phrase(Subtitulo, titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
            tbl.AddCell(new PdfPCell(new Phrase("", titulo)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

            document.Add(tbl);


        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            int pageN = writer.PageNumber;
            String text = "Página " + pageN + "/";
            float len = bf.GetWidthPoint(text, 8);
            Rectangle pageSize = document.PageSize;
            cb.SetRgbColorFill(100, 100, 100);

            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            cb.ShowText(text);
            cb.EndText();

            cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER,
                "Usuario: " + this.Usuario,
                  pageSize.Right / 2,
                pageSize.GetBottom(30), 0);
            cb.EndText();
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                "Fecha " + PrintTime.ToString(),
                pageSize.GetRight(40),
                pageSize.GetBottom(30), 0);
            cb.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            template.BeginText();
            template.SetFontAndSize(bf, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1));
            template.EndText();
        }

    }
}
