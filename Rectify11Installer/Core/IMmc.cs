using System.Collections.Generic;
using System.Xml.Serialization;

namespace MMC
{
	[XmlRoot(ElementName = "Point")]
	public partial class Point
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "X")]
		public string X { get; set; }
		[XmlAttribute(AttributeName = "Y")]
		public string Y { get; set; }
	}

	[XmlRoot(ElementName = "Rectangle")]
	public class Rectangle
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "Top")]
		public string Top { get; set; }
		[XmlAttribute(AttributeName = "Bottom")]
		public string Bottom { get; set; }
		[XmlAttribute(AttributeName = "Left")]
		public string Left { get; set; }
		[XmlAttribute(AttributeName = "Right")]
		public string Right { get; set; }
	}

	[XmlRoot(ElementName = "WindowPlacement")]
	public class WindowPlacement
	{
		[XmlElement(ElementName = "Point")]
		public List<Point> Point { get; set; }
		[XmlElement(ElementName = "Rectangle")]
		public Rectangle Rectangle { get; set; }
		[XmlAttribute(AttributeName = "ShowCommand")]
		public string ShowCommand { get; set; }
		[XmlAttribute(AttributeName = "WPF_RESTORETOMAXIMIZED")]
		public string WPF_RESTORETOMAXIMIZED { get; set; }
	}

	[XmlRoot(ElementName = "FrameState")]
	public class FrameState
	{
		[XmlElement(ElementName = "WindowPlacement")]
		public WindowPlacement WindowPlacement { get; set; }
		[XmlAttribute(AttributeName = "ShowStatusBar")]
		public string ShowStatusBar { get; set; }
		[XmlAttribute(AttributeName = "LogicalReadOnly")]
		public string LogicalReadOnly { get; set; }
	}

	[XmlRoot(ElementName = "BookMark")]
	public class BookMark
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "NodeID")]
		public string NodeID { get; set; }
	}

	[XmlRoot(ElementName = "ViewOptions")]
	public class ViewOptions
	{
		[XmlAttribute(AttributeName = "ViewMode")]
		public string ViewMode { get; set; }
		[XmlAttribute(AttributeName = "DescriptionBarVisible")]
		public string DescriptionBarVisible { get; set; }
		[XmlAttribute(AttributeName = "DefaultColumn0Width")]
		public string DefaultColumn0Width { get; set; }
		[XmlAttribute(AttributeName = "DefaultColumn1Width")]
		public string DefaultColumn1Width { get; set; }
	}

	[XmlRoot(ElementName = "View")]
	public class View
	{
		[XmlElement(ElementName = "BookMark")]
		public List<BookMark> BookMark { get; set; }
		[XmlElement(ElementName = "WindowPlacement")]
		public WindowPlacement WindowPlacement { get; set; }
		[XmlElement(ElementName = "ViewOptions")]
		public ViewOptions ViewOptions { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string ID { get; set; }
		[XmlAttribute(AttributeName = "ScopePaneWidth")]
		public string ScopePaneWidth { get; set; }
		[XmlAttribute(AttributeName = "ActionsPaneWidth")]
		public string ActionsPaneWidth { get; set; }
	}

	[XmlRoot(ElementName = "Views")]
	public class Views
	{
		[XmlElement(ElementName = "View")]
		public View View { get; set; }
	}

	[XmlRoot(ElementName = "String")]
	public class String
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string ID { get; set; }
		[XmlAttribute(AttributeName = "Refs")]
		public string Refs { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Image")]
	public class Image
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "BinaryRefIndex")]
		public string BinaryRefIndex { get; set; }
	}

	[XmlRoot(ElementName = "Icon")]
	public class Icon
	{
		[XmlElement(ElementName = "Image")]
		public List<Image> Image { get; set; }
		[XmlAttribute(AttributeName = "Index")]
		public string Index { get; set; }
		[XmlAttribute(AttributeName = "File")]
		public string File { get; set; }
	}

	[XmlRoot(ElementName = "VisualAttributes")]
	public class VisualAttributes
	{
		[XmlElement(ElementName = "String")]
		public String String { get; set; }
		[XmlElement(ElementName = "Icon")]
		public Icon Icon { get; set; }
	}

	[XmlRoot(ElementName = "Favorite")]
	public class Favorite
	{
		[XmlElement(ElementName = "String")]
		public String String { get; set; }
		[XmlElement(ElementName = "Favorites")]
		public string Favorites { get; set; }
		[XmlAttribute(AttributeName = "TYPE")]
		public string TYPE { get; set; }
	}

	[XmlRoot(ElementName = "Favorites")]
	public class Favorites
	{
		[XmlElement(ElementName = "Favorite")]
		public Favorite Favorite { get; set; }
	}

	[XmlRoot(ElementName = "Snapin")]
	public class Snapin
	{
		[XmlElement(ElementName = "Extensions")]
		public string Extensions { get; set; }
		[XmlAttribute(AttributeName = "CLSID")]
		public string CLSID { get; set; }
		[XmlAttribute(AttributeName = "AllExtensionsEnabled")]
		public string AllExtensionsEnabled { get; set; }
	}

	[XmlRoot(ElementName = "SnapinCache")]
	public class SnapinCache
	{
		[XmlElement(ElementName = "Snapin")]
		public List<Snapin> Snapin { get; set; }
	}

	[XmlRoot(ElementName = "BinaryData")]
	public class BinaryData
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "BinaryRefIndex")]
		public string BinaryRefIndex { get; set; }
	}

	[XmlRoot(ElementName = "Bitmaps")]
	public class Bitmaps
	{
		[XmlElement(ElementName = "BinaryData")]
		public List<BinaryData> BinaryData { get; set; }
	}

	[XmlRoot(ElementName = "GUID")]
	public class GUID
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Stream")]
	public class Stream
	{
		[XmlAttribute(AttributeName = "BinaryRefIndex")]
		public string BinaryRefIndex { get; set; }
	}

	[XmlRoot(ElementName = "ComponentData")]
	public class ComponentData
	{
		[XmlElement(ElementName = "GUID")]
		public GUID GUID { get; set; }
		[XmlElement(ElementName = "Stream")]
		public Stream Stream { get; set; }
	}

	[XmlRoot(ElementName = "ComponentDatas")]
	public class ComponentDatas
	{
		[XmlElement(ElementName = "ComponentData")]
		public ComponentData ComponentData { get; set; }
	}

	[XmlRoot(ElementName = "Component")]
	public class Component
	{
		[XmlElement(ElementName = "GUID")]
		public GUID GUID { get; set; }
		[XmlElement(ElementName = "Stream")]
		public Stream Stream { get; set; }
		[XmlAttribute(AttributeName = "ViewID")]
		public string ViewID { get; set; }
	}

	[XmlRoot(ElementName = "Components")]
	public class Components
	{
		[XmlElement(ElementName = "Component")]
		public Component Component { get; set; }
	}

	[XmlRoot(ElementName = "Node")]
	public class Node
	{
		[XmlElement(ElementName = "Nodes")]
		public string Nodes { get; set; }
		[XmlElement(ElementName = "String")]
		public String String { get; set; }
		[XmlElement(ElementName = "Bitmaps")]
		public Bitmaps Bitmaps { get; set; }
		[XmlElement(ElementName = "ComponentDatas")]
		public ComponentDatas ComponentDatas { get; set; }
		[XmlElement(ElementName = "Components")]
		public Components Components { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string ID { get; set; }
		[XmlAttribute(AttributeName = "ImageIdx")]
		public string ImageIdx { get; set; }
		[XmlAttribute(AttributeName = "CLSID")]
		public string CLSID { get; set; }
		[XmlAttribute(AttributeName = "Preload")]
		public string Preload { get; set; }
	}

	[XmlRoot(ElementName = "Nodes")]
	public class Nodes
	{
		[XmlElement(ElementName = "Node")]
		public Node Node { get; set; }
	}

	[XmlRoot(ElementName = "ScopeTree")]
	public class ScopeTree
	{
		[XmlElement(ElementName = "SnapinCache")]
		public SnapinCache SnapinCache { get; set; }
		[XmlElement(ElementName = "Nodes")]
		public Nodes Nodes { get; set; }
	}

	[XmlRoot(ElementName = "TargetView")]
	public class TargetView
	{
		[XmlAttribute(AttributeName = "ViewID")]
		public string ViewID { get; set; }
		[XmlAttribute(AttributeName = "NodeTypeGUID")]
		public string NodeTypeGUID { get; set; }
	}

	[XmlRoot(ElementName = "ViewSettings")]
	public class ViewSettings
	{
		[XmlElement(ElementName = "GUID")]
		public string GUID { get; set; }
		[XmlAttribute(AttributeName = "Flag_TaskPadID")]
		public string Flag_TaskPadID { get; set; }
		[XmlAttribute(AttributeName = "Age")]
		public string Age { get; set; }
	}

	[XmlRoot(ElementName = "ViewSettingsCache")]
	public class ViewSettingsCache
	{
		[XmlElement(ElementName = "TargetView")]
		public TargetView TargetView { get; set; }
		[XmlElement(ElementName = "ViewSettings")]
		public ViewSettings ViewSettings { get; set; }
	}

	[XmlRoot(ElementName = "IdentifierPool")]
	public class IdentifierPool
	{
		[XmlAttribute(AttributeName = "AbsoluteMin")]
		public string AbsoluteMin { get; set; }
		[XmlAttribute(AttributeName = "AbsoluteMax")]
		public string AbsoluteMax { get; set; }
		[XmlAttribute(AttributeName = "NextAvailable")]
		public string NextAvailable { get; set; }
	}

	[XmlRoot(ElementName = "Strings")]
	public class Strings
	{
		[XmlElement(ElementName = "String")]
		public List<String> String { get; set; }
	}

	[XmlRoot(ElementName = "StringTable")]
	public class StringTable
	{
		[XmlElement(ElementName = "GUID")]
		public string GUID { get; set; }
		[XmlElement(ElementName = "Strings")]
		public Strings Strings { get; set; }
	}

	[XmlRoot(ElementName = "StringTables")]
	public class StringTables
	{
		[XmlElement(ElementName = "IdentifierPool")]
		public IdentifierPool IdentifierPool { get; set; }
		[XmlElement(ElementName = "StringTable")]
		public StringTable StringTable { get; set; }
	}

	[XmlRoot(ElementName = "Binary")]
	public class Binary
	{
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "BinaryStorage")]
	public class BinaryStorage
	{
		[XmlElement(ElementName = "Binary")]
		public List<Binary> Binary { get; set; }
	}

	[XmlRoot(ElementName = "MMC_ConsoleFile")]
	public class MMC_ConsoleFile
	{
		[XmlElement(ElementName = "ConsoleFileID")]
		public string ConsoleFileID { get; set; }
		[XmlElement(ElementName = "FrameState")]
		public FrameState FrameState { get; set; }
		[XmlElement(ElementName = "Views")]
		public Views Views { get; set; }
		[XmlElement(ElementName = "VisualAttributes")]
		public VisualAttributes VisualAttributes { get; set; }
		[XmlElement(ElementName = "Favorites")]
		public Favorites Favorites { get; set; }
		[XmlElement(ElementName = "ScopeTree")]
		public ScopeTree ScopeTree { get; set; }
		[XmlElement(ElementName = "ConsoleTaskpads")]
		public string ConsoleTaskpads { get; set; }
		[XmlElement(ElementName = "ViewSettingsCache")]
		public ViewSettingsCache ViewSettingsCache { get; set; }
		[XmlElement(ElementName = "ColumnSettingsCache")]
		public string ColumnSettingsCache { get; set; }
		[XmlElement(ElementName = "StringTables")]
		public StringTables StringTables { get; set; }
		[XmlElement(ElementName = "BinaryStorage")]
		public BinaryStorage BinaryStorage { get; set; }
		[XmlAttribute(AttributeName = "ConsoleVersion")]
		public string ConsoleVersion { get; set; }
		[XmlAttribute(AttributeName = "ProgramMode")]
		public string ProgramMode { get; set; }
	}
}