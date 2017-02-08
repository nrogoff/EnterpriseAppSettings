// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.model
// Filename: ParentAppSettingGroups.cs


namespace hms.entappsettings.model.Intermediate
{
    /// <summary>
    /// Intermediate model for joins
    /// </summary>
    public class ParentAppSettingGroups
    {
        public string Id { get; set; }
        public string ParentGroupId { get; set; }

    }
}