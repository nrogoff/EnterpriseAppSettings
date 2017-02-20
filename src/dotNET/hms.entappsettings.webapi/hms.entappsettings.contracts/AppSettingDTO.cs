// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.contracts
// Filename: AppSettingDTO.cs

using System;
using hms.entappsettings.model.Enums;
using System.ComponentModel.DataAnnotations;

namespace hms.entappsettings.contracts
{
    /// <summary>
    /// Application Setting
    /// </summary>
    public class AppSettingDTO
    {
        /// <summary>
        /// The setting Id
        /// </summary>
        public int? AppSettingId { get; set; }

        /// <summary>
        /// Setting Key
        /// </summary>
        [Display(Name = "Setting Key")]
        [Required]
        [StringLength(50)]
        public string SettingKey { get; set; }

        /// <summary>
        /// Tenant Id. Zero indicates that this setting applies to all affiliates unless overridden
        /// </summary>
        [Required]
        public int TenantId { get; set; }

        /// <summary>
        /// The group that this setting applies to. Each client is will be interested in the settings contained in a group and it's parent groups
        /// </summary>
        [Required]
        public string SettingGroupId { get; set; }

        /// <summary>
        /// The group path
        /// </summary>
        [Display(Name = "Group Path")]
        public string GroupPath { get; set; }

        /// <summary>
        /// The setting UI section
        /// </summary>
        [Required]
        public string SettingSectionId { get; set; }

        /// <summary>
        /// The type of setting value. e.g. HTML, TEXT, 
        /// </summary>
        [Required]
        public SettingType TypeId { get; set; }

        /// <summary>
        /// The setting value.
        /// </summary>
        [Display(Name = "Value")]
        public string SettingValue { get; set; }

        /// <summary>
        /// Indicates that this setting can not be edited in the DCR Admin UI
        /// </summary>
        [Display(Name = "Is locked?")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Indicates that this setting should be hidden to all public requests.
        /// </summary>
        [Display(Name = "Is Internal only?")]
        public bool IsInternalOnly { get; set; }

        /// <summary>
        /// Description of the App Setting
        /// </summary>
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// Last modified date and time
        /// </summary>
        [Display(Name = "Last modified")]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Modified by
        /// </summary>
        [StringLength(50)]
        [Display(Name = "Last modified by")]
        public string ModifiedBy { get; set; }
    }
}