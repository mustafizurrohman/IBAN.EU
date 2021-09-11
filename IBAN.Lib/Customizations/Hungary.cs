// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 09-11-2021
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 09-11-2021
// ***********************************************************************
// <copyright file="Hungary.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBANEU.Lib.Customizations
{
    /// <summary>
    /// Class Hungary.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    internal class Hungary : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 28;
        
        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public override string CountryCode => "HU";
        
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
                throw new Exception($"German IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BankCode = ibanAsString.Substring(4, 3);
            
            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Hungarian Bank Codes may contain only numbers.");

            ibanDto.BranchCode = ibanAsString.Substring(7, 4);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Hungarian Branch Codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(11, 16);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Hungarian Account numbers may contain only numbers.");

            var checkSum = ibanAsString.Substring(2, 2);
            var checkDigit = ibanAsString.Last().ToString();

            ibanDto.AsString = ibanAsString;

            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkSum, ibanDto.BankCode, ibanDto.BranchCode, ibanDto.AccountNumber, checkDigit
            });

            return ibanDto;
        }
    }
}
