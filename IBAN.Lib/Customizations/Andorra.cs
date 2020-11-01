// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-31-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="Andorra.cs" company="IBANEU.Lib">
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
    /// Class Andorra.
    /// </summary>
    internal class Andorra : CustomizationBase
    {

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected override int IBANLength => 24;

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANDto.</returns>
        /// <exception cref="Exception">Andorran IBANs must have {IBANLength} characters</exception>
        /// <exception cref="Exception">Andorran BankCode may contain only numbers.</exception>
        /// <exception cref="Exception">Andorran BranchCode may contain only numbers.</exception>
        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {

            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Andorran IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Andorran BankCode may contain only numbers.");

            ibanDto.BranchCode = ibanAsString.Substring(10, 4);

            if (!ibanDto.BranchCode.ContainsOnlyNumbers())
                throw new Exception("Andorran BranchCode may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(12, 12);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "AD" + Space + checksum + Space +
                                         ibanDto.BankCode + Space + ibanDto.BranchCode
                                         + Space + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
