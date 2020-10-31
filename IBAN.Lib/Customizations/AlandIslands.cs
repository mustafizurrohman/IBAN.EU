// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-31-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="AlandIslands.cs" company="IBANEU.Lib">
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
    /// Class AlandIslands.
    /// </summary>
    internal class AlandIslands : CustomizationBase
    {
        internal override int IBANLength => 18;


        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANDto.</returns>
        /// <exception cref="Exception">IBANs from Aland Islands must have 18 characters</exception>
        /// <exception cref="Exception">Bank and Branch code form Aland Islands may contain only numbers.</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"IBANs from Aland Islands must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.BankCode = ibanAsString.Substring(4, 6);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Bank and Branch code form Aland Islands may contain only numbers.");

            ibanDto.BranchCode = ibanDto.BankCode;

            ibanDto.AccountNumber = ibanAsString.Substring(10, 8);

            var space = " ";

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "AX" + space + checksum + space
                                         + ibanDto.BankCode + space + ibanDto.AccountNumber;

            return ibanDto;
        }

    }
}
