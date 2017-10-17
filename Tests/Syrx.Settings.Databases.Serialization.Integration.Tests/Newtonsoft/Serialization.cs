using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Xunit.Assert;

namespace Syrx.Settings.Databases.Serialization.Integration.Tests.Newtonsoft
{
    public class Serialization
    {
        private readonly IDatabaseCommanderSettings _settingsWithConnections;
        private readonly IDatabaseCommanderSettings _settingsWithoutConnections;

        public Serialization()
        {
            _settingsWithConnections = new DatabaseCommanderSettings(new List<DatabaseCommandNamespaceSetting>
            {
                new DatabaseCommandNamespaceSetting("Syrx.Test.With.Connections",
                new List<DatabaseCommandTypeSetting>
                {
                    new DatabaseCommandTypeSetting("Syrx.Test.With.Connections.TestType", 
                     new Dictionary<string, DatabaseCommandSetting>
                     {
                         ["Retrieve"] = new DatabaseCommandSetting("test.alias.with.connection", "command text")
                     })
                })
            },new List<ConnectionStringSetting>
            {
                new ConnectionStringSetting("test.alias.with.connection", "test connection string")
            });


            _settingsWithoutConnections = new DatabaseCommanderSettings(new List<DatabaseCommandNamespaceSetting>
            {
                new DatabaseCommandNamespaceSetting("Syrx.Test.With.Out.Connections",
                new List<DatabaseCommandTypeSetting>
                {
                    new DatabaseCommandTypeSetting("Syrx.Test.With.Out.Connections.TestType",
                     new Dictionary<string, DatabaseCommandSetting>
                     {
                         ["Retrieve"] = new DatabaseCommandSetting("test.alias.with.out.connection", "command text")
                     })
                })
            });
        }

        [Fact]
        public void SerializeWithConnections()
        {
            var expect = @"{""Namespaces"":[{""Namespace"":""Syrx.Test.With.Connections"",""Types"":[{""Name"":""Syrx.Test.With.Connections.TestType"",""Commands"":{""Retrieve"":{""Split"":""Id"",""CommandText"":""command text"",""CommandTimeout"":30,""CommandType"":1,""Flags"":0,""IsolationLevel"":1048576,""ConnectionAlias"":""test.alias.with.connection""}}}]}],""Connections"":[{""Alias"":""test.alias.with.connection"",""ConnectionString"":""test connection string""}]}";
            var result = JsonConvert.SerializeObject(_settingsWithConnections);
            Equal(expect, result);
        }

        [Fact]
        public void SerializeWithoutConnections()
        {
            var expect = @"{""Namespaces"":[{""Namespace"":""Syrx.Test.With.Out.Connections"",""Types"":[{""Name"":""Syrx.Test.With.Out.Connections.TestType"",""Commands"":{""Retrieve"":{""Split"":""Id"",""CommandText"":""command text"",""CommandTimeout"":30,""CommandType"":1,""Flags"":0,""IsolationLevel"":1048576,""ConnectionAlias"":""test.alias.with.out.connection""}}}]}],""Connections"":null}";
            var result = JsonConvert.SerializeObject(_settingsWithoutConnections);
            Equal(expect, result);
        }

        [Fact]
        public void DeserializeWithoutConnections()
        {
            var input = @"{""Namespaces"":[{""Namespace"":""Syrx.Test.With.Out.Connections"",""Types"":[{""Name"":""Syrx.Test.With.Out.Connections.TestType"",""Commands"":{""Retrieve"":{""Split"":""Id"",""CommandText"":""command text"",""CommandTimeout"":30,""CommandType"":1,""Flags"":0,""IsolationLevel"":1048576,""ConnectionAlias"":""test.alias.with.out.connection""}}}]}],""Connections"":null}";
            var result = JsonConvert.DeserializeObject<DatabaseCommanderSettings>(input);
            NotNull(result);            
            Null(result.Connections);

            var namespaceSetting = result.Namespaces.Single();
            var typeSetting = namespaceSetting.Types.Single();
            var commandSetting = typeSetting.Commands.Single();

            Equal("Syrx.Test.With.Out.Connections", namespaceSetting.Namespace);            
            Equal("Syrx.Test.With.Out.Connections.TestType", typeSetting.Name);            
            Equal("Retrieve", commandSetting.Key);
            Equal("command text", commandSetting.Value.CommandText);
            Equal("test.alias.with.out.connection", commandSetting.Value.ConnectionAlias);
        }

        [Fact]
        public void DeserializeWithConnections()
        {
            var input = @"{""Namespaces"":[{""Namespace"":""Syrx.Test.With.Connections"",""Types"":[{""Name"":""Syrx.Test.With.Connections.TestType"",""Commands"":{""Retrieve"":{""Split"":""Id"",""CommandText"":""command text"",""CommandTimeout"":30,""CommandType"":1,""Flags"":0,""IsolationLevel"":1048576,""ConnectionAlias"":""test.alias.with.connection""}}}]}],""Connections"":[{""Alias"":""test.alias.with.connection"",""ConnectionString"":""test connection string""}]}";
            var result = JsonConvert.DeserializeObject<DatabaseCommanderSettings>(input);
            NotNull(result);
            NotNull(result.Connections);

            var namespaceSetting = result.Namespaces.Single();
            var typeSetting = namespaceSetting.Types.Single();
            var commandSetting = typeSetting.Commands.Single();

            Equal("Syrx.Test.With.Connections", namespaceSetting.Namespace);
            Equal("Syrx.Test.With.Connections.TestType", typeSetting.Name);
            Equal("Retrieve", commandSetting.Key);
            Equal("command text", commandSetting.Value.CommandText);
            Equal("test.alias.with.connection", commandSetting.Value.ConnectionAlias);
        }
    }
}
