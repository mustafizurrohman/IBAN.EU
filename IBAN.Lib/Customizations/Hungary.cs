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
using System;

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
            throw new NotImplementedException();
        }
    }
}
