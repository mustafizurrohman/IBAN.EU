// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-31-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="Austria.cs" company="IBANEU.Lib">
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
    /// Class Austria.
    /// </summary>
    internal class Austria : CustomizationBase
    {
        /// <summary>
        /// Gets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        internal override int IBANLength => 20;

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
                throw new Exception($"Austrian IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.BankCode = ibanAsString.Substring(4, 5);
            ibanDto.BranchCode = ibanDto.BankCode;

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Bank and Branc does from Austria may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(9, 11);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces =
                "AT" + Space + checksum + Space + ibanDto.BankCode + Space + ibanDto.AccountNumber;

            return ibanDto;

        }
    }
}
