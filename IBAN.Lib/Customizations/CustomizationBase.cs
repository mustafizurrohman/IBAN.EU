// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-31-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-31-2020
// ***********************************************************************
// <copyright file="CustomizationBase.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using IBANEU.Lib.Core;

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
        internal abstract int IBANLength { get; }

        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBANEU.Lib.Core.IBANDto.</returns>
        // ReSharper disable once MemberCanBeProtected.Global
        internal abstract IBANDto ParseIbanFromString(string ibanAsString);

        internal string Space = " ";
    }
}
