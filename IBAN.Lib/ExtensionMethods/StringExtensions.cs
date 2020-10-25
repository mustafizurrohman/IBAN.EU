// ***********************************************************************
// Assembly         : IBAN.Lib
// Author           : Mustafizur Rohman
// Created          : 10-24-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-24-2020
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="IBAN.Lib">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Linq;

namespace IBANEU.Lib.ExtensionMethods
{

    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether [contains only numbers] [the specified input string].
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns><c>true</c> if [contains only numbers] [the specified input string]; otherwise, <c>false</c>.</returns>
        public static bool ContainsOnlyNumbers(this string inputString)
        {
            return inputString.All(char.IsDigit);
        }

        /// <summary>
        /// Determines whether [contains special characters] [the specified input string].
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns><c>true</c> if [contains special characters] [the specified input string]; otherwise, <c>false</c>.</returns>
        public static bool ContainsSpecialCharacters(this string inputString)
        {
            return !inputString.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Determines whether [contains only characters] [the specified input string].
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns><c>true</c> if [contains only characters] [the specified input string]; otherwise, <c>false</c>.</returns>
        public static bool ContainsOnlyCharacters(this string inputString)
        {
            return inputString.All(char.IsLetter);
        }

    }
}
