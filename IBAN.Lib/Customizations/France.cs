﻿// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="France.cs" company="IBANEU.Lib">
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
    /// Class France.
    /// </summary>
    internal class France : CustomizationBase
    {
        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 27;

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "FR";

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">French IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">French Branch codes may contain only numbers.</exception>
        /// <exception cref="Exception">French Bank codes may contain only numbers.</exception>
        /// <exception cref="Exception">French IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">French Branch codes may contain only numbers.</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"French IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BranchCode = ibanAsString.Substring(9, 5);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("French Branch codes may contain only numbers.");

            ibanDto.BankCode = ibanAsString.Substring(4, 5);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("French Bank codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(14, 11);

            var checkSum = ibanAsString.Substring(2, 2);

            var checkDigits = ibanAsString.Substring(25, 2);


            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkSum, ibanDto.BankCode, ibanDto.BranchCode, ibanDto.AccountNumber, checkDigits
            });

            return ibanDto;
        }
    }

}
