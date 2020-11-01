// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="Spain.cs" company="IBANEU.Lib">
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
    /// Class Spain.
    /// </summary>
    internal class Spain : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 24;

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANDto.</returns>
        /// <exception cref="Exception">Spanish IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">Spanish Checksums may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Bank codes may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Branch codes may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish CheckDigit may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Account Numbers may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish IBANs must have 24 characters</exception>
        /// <exception cref="Exception">Spanish Checksums may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Bank codes may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Branch codes may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish CheckDigit may contain only numbers.</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Spanish IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            if (!checksum.ContainsOnlyNumbers())
                throw new Exception("Spanish Checksums may contain only numbers.");

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Spanish Bank codes may contain only numbers.");

            ibanDto.BranchCode = ibanAsString.Substring(8, 4);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Spanish Branch codes may contain only numbers.");

            var checkDigit = ibanAsString.Substring(12, 2);

            if (!checkDigit.ContainsOnlyNumbers())
                throw new Exception("Spanish CheckDigit may contain only numbers.");


            ibanDto.AccountNumber = ibanAsString.Substring(14, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Spanish Account Numbers may contain only numbers.");

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "ES" + Space + checksum + Space
                                         + ibanDto.BankCode + Space + ibanDto.BranchCode
                                         + Space + checkDigit + Space + ibanDto.AccountNumber;


            return ibanDto;
        }
    }
}
