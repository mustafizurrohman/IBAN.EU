// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-31-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="Andorra.cs" company="IBANEU.Lib">
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
    /// Class Andorra.
    /// </summary>
    internal class Andorra
    {
        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANDto.</returns>
        /// <exception cref="Exception">French IBANs must have 27 characters</exception>
        internal IBANDto ParseIbanFromString(string ibanAsString)
        {
            var space = " ";

            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 24)
                throw new Exception("Andorran IBANs must have 24 characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Andorran BankCode may contain only numbers.");

            ibanDto.BranchCode = ibanAsString.Substring(10, 4);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Andorran BranchCode may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(12, 12);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "AD" + space + checksum + space +
                                         ibanDto.BankCode + space + ibanDto.BranchCode
                                         + space + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
