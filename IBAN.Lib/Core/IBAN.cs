// ***********************************************************************
// Assembly         : IBAN.Lib
// Author           : Mustafizur Rohman
// Created          : 10-24-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-24-2020
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
using System.Numerics;
using System.Text.RegularExpressions;

namespace IBANEU.Lib.Core
{

    /// <summary>
    /// IBAN Class
    /// </summary>
    public class IBAN
    {
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return AsStringWithSpaces;
        }


        private bool VerifyHash(string ibanAsString)
        {
            var newIban = ibanAsString.Substring(4) + ibanAsString.Substring(0, 4);

            newIban = Regex.Replace(newIban, @"\D", match => (match.Value[0] - 55).ToString());

            var remainder = BigInteger.Parse(newIban) % 97;

            return remainder == 1;
        }

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

            IBANDto parsed = this.CountryCode switch
            {
                "DE" => new Germany().ParseIbanFromString(ibanAsString),
                "CH" => new Switzerland().ParseIbanFromString(ibanAsString),
                "FR" => new France().ParseIbanFromString(ibanAsString),
                "IT" => new Italy().ParseIbanFromString(ibanAsString),
                "ES" => new Spain().ParseIbanFromString(ibanAsString),
                "LU" => new Luxembourg().ParseIbanFromString(ibanAsString),
                "AX" => new AlandIslands().ParseIbanFromString(ibanAsString),
                "AL" => new Albania().ParseIbanFromString(ibanAsString),
                "AD" => new Andorra().ParseIbanFromString(ibanAsString),
                _ => throw new Exception("Country not supported yet.")
            };

            this.Country = parsed.Country;
            this.AccountNumber = parsed.AccountNumber;
            this.BranchCode = parsed.BranchCode;
            this.BankCode = parsed.BankCode;
            this.AsStringWithSpaces = parsed.AsStringWithSpaces;
        }

        public static bool TryParse(string? s, out IBAN result)
        {

            if (string.IsNullOrWhiteSpace(s))
            {
                result = null;
                return false;
            }

            try
            {
                result = new IBAN(s);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }




    }
}

