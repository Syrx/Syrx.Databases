//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System;
using System.Collections.Generic;
using Syrx.Settings.Databases;
using Xunit;
using Syrx.Commanders.Databases.Readers;
using static Xunit.Assert;

namespace Syrx.Readers.Databases.Unit.Tests.DatabaseCommandReaderTests
{    
    public class GetCommand
    {
        private readonly IDatabaseCommandReader _reader;
        public GetCommand()
        {
            var namespaces = new List<DatabaseCommandNamespaceSetting>
            {
                new DatabaseCommandNamespaceSetting(
                    "Syrx",
                    new List<DatabaseCommandTypeSetting>
                    {
                        new DatabaseCommandTypeSetting(
                            "Syrx.RootNamespaceTest", 
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias.rootnamespace", "root namespace")
                            })
                    }),                

                new DatabaseCommandNamespaceSetting(
                    "Syrx.Readers.Databases",
                    new List<DatabaseCommandTypeSetting>
                    {
                        new DatabaseCommandTypeSetting(
                            "Syrx.Readers.Databases.Unit.Tests.DatabaseCommandReaderTests.Dummy",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias", "dummy text")
                            }),
                        new DatabaseCommandTypeSetting(
                            "Syrx.Readers.Databases.Unit.Tests.DatabaseCommandReaderTests.NoCommandSettingTest",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias", "dummy text")
                            }),                        
                        new DatabaseCommandTypeSetting(
                            "Syrx.Readers.Databases.Unit.Tests.DatabaseCommandReaderTests.ParentNamespaceTest",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias.parentnamespace", "parent namespace")
                            })
                    }),
                new DatabaseCommandNamespaceSetting(
                    "Syrx.Testing.Readers",
                    new List<DatabaseCommandTypeSetting>
                    {                        
                        new DatabaseCommandTypeSetting(
                            "Syrx.Testing.Readers.FullNamespaceTest",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias.fullnamespacetest", "fullnamespacetest")
                            })
                    })
            };

            var settings = new DatabaseCommanderSettings(namespaces);
            
            _reader = new DatabaseCommandReader(settings);
        }
               

        [Fact]
        public void NoNamespaceSettingThrowsNullReferenceException()
        {
            var result = Throws<NullReferenceException>(() =>
                _reader.GetCommand(typeof(NoNamespaceType), "NoNamespaceSettingThrowsNullReferenceException"));
            Equal(
                $"'{typeof(NoNamespaceType).FullName}' does not belong to any NamespaceSetting.\r\nPlease check settings.",
                result.Message);
        }

        [Fact]
        public void NoTypeSettingThrowsNullReferenceException()
        {
            var result = Throws<NullReferenceException>(() => _reader.GetCommand(typeof(NoTypeSettingTest), "NoTypeSettingThrowsNullReferenceException"));
            var expect = $"The type '{typeof(NoTypeSettingTest).FullName}' has no entry in the type settings of namespace 'Syrx.Readers.Databases'. Please add a type setting entry to the namespace setting.";
            Equal(expect, result.Message);
        }

        [Fact]
        public void NoCommandSettingThrowsNullReferenceException()
        {
            var result = Throws<NullReferenceException>(() => _reader.GetCommand(typeof(NoCommandSettingTest), "DoesNotExist"));
            var expect = $"The command setting 'DoesNotExist' has no entry for the type setting '{typeof(NoCommandSettingTest).FullName}'. Please add a command setting entry to the type setting.";
            Equal(expect, result.Message);
        }

        [Fact]
        public void NullEmptyWhitespaceKeyThrowsArgumentNullException()
        {
            var nulled = Throws<ArgumentNullException>(() => _reader.GetCommand(typeof(NoCommandSettingTest), null));
            var empty = Throws<ArgumentNullException>(() => _reader.GetCommand(typeof(NoCommandSettingTest), string.Empty));
            var whitespace = Throws<ArgumentNullException>(() => _reader.GetCommand(typeof(NoCommandSettingTest), ""));

            const string expect = "Value cannot be null.\r\nParameter name: key";
            Equal(expect, nulled.Message);
            Equal(expect, empty.Message);
            Equal(expect, whitespace.Message);
        }

        [Fact]
        public void NullTypePassedThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => _reader.GetCommand(null, "test"));
            Equal("Value cannot be null.\r\nParameter name: type", result.Message);
        }

        [Fact]
        public void FindsSettingWithFullyQualified()
        {
            var result = _reader.GetCommand(typeof(Testing.Readers.FullNamespaceTest), "Retrieve");
            NotNull(result);
            Equal("fullnamespacetest", result.CommandText);            
            Equal("test.alias.fullnamespacetest", result.ConnectionAlias);
        }

        [Fact]
        public void FindsTypeInParentNamespace()
        {
            var result = _reader.GetCommand(typeof(ParentNamespaceTest), "Retrieve");
            NotNull(result);
            Equal("parent namespace", result.CommandText);
            Equal("test.alias.parentnamespace", result.ConnectionAlias);
        }

        [Fact]
        public void FindsTypeInRootNamespace()
        {
            var result = _reader.GetCommand(typeof(RootNamespaceTest), "Retrieve");
            NotNull(result);            
            Equal("root namespace", result.CommandText);
            Equal("test.alias.rootnamespace", result.ConnectionAlias);
        }        
    }

    internal class ParentNamespaceTest { }    
    internal class NoTypeSettingTest { }
    internal class NoCommandSettingTest { }    
}

namespace Syrx.Commanders.Databases.Readers
{
    internal class NoNamespaceType
    {
    }
}

namespace Syrx
{
    internal class RootNamespaceTest { }
}

namespace Syrx.Testing.Readers
{

    internal class FullNamespaceTest { }

    // root namespace
    // parent namespace
    // full namespace
    // no namespace
    // no type setting
    // no command setting
}