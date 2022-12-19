using System.IO;
using System.Xml.Serialization;

namespace MMC
{
	class IMmcParser
	{
		#region Public Methods
		public static MMC_ConsoleFile GetAll(string file)
		{
			XmlSerializer ser = new(typeof(MMC_ConsoleFile));
			MMC_ConsoleFile patches;
			using (var reader = new StringReader(file))
			{
				patches = (MMC_ConsoleFile)ser.Deserialize(reader);
			}
			return patches;
		}
		#endregion
	}
}
