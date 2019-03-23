//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System.Collections.Generic;

namespace Syrx.Settings.Databases
{
    public interface IDatabaseCommanderSettings : ISettings<DatabaseCommandSetting>
    {
        IEnumerable<ConnectionStringSetting> Connections { get; set; }
    }
}