// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 11-08-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="FaroeIslands.cs" company="IBANEU.Lib">
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
    /// Class FaroeIslands.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class FaroeIslands : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 18;

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        protected override string CountryCode => "FO";

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">German IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">Bank Codes from Faroe Islands may contain only numbers.</exception>
        /// <exception cref="Exception">Account Numbers from Faroe Islands may contain only numbers.</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"German IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checkDigits = ibanAsString.Substring(2, 2);

            ibanDto.BranchCode = string.Empty;

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Bank Codes from Faroe Islands may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(8, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Account Numbers from Faroe Islands may contain only numbers.");


            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>()
            {
                "FO", checkDigits, ibanDto.BankCode, ibanDto.AccountNumber
            });

            return ibanDto;

        }
    }
}
