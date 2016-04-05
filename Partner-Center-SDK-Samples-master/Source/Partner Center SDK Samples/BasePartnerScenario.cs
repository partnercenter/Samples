﻿// -----------------------------------------------------------------------
// <copyright file="BasePartnerScenario.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples
{
    using System;
    using System.Collections.Generic;
    using ScenarioExecution;

    /// <summary>
    /// The base class for partner scenarios. Provides common behavior for all partner scenarios.
    /// </summary>
    public abstract class BasePartnerScenario : IPartnerScenario
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePartnerScenario"/> class.
        /// </summary>
        /// <param name="title">The scenario title.</param>
        /// <param name="context">The scenario context.</param>
        /// <param name="executionStrategy">The scenario execution strategy.</param>
        /// <param name="childScenarios">The child scenarios attached to the current scenario.</param>
        public BasePartnerScenario(string title, IScenarioContext context, IScenarioExecutionStrategy executionStrategy = null, IReadOnlyList<IPartnerScenario> childScenarios = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("title has to be set");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.Title = title;
            this.Context = context;

            this.ExecutionStrategy = executionStrategy ?? new PromptExecutionStrategy();
            this.Children = childScenarios;
        }
        
        /// <summary>
        /// Gets the scenario title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the children scenarios of the current scenario.
        /// </summary>
        public IReadOnlyList<IPartnerScenario> Children { get; private set; }

        /// <summary>
        /// Gets the scenario context.
        /// </summary>
        public IScenarioContext Context { get; private set; }

        /// <summary>
        /// Gets or sets the scenario execution behavior.
        /// </summary>
        private IScenarioExecutionStrategy ExecutionStrategy { get; set; }

        /// <summary>
        /// Runs the scenario.
        /// </summary>
        public void Run()
        {
            do
            {
                Console.Clear();
                this.Context.ConsoleHelper.WriteColored(this.Title, ConsoleColor.DarkCyan);
                this.Context.ConsoleHelper.WriteColored(new string('-', 80), ConsoleColor.DarkCyan);
                Console.WriteLine();

                try
                {
                    this.RunScenario();
                }
                catch (Exception exception)
                {
                    this.Context.ConsoleHelper.StopProgress();
                    this.Context.ConsoleHelper.Error(exception.ToString());
                }

                Console.WriteLine();
            }
            while (!this.ExecutionStrategy.IsScenarioComplete(this));
        }

        /// <summary>
        /// Obtains a customer ID to work with from the configuration if set there or prompts the user to enter it.
        /// </summary>
        /// <param name="promptMessage">An optional custom prompt message.</param>
        /// <returns>A customer ID.</returns>
        protected string ObtainCustomerId(string promptMessage = default(string))
        {
            return this.ObtainValue(
                this.Context.Configuration.Scenario.DefaultCustomerId,
                "Customer Id",
                string.IsNullOrWhiteSpace(promptMessage) ? "Enter the customer ID" : promptMessage,
                "The customer ID can't be empty");
        }

        /// <summary>
        /// Obtains an MPN ID to work with from the configuration if set there or prompts the user to enter it.
        /// </summary>
        /// <param name="promptMessage">An optional custom prompt message.</param>
        /// <returns>The MPN ID.</returns>
        protected string ObtainMpnId(string promptMessage = default(string))
        {
            return this.ObtainValue(
                this.Context.Configuration.Scenario.PartnerMpnId,
                "MPN Id",
                string.IsNullOrWhiteSpace(promptMessage) ? "Enter the MPN ID" : promptMessage,
                "The MPN ID can't be empty");
        }
        
        /// <summary>
        /// Obtains an offer ID to work with from the configuration if set there or prompts the user to enter it.
        /// </summary>
        /// <param name="promptMessage">An optional custom prompt message.</param>
        /// <returns>The offer ID.</returns>
        protected string ObtainOfferId(string promptMessage = default(string))
        {
            return this.ObtainValue(
                this.Context.Configuration.Scenario.DefaultOfferId,
                "Offer Id",
                string.IsNullOrWhiteSpace(promptMessage) ? "Enter the offer ID" : promptMessage,
                "The Offer ID can't be empty");
        }

        /// <summary>
        /// Obtain an order ID to work with the configuration if set there or prompts the user to enter it.
        /// </summary>
        /// <param name="promptMessage">An optional custom prompt message</param>
        /// <returns>The order ID</returns>
        protected string ObtainOrderID(string promptMessage = default(string))
        {
            return this.ObtainValue(
                this.Context.Configuration.Scenario.DefaultOrderId,
                "Order Id",
                string.IsNullOrWhiteSpace(promptMessage) ? "Enter the order ID" : promptMessage,
                "The Order ID can't be empty");
        }

        /// <summary>
        /// Obtains an subscription ID to work with from the configuration if set there or prompts the user to enter it.
        /// </summary>
        /// <param name="customerId">The customer ID who owns the subscription.</param>
        /// <param name="promptMessage">An optional custom prompt message.</param>
        /// <returns>The subscription ID.</returns>
        protected string ObtainSubscriptionId(string customerId, string promptMessage = default(string))
        {
            var partnerOperations = this.Context.UserPartnerOperations;
            var subscriptionId = this.Context.Configuration.Scenario.DefaultSubscriptionId;

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                // get the customer subscriptions and let the user enter the subscription Id afterwards
                this.Context.ConsoleHelper.StartProgress("Retrieving customer subscriptions");
                var subscriptions = partnerOperations.Customers.ById(customerId).Subscriptions.Get();
                this.Context.ConsoleHelper.StopProgress();
                this.Context.ConsoleHelper.WriteObject(subscriptions, "Customer subscriptions");

                Console.WriteLine();
                subscriptionId = this.Context.ConsoleHelper.ReadNonEmptyString(
                    string.IsNullOrWhiteSpace(promptMessage) ? "Enter the subscription ID" : promptMessage, "Subscription ID can't be empty");
            }
            else
            {
                Console.WriteLine("Found subscription ID: {0} in configuration.", subscriptionId);
            }

            return subscriptionId;
        }

        /// <summary>
        /// Runs the scenario logic. This is delegated to the implementing sub class.
        /// </summary>
        protected abstract void RunScenario();

        /// <summary>
        /// Obtains a value to work with from the configuration if set there or prompts the user to enter it.
        /// </summary>
        /// <param name="configuredValue">The value read from the configuration.</param>
        /// <param name="title">The title of the value.</param>
        /// <param name="promptMessage">The prompt message to use if the value was not set in the configuration.</param>
        /// <param name="errorMessage">The error message to use if the user did not enter a value.</param>
        /// <returns>A string value.</returns>
        private string ObtainValue(string configuredValue, string title, string promptMessage, string errorMessage)
        {
            string value = configuredValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                // The value was not set in the configuration, prompt the user the enter value
                value = this.Context.ConsoleHelper.ReadNonEmptyString(promptMessage, errorMessage);
            }
            else
            {
                Console.WriteLine("Found {0}: {1} in configuration.", title, value);
            }

            return value;
        }
    }
}
