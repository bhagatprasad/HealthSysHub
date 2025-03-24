using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HealthSysHub.Web.UI.Reports
{
    public class PdfLayoutService
    {
        public byte[] GeneratePdf(Action<IContainer> headerContent, 
            Action<IContainer> bodyContent,
            Action<IContainer> footerContent)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Add dynamic header
                    page.Header().Element(headerContent);

                    // Add body content with a border
                    page.Content().Element(body =>
                    {
                        body.Border(1).Padding(10).Column(column =>
                        {
                            bodyContent((IContainer)column); 
                        });
                    });

                    // Add dynamic footer
                    page.Footer().Element(footerContent);
                });
            });

            return document.GeneratePdf();
        }
    }
}
