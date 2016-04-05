// -----------------------------------------------------------------------
// <copyright file="CreateCustomer.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples.Customers
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Helpers;
    using Store.PartnerCenter.Models;
    using Store.PartnerCenter.Models.Customers;

    /// <summary>
    /// Creates a new customer for a partner.
    /// </summary>
    public class CreateCustomer : BasePartnerScenario
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomer"/> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public CreateCustomer(IScenarioContext context) : base("Create a new customer", context)
        {
        }

        /// <summary>
        /// executes the create customer scenario.
        /// </summary>
        protected override void RunScenario()
        {
            var partnerOperations = this.Context.UserPartnerOperations;

            var customerToCreate = new Customer()
            {
                CompanyProfile = new CustomerCompanyProfile()
                {
                    Domain = string.Format(CultureInfo.InvariantCulture, "SampleApplication{0}.{1}", new Random().Next(), this.Context.Configuration.Scenario.CustomerDomainSuffix)
                },
                BillingProfile = new CustomerBillingProfile()
                {
                    Culture = "EN-US",
                    Email = "SomeEmail@Outlook.com",
                    Language = "En",
                    CompanyName = "Some Company" + new Random().Next(),
                    DefaultAddress = new Address()
                    {
                        FirstName = "Admin",
                        LastName = "In Test",
                        AddressLine1 = "One Microsoft Way",
                        City = "Redmond",
                        State = "WA",
                        Country = "US",
                        PostalCode = "98052",
                        PhoneNumber = "4257778899"
                    }
                }
            };

            this.Context.ConsoleHelper.WriteObject(customerToCreate, "New user Information");
            this.Context.ConsoleHelper.StartProgress("Creating user");

            var newCustomer = partnerOperations.Customers.Create(customerToCreate);

            this.Context.ConsoleHelper.StopProgress();
            this.Context.ConsoleHelper.Success("Success!");
            this.Context.ConsoleHelper.WriteObject(newCustomer, "Created user Information");
        }
    }
}
