// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-25-2020
// ***********************************************************************
// <copyright file="France.cs" company="IBANEU.Lib">
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
    /// Class France.
    /// </summary>
    internal class France
    {
        internal IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 27)
                throw new Exception("French IBANs must have 27 characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BranchCode = ibanAsString.Substring(9, 5);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("French Branch codes may contain only numbers.");

            ibanDto.BankCode = ibanAsString.Substring(4, 5);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("French Bank codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(14, 11);

            var checkSum = ibanAsString.Substring(2, 2);

            var checkDigits = ibanAsString.Substring(25, 2);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "FR" + " " + checkSum + " " + ibanDto.BankCode + " " + ibanDto.BranchCode + " " + ibanDto.AccountNumber + " " + checkDigits;

            return ibanDto;
        }
    }

}
