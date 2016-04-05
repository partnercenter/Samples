// -----------------------------------------------------------------------
// <copyright file="GetPagedInvoices.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples.Invoice
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Gets paged invoices for partners.
    /// </summary>
    public class GetPagedInvoices : BasePartnerScenario
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPagedInvoices"/> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public GetPagedInvoices(IScenarioContext context) : base("Get paged invoices", context)
        {
        }

        /// <summary>
        /// executes the get paged invoices scenario.
        /// </summary>
        protected override void RunScenario()
        {
            var partnerOperations = this.Context.UserPartnerOperations;
            this.Context.ConsoleHelper.StartProgress("Querying invoices");
       
            // Query the invoices, get the first page if a page size was set, otherwise get all invoices
            var invoicesPage = partnerOperations.Invoices.Get();
            this.Context.ConsoleHelper.StopProgress();

            this.Context.ConsoleHelper.WriteObject(invoicesPage, "Invoices");
        }
    }
}
