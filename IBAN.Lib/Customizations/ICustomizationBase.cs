// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-25-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-25-2020
// ***********************************************************************
// <copyright file="ICustomizationBase.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using IBANEU.Lib.Core;

namespace IBANEU.Lib.Customizations
{
    /// <summary>
    /// Interface ICustomizationBase
    /// </summary>
    internal interface ICustomizationBase
    {
        /// <summary>
        /// Parses the iban from string.
        /// </summary>
        /// <param name="ibanAsString">The iban as string.</param>
        /// <returns>IBAN.</returns>
        IBANDto ParseIbanFromString(string ibanAsString);
    }
}