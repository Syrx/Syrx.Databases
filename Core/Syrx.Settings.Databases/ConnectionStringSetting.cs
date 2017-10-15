﻿//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:40)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System;
using static Syrx.Validation.Contract;

namespace Syrx.Settings.Databases
{
    /// <summary>
    /// Used so that we don't have to store connection strings in .config files.
    /// </summary>
    public class ConnectionStringSetting
    {
        /// <summary>
        /// Used to reference this ConnectionStringSetting
        /// from a CommandSetting
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        /// <value>
        /// The name of the provider.
        /// </value>
        public string ProviderName { get; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringSetting"/> class.
        /// </summary>
        /// /// <param name="alias">An alias to refer to this connection.</param>
        /// <param name="providerName">The name of the provider used by the conenction.</param>
        /// <param name="connectionString">The connection string.</param>        
        public ConnectionStringSetting(
            string alias,
            string providerName,
            string connectionString)
        {
            Require<ArgumentNullException>(!string.IsNullOrWhiteSpace(alias), nameof(alias));
            Require<ArgumentNullException>(!string.IsNullOrWhiteSpace(providerName), nameof(providerName));
            Require<ArgumentNullException>(!string.IsNullOrWhiteSpace(connectionString),
                nameof(connectionString));

            Alias = alias;
            ProviderName = providerName;
            ConnectionString = connectionString;
        }
    }
}