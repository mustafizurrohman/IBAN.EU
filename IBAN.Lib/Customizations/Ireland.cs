// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 09-11-2021
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 09-11-2021
// ***********************************************************************
// <copyright file="Ireland.cs" company="IBANEU.Lib">
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
    /// Class Ireland.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Ireland : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 22;
        
        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "IE";
        
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
                throw new Exception($"Irish IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checkSum = ibanAsString.Substring(2, 2);

            if (!checkSum.ContainsOnlyNumbers())
                throw new Exception("Irish IBAN checksums may contain only digits.");

            var swiftCode = ibanAsString.Substring(4, 4);

            if (!swiftCode.ContainsOnlyCharacters())
                throw new Exception("Irish IBAN Swift code may contain only characters");

            var sortCode = ibanAsString.Substring(8, 6);

            if (!swiftCode.ContainsOnlyCharacters())
                throw new Exception("Irish IBAN Sort code may contain only characters");

            ibanDto.AccountNumber = ibanAsString.Substring(14, 8);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Irish Account Numbers may contain only digits.");

            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkSum, swiftCode, sortCode, ibanDto.AccountNumber
            });

            return ibanDto;
        }
    }
}
