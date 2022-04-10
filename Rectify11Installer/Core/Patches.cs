using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Core
{
    public static class Patches
    {
        public static PatchDef[] GetAll()
        {
            return new PatchDef[]
            {
                new PatchDef(
                    "microsoft-windows-usercpl",
                    PackageArch.Amd64,
                    "usercpl.dll.mun",
                    @"C:\Windows\SystemResources\usercpl.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("addoverwrite","UserCPL_IconGroup1.ico","ICONGROUP,1,0")
                    }),
                  new PatchDef(
                    "microsoft-windows-bootux.deployment",
                    PackageArch.Amd64,
                    "bootux.dll",
                    @"C:\Windows\System32\bootux.dll",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("addoverwrite","BootUX_UiFile_100.ui","UIFILE,100,0"),
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","BootUX_Resources_Icons.res","ICONGROUP,")
                    }),
                   new PatchDef(
                    "microsoft-windows-themeui",
                    PackageArch.Amd64,
                    "themeui.dll.mun",
                    @"C:\Windows\SystemResources\themeui.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","ThemeUI_Icons.res","ICONGROUP,"),

                        new PatchInstruction("delete","","Image,"),
                        new PatchInstruction("addskip","ThemeUI_Images.res","Image,"),
                    }),
                    new PatchDef(
                    "microsoft-windows-themecpl",
                    PackageArch.Amd64,
                    "themecpl.dll.mun",
                    @"C:\Windows\SystemResources\themecpl.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","Themecpl_Icons.res","ICONGROUP,"),

                        new PatchInstruction("delete","","Image,"),
                        new PatchInstruction("addskip","Themecpl_Images.res","Image,"),
                    }),
                   new PatchDef(
                    "microsoft-windows-van",
                    PackageArch.Amd64,
                    "van.dll.mun",
                    @"C:\Windows\SystemResources\van.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","Van_Icons.res","ICONGROUP,"),
                    }),
                    new PatchDef(
                    "microsoft-windows-accessibilitycpl",
                    PackageArch.Amd64,
                    "accessibilitycpl.dll.mun",
                    @"C:\Windows\SystemResources\accessibilitycpl.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","accessibilitycpl_Icons.res","ICONGROUP,"),

                        new PatchInstruction("delete","","UIFILE,"),
                        new PatchInstruction("addskip","accessibilitycpl_uifile.res","UIFILE,"),
                    }),
                    new PatchDef(
                    "microsoft-windows-aclui",
                    PackageArch.Amd64,
                    "aclui.dll.mun",
                    @"C:\Windows\SystemResources\aclui.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","aclui_icons.res","ICONGROUP,"),
                    }),
                    new PatchDef(
                    "microsoft-windows-aclui",
                    PackageArch.Amd64,
                    "aclui.dll.mun",
                    @"C:\Windows\SystemResources\aclui.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","aclui_icons.res","ICONGROUP,"),
                    }),
                     new PatchDef(
                    "microsoft-windows-healthcentercpl",
                    PackageArch.Amd64,
                    "actioncentercpl.dll.mun",
                    @"C:\Windows\SystemResources\actioncentercpl.dll.mun",
                    new PatchInstruction[]
                    {
                        new PatchInstruction("delete","","ICONGROUP,"),
                        new PatchInstruction("addskip","ActioncenterCPL_icons.res","ICONGROUP,"),
                    }),
            };
        }
    }
}
