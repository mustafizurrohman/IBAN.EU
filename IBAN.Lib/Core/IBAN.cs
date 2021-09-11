// ***********************************************************************
// Assembly         : IBAN.Lib
// Author           : Mustafizur Rohman
// Created          : 10-24-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="IBAN.cs" company="IBAN.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable InconsistentNaming

using IBANEU.Lib.Customizations;
using IBANEU.Lib.ExtensionMethods;
using IBANEU.Lib.Helper;
using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace IBANEU.Lib.Core
{

    /// <summary>
    /// IBAN Class
    /// </summary>
    public class IBAN
    {

        #region -- Attributes --

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; }

        /// <summary>
        /// Gets the BranchCode.
        /// </summary>
        /// <value>The BranchCode.</value>
        public string BranchCode { get; }

        /// <summary>
        /// Gets the bank code.
        /// </summary>
        /// <value>The bank code.</value>
        public string BankCode { get; }

        /// <summary>
        /// Gets the account number.
        /// </summary>
        /// <value>The account number.</value>
        public string AccountNumber { get; }

        /// <summary>
        /// Gets or sets as string with spaces.
        /// </summary>
        /// <value>As string with spaces.</value>
        private string AsStringWithSpaces { get; }

        #endregion

        #region -- ToString Override --

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return AsStringWithSpaces;
        }

        #endregion

        #region -- Constructor -- 

        /// <summary>
        /// Initializes a new instance of the <see cref="IBAN" /> class.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <exception cref="ArgumentException">IBAN cannot contain special characters.</exception>
        /// <exception cref="ArgumentException">Invalid IBAN. Hash verification failed.</exception>
        /// <exception cref="NotImplementedException">Country not yet suported.</exception>
        /// <exception cref="ArgumentException">IBAN cannot contain special characters.</exception>
        public IBAN(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces().ToUpperInvariant();

            if (ibanAsString.ContainsSpecialCharacters())
                throw new ArgumentException("IBAN cannot contain special characters.");

            if (!VerifyHash(ibanAsString))
                throw new ArgumentException("Invalid IBAN. Hash verification failed.");

            this.CountryCode = CountryHelper.GetCountryCode(ibanAsString);

            if (!CountryHelper.IsCountryCustomized(this.CountryCode))
                throw new NotImplementedException("Country not yet suported.");

            IBANDto parsed = GetIBANDto(ibanAsString);

            this.Country = parsed.Country;
            this.AccountNumber = parsed.AccountNumber;
            this.BranchCode = parsed.BranchCode;
            this.BankCode = parsed.BankCode;
            this.AsStringWithSpaces = parsed.AsStringWithSpaces;
        }

        #endregion

        #region -- TryParse Method --

        /// <summary>
        /// Tries to parse an IBAN from string and returns the object if the parse is succesful.
        /// </summary>
        /// <param name="sourceString">The sourceString.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if parse is succesful, <c>false</c> otherwise.</returns>
        public static bool TryParse(string? sourceString, out IBAN result)
        {

            if (string.IsNullOrWhiteSpace(sourceString))
            {
                result = null;
                return false;
            }

            try
            {
                result = new IBAN(sourceString);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }


        #endregion

        #region -- Private: Verify Hash Routine --

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool VerifyHash(string ibanAsString)
        {
            var newIban = ibanAsString.Substring(4) + ibanAsString.Substring(0, 4);

            newIban = Regex.Replace(newIban, @"\D", match => (match.Value[0] - 55).ToString());

            var remainder = BigInteger.Parse(newIban) % 97;

            return remainder == 1;
        }

        #endregion

        #region -- Private: Method to parse string to IBAN DTO --

        /// <summary>
        /// Gets the iban dto.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANDto.</returns>
        private IBANDto GetIBANDto(string ibanAsString)
        {
            var customizer = typeof(IBAN)
                .Assembly
                .GetTypes()
                .Where(typ => typ.IsClass && !typ.IsAbstract && typ.IsSubclassOf(typeof(CustomizationBase)))
                .Select(Activator.CreateInstance)
                .Cast<CustomizationBase>()
                .First(typ => typ.CountryCode == this.CountryCode);

            var parsed = customizer.ParseIbanFromString(ibanAsString);
            
            return parsed;
        }

        #endregion


    }
}

