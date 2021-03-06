﻿//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System.ComponentModel;
using System.Data;
using System.Threading;
using Dapper;
using Syrx.Connectors.Databases;
using Syrx.Readers.Databases;
using Syrx.Settings.Databases;

namespace Syrx.Commanders.Databases
{
    public partial class DatabaseCommander<TRepository> : ICommander<TRepository>
    {
        private readonly IDatabaseConnector _connector;
        private readonly IDatabaseCommandReader _reader;

        public void Dispose()
        {
        }

        public DatabaseCommander(IDatabaseCommandReader reader, IDatabaseConnector connector)
        {
            _reader = reader;
            _connector = connector;
        }

        [Browsable(false)]
        private CommandDefinition GetCommandDefinition(
            DatabaseCommandSetting setting,
            object parameters = null,
            IDbTransaction transaction = null,
            CancellationToken cancellationToken = default(CancellationToken)) => new CommandDefinition(
            commandText: setting.CommandText,
            parameters: parameters,
            transaction: transaction,
            commandTimeout: setting.CommandTimeout,
            commandType: setting.CommandType,
            flags: (CommandFlags) setting.Flags,
            cancellationToken: cancellationToken);
    }
}