// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 09-12-2021
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 09-12-2021
// ***********************************************************************
// <copyright file="Kosovo.cs" company="IBANEU.Lib">
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
    /// Class Kosovo.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Kosovo : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 20;

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "XK";
        
        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="NotImplementedException"></exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Kosovian IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checkSum = ibanAsString.Substring(2, 2);

            if (!checkSum.ContainsOnlyNumbers())
                throw new Exception("Kosovian IBAN checksums may contain only digits.");

            ibanDto.BankCode = ibanAsString.Substring(4, 2);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Kosovian BankCodes may contain only characters.");

            ibanDto.BranchCode = ibanAsString.Substring(6, 2);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Kosovian Branch Codes may contain only digits.");

            ibanDto.AccountNumber = ibanAsString.Substring(8, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Kosovian AccountNumber may contain only digits.");

            var checkDigit = ibanAsString.Substring(18, 2);

            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkSum, ibanDto.BankCode, ibanDto.BranchCode, ibanDto.AccountNumber, checkDigit
            });

            return ibanDto;
        }
    }
}
