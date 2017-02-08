// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 02
// 
// Project: hms.entappsettings.webapi
// Filename: MultiXmlDocumentationProvider.cs

using System;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using hms.entappsettings.webapi.Areas.HelpPage;
using hms.entappsettings.webapi.Areas.HelpPage.ModelDescriptions;

namespace hms.entappsettings.webapi.Utils
{
    /// <summary>
    /// A custom <see cref="IDocumentationProvider"/> that reads the API documentation from a collection of XML documentation files.
    /// Leverages the XmlDocumentProvider shipped with the Microsoft WebAPI Help Pages.
    /// </summary>
    /// <remarks>You need to switch Document provider in the HelpPageConfig and ensure all XmlDocuments are copied to the App_Data folder. 
    /// The copying is performed by the post-build script called CopyXmlDocs.cmd. Make sure this is setup for your drive</remarks>
    /// <seealso cref="HelpPageConfig"/>
    public class MultiXmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        /// <summary>
        /// The internal documentation providers for specific files.
        /// </summary>
        private readonly XmlDocumentationProvider[] _providers;


        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="paths">The physical paths to the XML documents.</param>
        public MultiXmlDocumentationProvider(params string[] paths)
        {
            this._providers = paths.Select(p => new XmlDocumentationProvider(p)).ToArray();
        }


        /// <summary>
        /// Gets the documentation for a subject.
        /// </summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(MemberInfo subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }


        /// <summary>
        /// Gets the documentation for a subject.
        /// </summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(Type subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }


        /// <summary>
        /// Gets the documentation for a subject.
        /// </summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(HttpControllerDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }


        /// <summary>
        /// Gets the documentation for a subject.
        /// </summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }


        /// <summary>
        /// Gets the documentation for a subject.
        /// </summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(HttpParameterDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }


        /// <summary>
        /// Gets the documentation for a subject.
        /// </summary>
        /// <param name="subject">The subject to document.</param>
        public string GetResponseDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }


        /// <summary>
        /// Get the first valid result from the collection of XML documentation providers.
        /// </summary>
        /// <param name="expr">The method to invoke.</param>
        private string GetFirstMatch(Func<XmlDocumentationProvider, string> expr)
        {
            return this._providers
                .Select(expr)
                .FirstOrDefault(p => !String.IsNullOrWhiteSpace(p));
        }
    }
}