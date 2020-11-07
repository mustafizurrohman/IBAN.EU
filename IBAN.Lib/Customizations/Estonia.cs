// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 11-07-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-07-2020
// ***********************************************************************
// <copyright file="Estonia.cs" company="IBANEU.Lib">
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
    /// Class Estonia.
    /// Implements the <see cref="IBANEU.Lib.Customizations.CustomizationBase" />
    /// </summary>
    /// <seealso cref="IBANEU.Lib.Customizations.CustomizationBase" />
    class Estonia : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 20;

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

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.BankCode = ibanAsString.Substring(4, 2);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Estonian Bank Codes may contain only numbers");

            ibanDto.BranchCode = ibanAsString.Substring(6, 2);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Estonian Branch Codes may contain only numbers");

            ibanDto.AccountNumber = ibanAsString.Substring(8, 12);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Estonian Account Numbers may contain only numbers");

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "EE" + Space + checksum + Space + ibanDto.BankCode + Space + ibanDto.BranchCode
                                         + Space + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
