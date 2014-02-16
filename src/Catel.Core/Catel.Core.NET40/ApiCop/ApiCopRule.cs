﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiCopRule.cs" company="Catel development team">
//   Copyright (c) 2008 - 2014 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Catel.ApiCop
{
    using System;

    /// <summary>
    /// Class containing an ApiCop rule.
    /// </summary>
    public class ApiCopRule : IApiCopRule
    {
        private readonly Func<IApiCop, IApiCopRule, object, bool> _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCopRule" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="predicate">The predicate.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="predicate" /> is <c>null</c>.</exception>
        public ApiCopRule(string name, string description, Func<IApiCop, IApiCopRule, object, bool> predicate)
        {
            Argument.IsNotNullOrWhitespace("name", name);
            Argument.IsNotNullOrWhitespace("description", description);
            Argument.IsNotNull("predicate", predicate);

            Name = name;
            Description = description;
            _predicate = predicate;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; private  set; }

        /// <summary>
        /// Determines whether the specified ApiCop rule is valid.
        /// </summary>
        /// <param name="apiCop">The ApiCop.</param>
        /// <param name="tag">The tag.</param>
        /// <returns><c>true</c> if the specified ApiCop is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid(IApiCop apiCop, object tag)
        {
            Argument.IsNotNull("apiCop", apiCop);

            return _predicate.Invoke(apiCop, this, tag);
        }
    }
}