// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.contracts
// Filename: TenantDTO.cs

using System;

namespace hms.entappsettings.contracts
{
    /// <summary>
    /// Tenant
    /// </summary>
    public class TenantDTO
    {
        /// <summary>
        /// Unique Tenant Id
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Tenant name (public display)
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// Tenant code (used for reporting and reference)
        /// </summary>
        public string TenantCode { get; set; }

        /// <summary>
        /// Tenant Description
        /// </summary>
        public string TenantDescription { get; set; }

        /// <summary>
        /// Date last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Last modified by
        /// </summary>
        public string ModifiedBy { get; set; }

    }
}