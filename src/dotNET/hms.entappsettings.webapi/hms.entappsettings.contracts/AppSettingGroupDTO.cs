// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.contracts
// Filename: AppSettingGroupDTO.cs

using System;

namespace hms.entappsettings.contracts
{
    /// <summary>
    /// App Setting Group
    /// </summary>
    public class AppSettingGroupDTO
    {
        /// <summary>
        /// Unique App Setting Group Id
        /// </summary>
        public int AppSettingGroupId { get; set; } 

        /// <summary>
        /// Parent Group Id
        /// </summary>
        public int? ParentGroupId { get; set; } 

        /// <summary>
        /// Group name
        /// </summary>
        public string Group { get; set; } 

        /// <summary>
        /// Group Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Group Path
        /// </summary>
        public string GroupPath { get; set; }

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