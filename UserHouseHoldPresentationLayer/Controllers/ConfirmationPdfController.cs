using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserHouseHoldPresentationLayer.Controllers
{
    public class ConfirmationPdfController : Controller
    {
        // GET: ConfirmationPdf
        public ActionResult Index()
        {

            WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
            ViewBag.list = service.PdfRelationMapperList();
            return View();
        }
        public ActionResult ConfiramationScreen()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {

            MemoryStream stream = new System.IO.MemoryStream();
            
                WcfService_CodeFirstApproach.Service1 service = new WcfService_CodeFirstApproach.Service1();
                List<Household> list = service.GetAllHouseHoldMembers().ToList();
                //StringReader sr = new StringReader(JsonConvert.SerializeObject());
                PdfPTable table = new PdfPTable(5);
                //foreach (var item in list)
                //{
                //    PdfPCell p = new PdfPCell()
                //    table.AddCell(item);
                //}
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
            //pdfDoc.AddSubject(sr.ToString());
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Add(table);

                var bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(list));
                var memStream = new MemoryStream(bytes);

             using (FileStream file = new FileStream(@"C:\file.pdf", FileMode.Create, FileAccess.Write))
            {
              memStream.WriteTo(file);
            }
            
            //pdfDoc.save(
              //  "Sample.pdf",
                //HttpContext.ApplicationInstance.Response);

            return File(memStream, "application/pdf", "Grid.pdf");
                
                
                //Document document = new Document();
                //PdfWriter.GetInstance(document, stream);
                //document.Open();

                //// This is how not to do it (but it works anyway):

                //// We create a list:
            

                //// We wrap this list in a phrase:     
                //Phrase phrase = new Phrase();
                //phrase.Add(list);
                //// We add this phrase to a cell
                //PdfPCell phraseCell = new PdfPCell();
                //phraseCell.AddElement(phrase);

                //// We add the cell to a table:
                //PdfPTable phraseTable = new PdfPTable(2);
                ////phraseTable.SetSpacingBefore(5);
                //phraseTable.AddCell("List wrapped in a phrase:");
                //phraseTable.AddCell(phraseCell);

                //// We wrap the phrase table in another table:
                //Phrase phraseTableWrapper = new Phrase();
                //phraseTableWrapper.Add(phraseTable);

                //// We add these nested tables to the document:
                //document.Add(new Paragraph("A list, wrapped in a phrase, wrapped in a cell, wrapped in a table, wrapped in a phrase:"));
                //document.Add(phraseTableWrapper);

                //// This is how to do it:

                //// We add the list directly to a cell:
                //PdfPCell cell = new PdfPCell();
                //cell.AddElement(list);

                //// We add the cell to the table:
                //PdfPTable table1 = new PdfPTable(2);
                //table1.AddCell("List placed directly into cell");
                //table1.AddCell(cell);

                //// We add the table to the document:
                //document.Add(new Paragraph("A list, wrapped in a cell, wrapped in a table:"));
                //document.Add(table);

                //// Avoid adding tables to phrase (but it works anyway):

                //Phrase tableWrapper = new Phrase();
                //tableWrapper.Add(table); 
                //document.Add(new Paragraph("A list, wrapped in a cell, wrapped in a table, wrapped in a phrase:"));
                //document.Add(tableWrapper);
                //document.Close();

                //return File(, "application/pdf", "Grid.pdf");
            
        }
    }
}