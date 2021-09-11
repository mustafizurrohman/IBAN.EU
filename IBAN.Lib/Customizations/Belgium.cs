// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 11-01-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="Belgium.cs" company="IBANEU.Lib">
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
    /// Class Belgium.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Belgium : CustomizationBase
    {
        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 16;

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "BE";

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">Belgian IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">Belgian Bank codes may contain only numbers.</exception>
        /// <exception cref="Exception">Belgian Account Numbers may contain only numbers.</exception>
        /// <exception cref="NotImplementedException"></exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Belgian IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.BankCode = ibanAsString.Substring(4, 3);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Belgian Bank codes may contain only numbers.");

            ibanDto.BranchCode = ibanDto.BankCode;

            ibanDto.AccountNumber = ibanAsString.Substring(7, 7);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Belgian Account Numbers may contain only numbers.");

            var checkDigit = ibanAsString.Substring(14, 2);

            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checksum, ibanDto.BankCode, ibanDto.AccountNumber, checkDigit
            });

            return ibanDto;
        }
    }
}
