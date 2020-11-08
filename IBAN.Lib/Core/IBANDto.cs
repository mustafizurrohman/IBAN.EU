// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="IBANDto.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Helper;

namespace IBANEU.Lib.Core
{
    /// <summary>
    /// Class IBANDto.
    /// </summary>
    internal class IBANDto
    {
        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; private set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the BranchCode.
        /// </summary>
        /// <value>The BranchCode.</value>
        public string BranchCode { get; set; }

        /// <summary>
        /// Gets the bank code.
        /// </summary>
        /// <value>The bank code.</value>
        public string BankCode { get; set; }

        /// <summary>
        /// Gets the account number.
        /// </summary>
        /// <value>The account number.</value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets as string.
        /// </summary>
        /// <value>As string.</value>
        public string AsString { get; set; }

        /// <summary>
        /// Gets or sets as string with spaces.
        /// </summary>
        /// <value>As string with spaces.</value>
        public string AsStringWithSpaces { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return AsString;
        }

        /// <summary>
        /// Assigns the country and code.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        internal void AssignCountryAndCode(string ibanAsString)
        {
            this.CountryCode = CountryHelper.GetCountryCode(ibanAsString);
            this.Country = CountryHelper.GetCountryFromIBANString(ibanAsString);
            this.AsString = ibanAsString;
        }

    }
}
