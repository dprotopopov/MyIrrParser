using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using MyParser.Library;
using MyWebSimulator;

namespace MyIrrParser
{
    public class IrrPublicationLookup : WebSimulator, IWebPublicationLookup
    {
        public IrrPublicationLookup()
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"var xpath=""//a[contains(text(),'Показать телефон')]"";");
            sb.AppendLine(
                @"while(Simulator.get_DocumentCompleted()==0 && Simulator.Select(xpath).Count()==0) Simulator.Sleep(1000);");
            sb.AppendLine(@"Simulator.Click(xpath);");
            sb.AppendLine(@"return 0;");
            JavaScript = sb.ToString();
        }

        public string PublicationId { get; set; }

        public override ListViewItem ToListViewItem()
        {
            ListViewItem viewItem = base.ToListViewItem();
            Debug.Assert(viewItem != null, "viewItem != null");
            viewItem.SubItems.Add(PublicationId);
            return viewItem;
        }
    }
}