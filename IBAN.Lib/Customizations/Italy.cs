// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-25-2020
// ***********************************************************************
// <copyright file="Italy.cs" company="IBANEU.Lib">
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
    /// Class Italy.
    /// </summary>
    internal class Italy : CustomizationBase
    {

        internal override int IBANLength => 27;

        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Italian IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);
            var checkChar = ibanAsString.Substring(4, 1);

            ibanDto.BankCode = ibanAsString.Substring(5, 5);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Italian Bank codes may contain only numbers.");

            ibanDto.BranchCode = ibanAsString.Substring(10, 5);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Italian Branch codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(15, 12);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "IT" + " " + checksum + " " + checkChar + " " + ibanDto.BankCode
                                         + " " + ibanDto.BranchCode + " " + ibanDto.AccountNumber;

            return ibanDto;

        }
    }
}
