﻿// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 11-01-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-01-2020
// ***********************************************************************
// <copyright file="BosniaAndHerzegovina.cs" company="IBANEU.Lib">
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
    /// Class BosniaAndHerzegovina.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class BosniaAndHerzegovina : CustomizationBase
    {
        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 20;
        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">French IBANs must have {IBANLength} characters</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"French IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            if (!checksum.ContainsOnlyNumbers())
                throw new Exception("Bosnia and Herzegovinian checksum may contain only numbers.");

            ibanDto.BankCode = ibanAsString.Substring(4, 3);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Bosnia and Herzegovinian BankCodes may contain only numbers.");

            ibanDto.BranchCode = ibanAsString.Substring(7, 3);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Bosnia and Herzegovinian BranchCodes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(10, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Bosnia and Herzegovinian AccountNumber may contain only numbers.");


            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "BA" + Space + checksum + Space + ibanDto.BankCode + Space
                                         + ibanDto.BranchCode + Space + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
