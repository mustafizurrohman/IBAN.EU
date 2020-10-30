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
                "DE73500105175851456951",
                "DE28500105173875983891",
                "IT93W0300203280218279291256",
                "IT93W0300203280218279291256",
                "FR0517569000705155569433Z66",
                "  FR0917569000506234196834A60",
                " LU740102566746143857 ",
                "   LU650105818714497199   ",
                "ES3821001145312151912217",
                "ES4620803394737891248453",
                "CH8589144863123213561",
                "CH9489144997985576188",
                "CH9489144992985576288"

            };



            foreach (var ibanString in IBANList)
            {
                var parseResult = IBAN.TryParse(ibanString, out IBAN ibanResult);

                Console.WriteLine(parseResult);
                Console.WriteLine(ibanResult);
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

            if (!string.IsNullOrWhiteSpace(iban.BranchCode))
                Console.WriteLine("BranchCode       : " + iban.BranchCode);

            Console.WriteLine("AccountNumber    : " + iban.AccountNumber);
            Console.WriteLine("Formatted string : " + iban);
            Console.WriteLine();
            Console.WriteLine();

        }



    }
}
