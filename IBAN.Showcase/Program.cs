﻿// ***********************************************************************
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
using IBANEU.Lib.ExtensionMethods;
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
                "AD 12 0001 2030 200359100100"
            };



            foreach (var ibanString in IBANList)
            {
                // _ =
                IBAN.TryParse(ibanString, out IBAN ibanResult);
                Console.WriteLine(ibanResult);
                ibanResult.PrintToConsole();
            }

            Console.ReadKey();
        }





    }
}
