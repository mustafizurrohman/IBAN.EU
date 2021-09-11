// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 09-11-2021
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 09-11-2021
// ***********************************************************************
// <copyright file="Greenland.cs" company="IBANEU.Lib">
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
    /// Class Greenland.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Greenland : CustomizationBase
    {
        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 18;

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "GL";
        
        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Greenlandic IBANs must have {IBANLength} characters.");

            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Greek Bank codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(8, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Greek Branch codes may contain only numbers.");
            
            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.AsString = ibanAsString;

            ibanDto.AsStringWithSpaces = FormatIBANString(ibanDto, checksum);

            return ibanDto;
        }
    }
}