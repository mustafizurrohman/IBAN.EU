// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="Switzerland.cs" company="IBANEU.Lib">
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
    /// Class Switzerland.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Switzerland : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 21;

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "CH";

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">Swiss IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">Swiss Bank codes may contain only numbers.</exception>
        /// <exception cref="Exception">Swiss Account numbers may contain only numbers.</exception>
        /// <exception cref="Exception">Swiss IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">Swiss Bank codes may contain only numbers.</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 21)
                throw new Exception($"Swiss IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BranchCode = ibanAsString.Substring(4, 5);
            ibanDto.BankCode = ibanDto.BranchCode;

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Swiss Bank codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(9, 12);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Swiss Account numbers may contain only numbers.");

            var checkSum = ibanAsString.Substring(2, 2);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkSum, ibanDto.BranchCode, ibanDto.AccountNumber
            });


            return ibanDto;
        }
    }
}
