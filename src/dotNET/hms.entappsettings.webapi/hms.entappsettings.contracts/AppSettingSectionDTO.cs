// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.contracts
// Filename: AppSettingSectionDTO.cs

using System;

namespace hms.entappsettings.contracts
{
    /// <summary>
    /// App Setting UI Section
    /// </summary>
    public class AppSettingSectionDTO
    {
        /// <summary>
        /// Unique Section Id
        /// </summary>
        public int AppSettingSectionId { get; set; }

        /// <summary>
        /// Parent Section Id
        /// </summary>
        public int? ParentSectionId { get; set; }

        /// <summary>
        /// Section name
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Section description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The ordinality of the section
        /// </summary>
        public int Ordinality { get; set; }

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