// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.contracts
// Filename: AppSettingWithOverrideDTO.cs
namespace hms.entappsettings.contracts
{
    /// <summary>
    /// AppSetting with overridden indicator. Used in the management UI to highlight or dim entries
    /// </summary>
    public class AppSettingWithOverrideDTO : AppSettingDTO
    {
        /// <summary>
        /// Has this setting been overridden by one at a high level
        /// </summary>
        public bool Overridden { get; set; }
    }
}