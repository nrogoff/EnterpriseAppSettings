// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 31
// 
// Project: hms.entappsettings.contracts
// Filename: AutoMapperBootstrap.cs

using AutoMapper;
using hms.entappsettings.model;

namespace hms.entappsettings.contracts.Automapper
{
    /// <summary>
    /// AutoMapper Bootstrap class for the Enterprise App Settings DTO Contracts. This should be called by the consuming service 
    /// in its startup routines such as Global.asax.cs
    /// <see cref="T:AutoMapper.Mapper" />
    /// </summary>
    public class EntAppContractsAutoMapperProfile : Profile
    {
        /// <summary>
        /// AutoMapper profile for Enterprise Application Settings DTO Contracts
        /// </summary>
        public EntAppContractsAutoMapperProfile()
        {
            CreateMap<AppSetting, AppSettingDTO>()
                .ForMember(x => x.GroupPath, opt => opt.MapFrom(x => x.AppSettingGroup.GroupPath));
            CreateMap<AppSettingDTO, AppSetting>()
                .ForMember(x => x.AppSettingGroup, opt => opt.Ignore())
                .ForMember(x => x.AppSettingSection, opt => opt.Ignore())
                .ForMember(x => x.Tenant, opt => opt.Ignore())
                .ForMember(x => x.AppSettingType, opt => opt.Ignore());
            CreateMap<AppSetting, AppSettingWithOverrideDTO>()
                .ForMember(x => x.Overridden, opt => opt.Ignore())
                .ForMember(x => x.GroupPath, opt => opt.MapFrom(x => x.AppSettingGroup.GroupPath));
            CreateMap<AppSettingDTO, AppSettingWithOverrideDTO>()
                .ForMember(x => x.Overridden, opt => opt.Ignore());
            CreateMap<AppSettingWithOverrideDTO, AppSetting>()
                .ForSourceMember(x => x.Overridden, opt => opt.Ignore())
                .ForMember(x => x.AppSettingGroup, opt => opt.Ignore())
                .ForMember(x => x.AppSettingSection, opt => opt.Ignore())
                .ForMember(x => x.AppSettingType, opt => opt.Ignore())
                .ForMember(x => x.Tenant, opt => opt.Ignore());

            CreateMap<AppSettingGroup, AppSettingGroupDTO>();
            CreateMap<AppSettingGroupDTO, AppSettingGroup>()
                .ForMember(x => x.AppSettings, opt => opt.Ignore())
                .ForMember(x => x.AppSettingGroups, opt => opt.Ignore())
                .ForMember(x => x.ParentGroup, opt => opt.Ignore());

            CreateMap<AppSettingSection, AppSettingSectionDTO>();
            CreateMap<AppSettingSectionDTO, AppSettingSection>()
                .ForMember(x => x.AppSettingSections, opt => opt.Ignore())
                .ForMember(x => x.ParentSection, opt => opt.Ignore())
                .ForMember(x => x.AppSettings, opt => opt.Ignore());

            CreateMap<AppSettingType, AppSettingTypeDTO>();
            CreateMap<AppSettingTypeDTO, AppSettingType>()
                .ForMember(x => x.AppSettings, opt => opt.Ignore());

            CreateMap<Tenant, TenantDTO>();
            CreateMap<TenantDTO, Tenant>()
                .ForMember(x => x.AppSettings, opt => opt.Ignore());
        }
    }
}