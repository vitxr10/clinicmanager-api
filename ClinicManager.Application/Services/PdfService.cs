using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Services
{
    public class PdfService : IPdfService
    {
        public byte[] CreatePdf(string header, string content, string footer)
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                var attachmentBytes =
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header()
                            .AlignCenter()
                            .Text(header)
                            .SemiBold()
                            .FontSize(35);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Item()
                                    .Text(content);
                            });

                        page.Footer()
                        .AlignCenter()
                        .Text(footer);
                    });
                })
                .GeneratePdf();

                return attachmentBytes;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
