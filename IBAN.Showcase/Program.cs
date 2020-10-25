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

// using Iban = IBANEU.Lib.Core.IBAN;


namespace IBANEU.Showcase
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // DE 89 37040044 053201300
            var germanIban = "CH 93 00762 011623852957";

            var iban = new IBAN(germanIban);

            Console.ReadKey();
        }
    }
}
