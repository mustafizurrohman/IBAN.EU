// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-25-2020
// ***********************************************************************
// <copyright file="CountryHelper.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

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
        private static readonly List<string> CustomizedCountries = new List<string>()
        {
            "DE",
            "CH",
            "FR",
            "IT",
            "ES",
            "LU"
        };

        /// <summary>
        /// Determines whether [is country customized] [the specified country code].
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns><c>true</c> if [is country customized] [the specified country code]; otherwise, <c>false</c>.</returns>
        public static bool IsCountryCustomized(string countryCode)
        {
            return CustomizedCountries.Contains(countryCode);
        }
    }
}
