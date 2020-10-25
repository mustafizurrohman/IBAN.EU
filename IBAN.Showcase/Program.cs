// ***********************************************************************
// Assembly         : IBAN.Showcase
// Author           : Mustafizur Rohman
// Created          : 10-24-2020
//
// Last Modified By : Mustafizur Rohman
// Last Modified On : 10-25-2020
// ***********************************************************************
// <copyright file="Program.cs" company="IBAN.Showcase">
//     Copyright (c) Personal. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using IBANEU.Lib.Core;
using System;
using System.Collections.Generic;

namespace IBANEU.Showcase
{
    /// <summary>
    /// Class Program.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            List<string> IBANList = new List<string>()
            {
                "IT97V0300203280544573814611",
                "FR5317569000506249263175I27",
                "FR5317569000506249263175I27",
                "DE30500105176873617729",
                "DE77500105175734242273"
            };


            foreach (var ibanString in IBANList)
            {
                var iban = new IBAN(ibanString);
                PrintIBAN(iban);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Prints the iban.
        /// </summary>
        /// <param name="iban">The iban.</param>
        private static void PrintIBAN(IBAN iban)
        {
            Console.WriteLine("Country          : " + iban.Country);
            Console.WriteLine("CountryCode      : " + iban.CountryCode);
            Console.WriteLine("BankCode         : " + iban.BankCode);
            Console.WriteLine("BranchCode       : " + iban.BranchCode);
            Console.WriteLine("AccountNumber    : " + iban.AccountNumber);
            Console.WriteLine("Formatted string : " + iban.AsStringWithSpaces);
            Console.WriteLine();
            Console.WriteLine();

        }



    }
}
