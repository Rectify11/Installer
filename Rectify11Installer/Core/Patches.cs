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
                    })
            };
        }
    }
}
