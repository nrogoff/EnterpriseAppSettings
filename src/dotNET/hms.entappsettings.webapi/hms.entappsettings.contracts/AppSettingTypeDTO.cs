// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.contracts
// Filename: AppSettingTypeDTO.cs

using System;

namespace hms.entappsettings.contracts
{
    /// <summary>
    /// App Setting value data type
    /// </summary>
    public class AppSettingTypeDTO
    {
        /// <summary>
        /// Unique Type Id
        /// </summary>
        public int AppSettingTypeId { get; set; }

        /// <summary>
        /// Type code
        /// </summary>
        public string AppSettingType_ { get; set; }

        /// <summary>
        /// Type description
        /// </summary>
        public string AppSettingTypeDescription { get; set; }

        /// <summary>
        /// Date last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Last updated by
        /// </summary>
        public string ModifiedBy { get; set; }

    }
}