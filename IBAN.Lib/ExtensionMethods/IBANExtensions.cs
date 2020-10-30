// ***********************************************************************
// Assembly         : IBANEU.Lib
// Author           : Mustafizur Rohman
// Created          : 10-30-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-30-2020
// ***********************************************************************
// <copyright file="IBANExtensions.cs" company="IBANEU.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;

namespace IBANEU.Lib.ExtensionMethods
{
    /// <summary>
    /// Class IBANExtensions.
    /// </summary>
    public static class IBANExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IBANString"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static bool IsValidIBAN(this string IBANString)
        {
            try
            {
                _ = new IBAN(IBANString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Converts to formattedstring.
        /// </summary>
        /// <param name="IBANString">The IBAN string.</param>
        /// <returns>System.String.</returns>
        // ReSharper disable once InconsistentNaming
        public static string ToFormattedIBANString(this string IBANString)
        {
            return new IBAN(IBANString).ToString();
        }
    }
}
