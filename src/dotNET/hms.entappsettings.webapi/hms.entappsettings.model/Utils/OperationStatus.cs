// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.model
// Filename: OperationStatus.cs

using System;
using System.Diagnostics;

namespace hms.entappsettings.model.Utils
{
    /// <summary>
    /// Object for holding and returning database operation info. Failure or success.
    /// </summary>
    [DebuggerDisplay("Operation Status: {Status}")]
    public class OperationStatus
    {
        /// <summary>
        /// True indicates success, false = failure
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// If records are impacted then you can return the number here
        /// </summary>
        public int RecordsAffected { get; set; }

        /// <summary>
        /// A simple message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// An id or object of some sort
        /// </summary>
        public Object OperationId { get; set; }

        /// <summary>
        /// The exception object
        /// </summary>
        public Exception ExceptionObject { get; set; }

        /// <summary>
        /// The exception message
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// The stack trace of an exception
        /// </summary>
        public string ExceptionStackTrace { get; set; }

        /// <summary>
        /// The inner exception message
        /// </summary>
        public string ExceptionInnerMessage { get; set; }

        /// <summary>
        /// The inner exception stack trace
        /// </summary>
        public string ExceptionInnerStackTrace { get; set; }

        /// <summary>
        /// Creates an Operation Status directly from an exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static OperationStatus CreateFromException(string message, Exception ex)
        {
            var opStatus = new OperationStatus
            {
                Status = false,
                Message = message,
                OperationId = null
            };

            if (ex != null)
            {
                opStatus.ExceptionObject = ex;
                opStatus.ExceptionMessage = ex.Message;
                opStatus.ExceptionStackTrace = ex.StackTrace;
                opStatus.ExceptionInnerMessage = ex.InnerException?.Message;
                opStatus.ExceptionInnerStackTrace = ex.InnerException?.StackTrace;
            }
            return opStatus;
        }

        public override string ToString()
        {
            return $"Status: {Status}, Message: {Message}";
        }
    }
}