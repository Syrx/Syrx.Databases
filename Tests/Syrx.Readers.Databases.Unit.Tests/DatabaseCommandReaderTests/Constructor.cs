//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System;
using Xunit;
using static Xunit.Assert;

namespace Syrx.Readers.Databases.Unit.Tests.DatabaseCommandReaderTests
{
    public class Constructor
    {
        [Fact]
        public void NullSettingsThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => new DatabaseCommandReader(null));
            const string expect =
                "Value cannot be null.\r\nParameter name: settings. No settings were passed to DatabaseCommandReader.";
            Equal(expect, result.Message);
        }

        [Fact]
        public void Successfully()
        {
            var result = new DatabaseCommandReader(SettingsDouble.GetSettings());
            NotNull(result);
        }
    }
}