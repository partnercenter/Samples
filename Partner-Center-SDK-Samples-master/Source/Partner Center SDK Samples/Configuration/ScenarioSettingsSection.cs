﻿// -----------------------------------------------------------------------
// <copyright file="ScenarioSettingsSection.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples.Configuration
{
    /// <summary>
    /// Holds the scenario specific settings section.
    /// </summary>
    public class ScenarioSettingsSection : Section
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioSettingsSection"/> class.
        /// </summary>
        public ScenarioSettingsSection() : base("ScenarioSettings")
        {
        }

        /// <summary>
        /// Gets the customer domain suffix.
        /// </summary>
        public string CustomerDomainSuffix
        {
            get
            {
                return this.ConfigurationSection["CustomerDomainSuffix"];
            }
        }

        /// <summary>
        /// Gets the ID of the customer to delete from the TIP account.
        /// </summary>
        public string CustomerIdToDelete
        {
            get
            {
                return this.ConfigurationSection["CustomerIdToDelete"];
            }
        }

        /// <summary>
        /// Gets the ID of the customer whose details should be read.
        /// </summary>
        public string DefaultCustomerId
        {
            get
            {
                return this.ConfigurationSection["DefaultCustomerId"];
            }
        }

        /// <summary>
        /// Gets the number of customers to return in each customer page.
        /// </summary>
        public int CustomerPageSize
        {
            get
            {
                return int.Parse(this.ConfigurationSection["CustomerPageSize"]);
            }
        }

        /// <summary>
        /// Gets the number of offers to return in each offer page.
        /// </summary>
        public int DefaultOfferPageSize
        {
            get
            {
                return int.Parse(this.ConfigurationSection["DefaultOfferPageSize"]);
            }
        }

        /// <summary>
        /// Gets the number of invoices to return in each invoice page.
        /// </summary>
        public int InvoicePageSize
        {
            get
            {
                return int.Parse(this.ConfigurationSection["InvoicePageSize"]);
            }
        }

        /// <summary>
        /// Gets the configured Invoice ID.
        /// </summary>
        public string DefaultInvoiceId
        {
            get
            {
                return this.ConfigurationSection["DefaultInvoiceId"];
            }
        }

        /// <summary>
        /// Gets the configured partner MPD ID.
        /// </summary>
        public string PartnerMpnId
        {
            get
            {
                return this.ConfigurationSection["PartnerMpnId"];
            }
        }

        /// <summary>
        /// Gets the configured offer ID.
        /// </summary>
        public string DefaultOfferId
        {
            get
            {
                return this.ConfigurationSection["DefaultOfferId"];
            }
        }

        /// <summary>
        /// Gets the configured order ID.
        /// </summary>
        public string DefaultOrderId
        {
            get
            {
                return this.ConfigurationSection["DefaultOrderId"];
            }
        }

        /// <summary>
        /// Gets the configured subscription ID.
        /// </summary>
        public string DefaultSubscriptionId
        {
            get
            {
                return this.ConfigurationSection["DefaultSubscriptionId"];
            }
        }

        /// <summary>
        /// Gets the service request ID.
        /// </summary>
        public string DefaultServiceRequestId
        {
            get
            {
                return this.ConfigurationSection["DefaultServiceRequestId"];
            }
        }

        /// <summary>
        /// Gets the number of service requests to return in each service request page.
        /// </summary>
        public int ServiceRequestPageSize
        {
            get
            {
                return int.Parse(this.ConfigurationSection["ServiceRequestPageSize"]);
            }
        }

        /// <summary>
        /// Gets the configured support topic ID for creating new service request.
        /// </summary>
        public string DefaultSupportTopicId
        {
            get
            {
                return this.ConfigurationSection["DefaultSupportTopicId"];
            }
        }
    }
}
