// ***********************************************************************
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
using IBANEU.Lib.Helper;
using System;

namespace IBANEU.Lib.Customizations
{
    /// <summary>
    /// Class Albania.
    /// </summary>
    internal class Albania
    {
        internal IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 28)
                throw new Exception("Albanian Islands must have 28 characters.");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.CountryCode = CountryHelper.GetCountryCode(ibanAsString);
            ibanDto.Country = CountryHelper.GetCountryFromIBANString(ibanAsString);

            return new IBANDto();
        }
    }

}
