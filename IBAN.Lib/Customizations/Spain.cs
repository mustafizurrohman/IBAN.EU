// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-25-2020
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
    internal class Spain
    {
        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANDto.</returns>
        /// <exception cref="Exception">Spanish IBANs must have 24 characters</exception>
        /// <exception cref="Exception">Spanish Checksums may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Bank codes may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Branch codes may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish CheckDigit may contain only numbers.</exception>
        /// <exception cref="Exception">Spanish Account Numbers may contain only numbers.</exception>
        internal IBANDto ParseIbanFromString(string ibanAsString)
        {
            var space = " ";

            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 24)
                throw new Exception("Spanish IBANs must have 24 characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.Country = "Spain";
            ibanDto.CountryCode = "ES";

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
            ibanDto.AsStringWithSpaces = "ES" + space + checksum + space
                                         + ibanDto.BankCode + space + ibanDto.BranchCode
                                         + space + checkDigit + space + ibanDto.AccountNumber;


            return ibanDto;
        }
    }
}
