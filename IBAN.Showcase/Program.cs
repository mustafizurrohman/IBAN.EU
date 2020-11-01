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
                //"AX 21 123456 00000785",
                //"AL57134113217528637732263667",
                //"AL51214511261137674831354215",
                //"AD3286538925232727517527",
                //"AD5049399964224638447645",
                //"FR9817569000305544869843E32",
                //"FR3610096000704321729662T49",
                //"DE82500105176165735431",
                //"DE80500105177968364552",
                //"IT06G0300203280946811197846",
                //"IT23U0300203280287762594757",
                //"LU690103334317935973",
                //"LU420107983598849182",
                //"ES4514656743466943856983",
                //"ES6221001567195882969695",
                //"CH8189144628241783559",
                //"CH9289144925817269413",
                //"AT 61 19043 00234573201",
                //"AT435400071838797118"
                //"BY13NBRB3600900000002Z00AB00",
                //"BE34587884484990",
                // "BE98897561747993",
                // "BA 39 129 007 9401028494"
                "BG 80 BNBG 9661 1020345678"
            };



            foreach (var ibanString in IBANList)
            {
                _ = IBAN.TryParse(ibanString, out IBAN ibanResult);
                Console.WriteLine(ibanResult);
                ibanResult.PrintToConsole();
            }

            Console.ReadKey();
        }





    }
}
