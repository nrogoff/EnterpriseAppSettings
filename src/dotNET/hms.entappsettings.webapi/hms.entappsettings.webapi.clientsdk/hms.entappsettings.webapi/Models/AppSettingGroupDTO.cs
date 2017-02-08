﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace hms.entappsettings.webapi.clientsdk.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    /// <summary>
    /// App Setting Group
    /// </summary>
    public partial class AppSettingGroupDTO
    {
        /// <summary>
        /// Initializes a new instance of the AppSettingGroupDTO class.
        /// </summary>
        public AppSettingGroupDTO() { }

        /// <summary>
        /// Initializes a new instance of the AppSettingGroupDTO class.
        /// </summary>
        public AppSettingGroupDTO(int? appSettingGroupId = default(int?), int? parentGroupId = default(int?), string group = default(string), string description = default(string), string groupPath = default(string), DateTime? modifiedDate = default(DateTime?), string modifiedBy = default(string))
        {
            AppSettingGroupId = appSettingGroupId;
            ParentGroupId = parentGroupId;
            Group = group;
            Description = description;
            GroupPath = groupPath;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
        }

        /// <summary>
        /// Unique App Setting Group Id
        /// </summary>
        [JsonProperty(PropertyName = "AppSettingGroupId")]
        public int? AppSettingGroupId { get; set; }

        /// <summary>
        /// Parent Group Id
        /// </summary>
        [JsonProperty(PropertyName = "ParentGroupId")]
        public int? ParentGroupId { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        [JsonProperty(PropertyName = "Group")]
        public string Group { get; set; }

        /// <summary>
        /// Group Description
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Group Path
        /// </summary>
        [JsonProperty(PropertyName = "GroupPath")]
        public string GroupPath { get; set; }

        /// <summary>
        /// Date last modified
        /// </summary>
        [JsonProperty(PropertyName = "ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Last modified by
        /// </summary>
        [JsonProperty(PropertyName = "ModifiedBy")]
        public string ModifiedBy { get; set; }

    }
}