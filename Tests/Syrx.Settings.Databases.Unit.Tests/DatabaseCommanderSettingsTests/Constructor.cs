//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System;
using System.Collections.Generic;
using Xunit;
using static Xunit.Assert;

namespace Syrx.Settings.Databases.Unit.Tests.DatabaseCommanderSettingsTests
{
    public class Constructor
    {
        public Constructor()
        {
            _connectionStringSettings = new List<ConnectionStringSetting>
            {
                new ConnectionStringSetting("test", "connectionString")
            };

            _namespaceSettings = new List<DatabaseCommandNamespaceSetting>
            {
                new DatabaseCommandNamespaceSetting("test.namespace", new List<DatabaseCommandTypeSetting>
                {
                    new DatabaseCommandTypeSetting("test.type", new Dictionary<string, DatabaseCommandSetting>
                    {
                        ["Retrieve"] = new DatabaseCommandSetting("alias", "select 1")
                    })
                })
            };
        }

        private readonly IEnumerable<DatabaseCommandNamespaceSetting> _namespaceSettings;
        private readonly IEnumerable<ConnectionStringSetting> _connectionStringSettings;

        [Fact]
        public void EmptyConnectionStringSettingListThrowsArgumentException()
        {
            var result = Throws<ArgumentException>(() =>
                new DatabaseCommanderSettings(_namespaceSettings, new List<ConnectionStringSetting>()));
            Equal("At least 1 ConnectionStringSetting was expected.", result.Message);
        }

        [Fact]
        public void EmptyNamespaceSettingListThrowsArgumentException()
        {
            var result = Throws<ArgumentException>(() =>
                new DatabaseCommanderSettings(new List<DatabaseCommandNamespaceSetting>(), _connectionStringSettings));
            Equal(
                "At least 1 DatabaseCommandNamespaceSetting was expected to be passed to the DatabaseCommanderSettings constructor.",
                result.Message);
        }

        [Fact]
        public void NullConnectionStringSettingsSupported()
        {
            var result = new DatabaseCommanderSettings(_namespaceSettings);
            Equal(_namespaceSettings, result.Namespaces);
        }

        [Fact]
        public void NullNamespacesSettingThrowsArgumentNullException()
        {
            var result =
                Throws<ArgumentNullException>(() =>
                    new DatabaseCommanderSettings(null, _connectionStringSettings));
            Equal("Value cannot be null.\r\nParameter name: namespaces", result.Message);
        }

        [Fact]
        public void Successfully()
        {
            var result = new DatabaseCommanderSettings(_namespaceSettings, _connectionStringSettings);
            Equal(_connectionStringSettings, result.Connections);
            Equal(_namespaceSettings, result.Namespaces);
        }

        [Fact]
        public void SupportsMutableConnectionSettings()
        {
            var newConnections = new List<ConnectionStringSetting>
            {
                new ConnectionStringSetting("connections.a", "connection.string.a")
            };

            var result = new DatabaseCommanderSettings(_namespaceSettings, _connectionStringSettings);
            Equal(_connectionStringSettings, result.Connections);

            // mutate
            result.Connections = newConnections;
            Equal(newConnections, result.Connections);
        }
    }
}