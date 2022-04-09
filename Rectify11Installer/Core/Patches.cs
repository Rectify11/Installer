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
                    })
            };
        }
    }
}
