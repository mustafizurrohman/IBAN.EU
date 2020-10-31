// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-25-2020
// ***********************************************************************
// <copyright file="Germany.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;

namespace IBANEU.Lib.Customizations
{

    internal class Germany : CustomizationBase
    {
        internal override int IBANLength => 22;

        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 22)
                throw new Exception($"German IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BranchCode = ibanAsString.Substring(4, 8);
            ibanDto.BankCode = ibanDto.BranchCode;

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("German Bank Codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(12, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("German Account numbers may contain only numbers.");

            var checkSum = ibanAsString.Substring(2, 2);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "DE" + " " + checkSum + " " + ibanDto.BranchCode + " " + ibanDto.AccountNumber;

            return ibanDto;
        }


    }
}
