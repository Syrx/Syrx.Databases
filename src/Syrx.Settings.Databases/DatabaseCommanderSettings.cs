//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using static Syrx.Validation.Contract;

namespace Syrx.Settings.Databases
{
    public class DatabaseCommanderSettings : IDatabaseCommanderSettings
    {
        public IEnumerable<INamespaceSetting<DatabaseCommandSetting>> Namespaces { get; }
        public IEnumerable<ConnectionStringSetting> Connections { get; set; }        
        
        public DatabaseCommanderSettings(
            IEnumerable<DatabaseCommandNamespaceSetting> namespaces,
            IEnumerable<ConnectionStringSetting> connections = null)
        {
            Require<ArgumentNullException>(namespaces != null, nameof(namespaces));
            var namespaceSettings = namespaces as DatabaseCommandNamespaceSetting[] ?? namespaces.ToArray();
            Require<ArgumentException>(namespaceSettings.Any(), Messages.EmptyNamespaceSettingsList);
            Namespaces = namespaceSettings;

            if (connections != null)
            {
                var connectionStringSettings = connections as ConnectionStringSetting[] ?? connections.ToArray();
                Require<ArgumentException>(connectionStringSettings.Any(), Messages.EmptyConnectionStringSettingsList);
                Connections = connectionStringSettings;
            }
        }

        private static class Messages
        {
            internal const string EmptyConnectionStringSettingsList =
                "At least 1 ConnectionStringSetting was expected.";

            internal const string EmptyNamespaceSettingsList =
                    "At least 1 DatabaseCommandNamespaceSetting was expected to be passed to the DatabaseCommanderSettings constructor."
                ;
        }
    }
}