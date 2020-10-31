﻿// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-31-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="Albania.cs" company="IBANEU.Lib">
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
    /// Class Albania.
    /// </summary>
    internal class Albania : CustomizationBase
    {
        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        internal override int IBANLength => 28;

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="Exception">Albanian Islands must have {IBANLength} characters.</exception>
        /// <exception cref="Exception">Albanian Bank and Branch codes may contain only numbers.</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Albanian Islands must have {IBANLength} characters.");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BankCode = ibanAsString.Substring(4, 8);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Albanian Bank and Branch codes may contain only numbers.");

            ibanDto.BranchCode = ibanDto.BankCode;

            ibanDto.AccountNumber = ibanAsString.Substring(12, 16);

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.AsString = ibanAsString;

            ibanDto.AsStringWithSpaces = "AL" + Space + checksum + Space
                                         + ibanDto.BankCode + Space + ibanDto.AccountNumber;

            return ibanDto;
        }
    }

}
