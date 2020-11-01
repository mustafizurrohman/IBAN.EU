﻿// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 11-01-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-01-2020
// ***********************************************************************
// <copyright file="Croatia.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;

namespace IBANEU.Lib.Customizations
{
    /// <summary>
    /// Class Croatia.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Croatia : CustomizationBase
    {
        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 21;

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">Croatian IBANs must have {IBANLength} characters</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Croatian IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checkSum = ibanAsString.Substring(2, 2);

            ibanDto.BranchCode = ibanAsString.Substring(4, 7);
            ibanDto.BankCode = ibanDto.BranchCode;

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Croatian Brank Codes may contain only numbers");

            ibanDto.AccountNumber = ibanAsString.Substring(11, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Croatian Account Numbers may contain only numbers");

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "HR" + Space + checkSum + Space
                                         + ibanDto.BankCode + Space + ibanDto.AccountNumber;


            return ibanDto;
        }
    }
}