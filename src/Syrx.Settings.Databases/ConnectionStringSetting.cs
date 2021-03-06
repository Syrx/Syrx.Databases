﻿//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  modified     : 2017.10.15 (22:43)
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
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringSetting"/> class.
        /// </summary>
        /// <param name="alias">An alias to refer to this connection.</param>        
        /// <param name="connectionString">The connection string.</param>        
        public ConnectionStringSetting(
            string alias,            
            string connectionString)
        {
            Require<ArgumentNullException>(!string.IsNullOrWhiteSpace(alias), nameof(alias));            
            Require<ArgumentNullException>(!string.IsNullOrWhiteSpace(connectionString), nameof(connectionString));

            Alias = alias;            
            ConnectionString = connectionString;
        }
    }
}