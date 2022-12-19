using System.IO;
using System.Xml.Serialization;

namespace Rectify11Installer
{
	public class PatchesParser
	{
		#region Public Methods
		public static Patches GetAll()
		{
			XmlSerializer ser = new(typeof(Patches));
			Patches patches;
			using (var reader = new StringReader(Properties.Resources.rectify11xml))
			{
				patches = (Patches)ser.Deserialize(reader);
			}
			return patches;
		}
		#endregion
	}
}
