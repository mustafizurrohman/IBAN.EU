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
using System;

namespace IBANEU.Lib.Customizations
{
    /// <summary>
    /// Class Germany.
    /// Implements the <see cref="IBANEU.Lib.Customizations.ICustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.ICustomizationBase" />
    public class Germany
    {
        internal IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.Replace(" ", string.Empty);

            if (ibanAsString.Length != 22)
                throw new Exception("German IBANs must have atleast 22 characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.Country = "Germany";
            ibanDto.CountryCode = "DE";

            ibanDto.BLZ = ibanAsString.Substring(4, 8);
            ibanDto.BankCode = ibanDto.BLZ;

            ibanDto.AccountNumber = ibanAsString.Substring(12, 10);

            var checkSum = ibanAsString.Substring(2, 2);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "DE" + " " + checkSum + " " + ibanDto.BLZ + " " + ibanDto.AccountNumber;

            return ibanDto;
        }


    }
}
