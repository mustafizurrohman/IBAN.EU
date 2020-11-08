// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-31-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 11-08-2020
// ***********************************************************************
// <copyright file="CustomizationBase.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBANEU.Lib.Customizations
{
    /// <summary>
    /// Class CustomizationBase.
    /// </summary>
    internal abstract class CustomizationBase
    {
        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        protected abstract int IBANLength { get; }

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        internal abstract IBANDto ParseIbanFromString(string ibanAsString);

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        protected abstract string CountryCode { get; }

        /// <summary>
        /// The space
        /// </summary>
        internal string Space = " ";

        /// <summary>
        /// The maximum length
        /// </summary>
        private readonly int MaxLength = 35;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizationBase" /> class.
        /// </summary>
        /// <exception cref="Exception">Length of IBAN cannot be more than {MaxLength}</exception>
        internal CustomizationBase()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            if (IBANLength > MaxLength)
            {
                throw new Exception($"Length of IBAN cannot be more than {MaxLength}");
            }
        }

        /// <summary>
        /// Formats the iban string.
        /// </summary>
        /// <param name="parts">The parts.</param>
        /// <returns>System.String.</returns>
        internal string FormatIBANString(IEnumerable<string> parts)
        {
            return CountryCode + Space + parts.Aggregate((a, b) => a + Space + b);
        }

    }
}
