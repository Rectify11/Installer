// This is used to simplify the creation of Rectify11.xml to avoid unnessesary steps

List<Info> dllsToSearch = new List<Info>();

foreach(var file in Directory.GetFiles(Directory.GetCurrentDirectory() + "/stuff"))
{
    var name = Path.GetFileName(file);

    var dll = name.Substring(0, name.IndexOf('_'));

    string type = "??";

    if (!File.Exists(@"C:\Windows\System32\" + dll+".exe"))
    {
        if (!File.Exists(@"C:\Windows\System32\" + dll+".dll"))
        {
            Console.WriteLine("Warning: Cannot find " + dll + ".dll");
        }
        else
        {
            type = "dll";
        }
    }
    else
    {
        type = "exe";
    }

    if (type!= "??")
    {
        dllsToSearch.Add(new Info() { DllName = dll + "." + type, ResFile=file, DllPath=@"C:\Windows\System32\"+dll+"."+type });
    }
}

foreach(var dir in Directory.GetDirectories(@"C:\Windows\WinSxS\"))
{
    var dirName = Path.GetDirectoryName(dir+"/").Substring(18);
    if (dirName.StartsWith("amd64_"))
    {
        foreach (var file in Directory.GetFiles(dir))
        {
            var fs = Path.GetFileName(file);
            foreach (var item in dllsToSearch)
            {
                if (fs == item.DllName)
                {
                    var properPackageName = dirName.Substring(6);
                    properPackageName = properPackageName.Substring(0, properPackageName.IndexOf('_'));

                    Console.WriteLine($"	<Patch Package=\"" + properPackageName + "\" HardlinkTarget=\"" +item.DllPath+ "\" DisableOnSafeMode=\"false\" Arch=\"amd64\">");
                    Console.WriteLine($"		<Commands>");
                    Console.WriteLine($"			<Command action=\"delete\" mask=\"ICONGROUP, \"></Command>");
                    Console.WriteLine($"			<Command action=\"addskip\" resource=\"{Path.GetFileName(item.ResFile)}\" mask=\"ICONGROUP, \"></Command>");
                    Console.WriteLine($"		</Commands>");
                    Console.WriteLine($"	</Patch>");
                    dllsToSearch.Remove(item);
                    break;
                }
            }
        }
    }
}

Console.WriteLine("cannot find "+dllsToSearch.Count + " dlls/exes in winsxs");

class Info
{
    public string DllName;
    public string ResFile;
    public bool SxsFound;
    public string DllPath;
}