﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace hms.entappsettings.webapi.clientsdk
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for AppSettingSections.
    /// </summary>
    public static partial class AppSettingSectionsExtensions
    {
            /// <summary>
            /// Get App Setting Section details by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionId'>
            /// App Setting Section Id
            /// </param>
            public static AppSettingSectionDTO GetAppSettingSection(this IAppSettingSections operations, int appSettingSectionId)
            {
                return Task.Factory.StartNew(s => ((IAppSettingSections)s).GetAppSettingSectionAsync(appSettingSectionId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get App Setting Section details by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionId'>
            /// App Setting Section Id
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<AppSettingSectionDTO> GetAppSettingSectionAsync(this IAppSettingSections operations, int appSettingSectionId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAppSettingSectionWithHttpMessagesAsync(appSettingSectionId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Update an existing App Setting Section
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionId'>
            /// App Setting Section Id
            /// </param>
            /// <param name='appSettingSectionDTO'>
            /// App Setting Section DTO
            /// </param>
            public static void PutAppSettingSection(this IAppSettingSections operations, int appSettingSectionId, AppSettingSectionDTO appSettingSectionDTO)
            {
                Task.Factory.StartNew(s => ((IAppSettingSections)s).PutAppSettingSectionAsync(appSettingSectionId, appSettingSectionDTO), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Update an existing App Setting Section
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionId'>
            /// App Setting Section Id
            /// </param>
            /// <param name='appSettingSectionDTO'>
            /// App Setting Section DTO
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PutAppSettingSectionAsync(this IAppSettingSections operations, int appSettingSectionId, AppSettingSectionDTO appSettingSectionDTO, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PutAppSettingSectionWithHttpMessagesAsync(appSettingSectionId, appSettingSectionDTO, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Delete a AppSettingSection
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionId'>
            /// The App Setting Section Id
            /// </param>
            public static AppSettingSectionDTO DeleteAppSettingSection(this IAppSettingSections operations, int appSettingSectionId)
            {
                return Task.Factory.StartNew(s => ((IAppSettingSections)s).DeleteAppSettingSectionAsync(appSettingSectionId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Delete a AppSettingSection
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionId'>
            /// The App Setting Section Id
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<AppSettingSectionDTO> DeleteAppSettingSectionAsync(this IAppSettingSections operations, int appSettingSectionId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.DeleteAppSettingSectionWithHttpMessagesAsync(appSettingSectionId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Returns all Sections
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<AppSettingSectionDTO> GetAppSettingSections(this IAppSettingSections operations)
            {
                return Task.Factory.StartNew(s => ((IAppSettingSections)s).GetAppSettingSectionsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns all Sections
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<AppSettingSectionDTO>> GetAppSettingSectionsAsync(this IAppSettingSections operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAppSettingSectionsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Add an App Setting Section
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionDTO'>
            /// The new appSettingSection DTO
            /// </param>
            public static AppSettingSectionDTO PostAppSettingSection(this IAppSettingSections operations, AppSettingSectionDTO appSettingSectionDTO)
            {
                return Task.Factory.StartNew(s => ((IAppSettingSections)s).PostAppSettingSectionAsync(appSettingSectionDTO), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Add an App Setting Section
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='appSettingSectionDTO'>
            /// The new appSettingSection DTO
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<AppSettingSectionDTO> PostAppSettingSectionAsync(this IAppSettingSections operations, AppSettingSectionDTO appSettingSectionDTO, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostAppSettingSectionWithHttpMessagesAsync(appSettingSectionDTO, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
