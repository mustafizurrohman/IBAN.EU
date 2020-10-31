// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="CountryHelper.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace IBANEU.Lib.Helper
{
    /// <summary>
    /// Class CountryHelper.
    /// </summary>
    internal static class CountryHelper
    {

        /// <summary>
        /// The customized countries
        /// </summary>
        private static readonly Dictionary<string, string> CustomizedCountries = new Dictionary<string, string>()
        {
            { "DE", "Germany" },
            { "CH", "Switzerland" },
            { "FR", "France" },
            { "IT", "Italy" },
            { "ES", "Spain" },
            { "LU", "Luxembourg" },
            { "AX", "Aland Islands"},
            { "AL", "Albania"},
            { "AD", "Andorra" },
            { "AT", "Austria" }
        };

        /// <summary>
        /// Determines whether [is country customized] [the specified country code].
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns><c>true</c> if [is country customized] [the specified country code]; otherwise, <c>false</c>.</returns>
        public static bool IsCountryCustomized(string countryCode)
        {
            return CustomizedCountries.Keys.Contains(countryCode);
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

            return CustomizedCountries[countryCode];
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
    }
}
