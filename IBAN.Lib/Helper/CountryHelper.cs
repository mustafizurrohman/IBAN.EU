// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="CountryHelper.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using IBANEU.Lib.Customizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IBANEU.Lib.Helper
{
    /// <summary>
    /// Class CountryHelper.
    /// </summary>
    internal static class CountryHelper
    {
        public static readonly List<CustomizationBase> Customizers = typeof(IBAN)
            .Assembly
            .GetTypes()
            .Where(typ => typ.IsClass && !typ.IsAbstract && typ.IsSubclassOf(typeof(CustomizationBase)))
            .Select(Activator.CreateInstance)
            .Cast<CustomizationBase>()
            .ToList();

        private static readonly List<string> CustomizedCountriesList = Customizers
            .Select(x => x.CountryCode)
            .ToList();


        /// <summary>
        /// Determines whether [is country customized] [the specified country code].
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns><c>true</c> if [is country customized] [the specified country code]; otherwise, <c>false</c>.</returns>
        public static bool IsCountryCustomized(string countryCode)
        {
            return CustomizedCountriesList.Contains(countryCode);
        }

        /// <summary>
        /// Gets the country from iban string.
        /// </summary>
        /// <param name="ibanString">The iban string.</param>
        /// <returns>string.</returns>
        /// <exception cref="Exception">Country not yet customized</exception>
        public static string GetCountryFromIBANString(string ibanString)
        {
            var countryCode = GetCountryCode(ibanString);

            if (!IsCountryCustomized(countryCode))
                throw new Exception("Country not yet customized");

            var customizerName = GetCountryFromCode(countryCode);

            return customizerName;
        }

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <param name="ibanString">The iban string.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentException">No country code found.</exception>
        public static string GetCountryCode(string ibanString)
        {
            if (ibanString.Length < 2)
                throw new ArgumentException("No country code found.");

            return ibanString.Substring(0, Math.Min(2, ibanString.Length)).ToUpperInvariant();
        }

        /// <summary>
        /// Gets the country from code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>System.String.</returns>
        private static string GetCountryFromCode(string countryCode)
        {
            var customizerName = Customizers
                .Single(x => x.CountryCode == countryCode)
                .GetType()
                .Name;

            if (customizerName == nameof(BosniaAndHerzegovina))
                return "Bosnia and Herzegovina";

            var formattedName = Regex.Split(customizerName, @"(?<!^)(?=[A-Z])")
                .Aggregate((a, b) => a + " " + b);

            return formattedName;

        }
    }
}
