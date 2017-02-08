// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.model
// Filename: SettingType.cs

// ReSharper disable InconsistentNaming
namespace hms.entappsettings.model.Enums
{
    /// <summary>
    /// App Setting Value Types. Must match the database table
    /// </summary>
    public enum SettingType
    {
        /// <summary>
        /// PLain Text value content
        /// </summary>
        TEXT = 1,
        /// <summary>
        /// Integer value content
        /// </summary>
        INTEGER = 2,
        /// <summary>
        /// Decimal or float value content
        /// </summary>
        DECIMAL = 3,
        /// <summary>
        /// Boolean content (true, false)
        /// </summary>
        BOOL = 4,
        /// <summary>
        /// Comma seperated list value content
        /// </summary>
        CSV = 5,
        /// <summary>
        /// HTML value content
        /// </summary>
        HTML = 6,
        /// <summary>
        /// JSON value content
        /// </summary>
        JSON = 7,
        /// <summary>
        /// XML value content
        /// </summary>
        XML = 8
    }
}