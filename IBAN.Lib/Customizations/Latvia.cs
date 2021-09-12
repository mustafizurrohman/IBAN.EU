// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 09-12-2021
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 09-12-2021
// ***********************************************************************
// <copyright file="Latvia.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace IBANEU.Lib.Customizations
{

    /// <summary>
    /// Class Latvia.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Latvia : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 21;
        
        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "LV";
        
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
                throw new Exception($"Latvian IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checkSum = ibanAsString.Substring(2, 2);

            if (!checkSum.ContainsOnlyNumbers())
                throw new Exception("Latvian IBAN checksums may contain only digits.");

            var swiftOrBic = ibanAsString.Substring(4, 4);

            if (!swiftOrBic.ContainsOnlyCharacters())
                throw new Exception("v BankCodes may contain only characters.");

            ibanDto.AccountNumber = ibanAsString.Substring(8, 13);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Latvian AccountNumber may contain only digits.");

            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkSum, swiftOrBic, ibanDto.AccountNumber
            });

            return ibanDto;
        }
    }
}
