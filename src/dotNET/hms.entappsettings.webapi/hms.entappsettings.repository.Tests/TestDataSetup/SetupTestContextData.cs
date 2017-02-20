// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 05
// 
// Project: hms.entappsettings.repository.Tests
// Filename: SetupTestContextData.cs

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using hms.entappsettings.context;
using hms.entappsettings.model;
using hms.entappsettings.model.Enums;
using hms.entappsettings.Tests.common;
using NUnit.Framework;

namespace hms.entappsettings.repository.Repositories.Tests
{
    /// <summary>
    /// This class will create ACTUAL DATA in the integration DATABASE!!
    /// </summary>
    [SetUpFixture]
    public class SetupDataForIntegrationTests
    {
        [OneTimeSetUp]
        public void CreateData()
        {
            var testDataHandler = new TestDataHandler();

            //testDataHandler.ForceInitialClearDownOfDb = true;

            testDataHandler.CreateData();
        }


        /// <summary>
        /// Truncates all tables and reset identities to zero
        /// </summary>
        [OneTimeTearDown]
        public void TearDownTestData()
        {
            var testDataHandler = new TestDataHandler();
            testDataHandler.TearDownTestData();
        }
    }
}