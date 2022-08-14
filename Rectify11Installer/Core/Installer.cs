using System.Windows.Forms;
using System.Xml.Linq;

namespace Rectify11Installer.Core
{
    public class Installer
    {
        public static void CreatePendingXml()
        {
            new XDocument(
                new XElement("PendingTransaction",
                new XAttribute("Identifier", "0b3b509abaa9d80118ba000074118811"),
                new XAttribute("Repair", "true"),
                new XAttribute("Version", "3.1"),
                    new XElement("Repaired"))
                ).Save(Application.StartupPath + @"\pending.xml");
        }
        public static void Install()
        {
            CreatePendingXml();
        }
    }
}
