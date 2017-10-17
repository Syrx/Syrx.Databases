//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System.Collections.Generic;
using Syrx.Readers.Databases.Unit.Tests.DatabaseCommandReaderTests;
using Syrx.Settings.Databases;

namespace Syrx.Readers.Databases.Unit.Tests
{
    public static class SettingsDouble
    {
        public static IDatabaseCommanderSettings GetSettings()
        {
            return new DatabaseCommanderSettings(
                new List<DatabaseCommandNamespaceSetting>
                {
                    new DatabaseCommandNamespaceSetting(
                        typeof(GetCommand).Namespace,
                        new List<DatabaseCommandTypeSetting>
                        {
                            new DatabaseCommandTypeSetting(
                                typeof(Constructor).FullName,
                                new Dictionary<string, DatabaseCommandSetting>
                                {
                                    ["Retrieve"] =
                                    new DatabaseCommandSetting("test_alias", "select 'Readers.Test.Settings'")
                                }),
                            new DatabaseCommandTypeSetting(
                                typeof(GetCommand).FullName,
                                new Dictionary<string, DatabaseCommandSetting>
                                {
                                    ["Retrieve"] =
                                    new DatabaseCommandSetting("test_alias", "select 'Readers.Test.Settings'")
                                })
                        })
                }
                , new List<ConnectionStringSetting>
                {
                    new ConnectionStringSetting("test_alias", "connectionString")
                });
        }
    }
}