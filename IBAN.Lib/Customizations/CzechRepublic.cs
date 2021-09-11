// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 11-07-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="CzechRepublic.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace IBANEU.Lib.Customizations
{
    /// <summary>
    /// Class CzechRepublic.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class CzechRepublic : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 24;


        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "CZ";

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">Czech IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">Account Prefix in Czech IBANs may contain only numbers</exception>
        /// <exception cref="Exception">Account Number in Czech IBANs may contain only numbers</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Czech IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BranchCode = string.Empty;

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            var accountPrefix = ibanAsString.Substring(8, 6);
            var accountNumber = ibanAsString.Substring(14, 10);

            if (!accountPrefix.ContainsOnlyNumbers())
                throw new Exception("Account Prefix in Czech IBANs may contain only numbers");

            if (!accountNumber.ContainsOnlyNumbers())
                throw new Exception("Account Number in Czech IBANs may contain only numbers");

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.AccountNumber = accountPrefix + Space + accountNumber;

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "CZ" + Space + checksum + Space + ibanDto.BankCode + Space +
                                         accountPrefix + Space + accountNumber;

            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checksum, ibanDto.BankCode, ibanDto.AccountNumber
            });

            return ibanDto;
        }
    }
}
