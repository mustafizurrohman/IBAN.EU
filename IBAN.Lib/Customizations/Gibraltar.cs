using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace IBANEU.Lib.Customizations
{
    internal class Gibraltar : CustomizationBase
    {
        protected override int IBANLength => 23;
        protected override string CountryCode => "GI";

        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"Gibraltarian IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checkDigits = ibanAsString.Substring(2, 2);

            if (!checkDigits.ContainsOnlyNumbers())
                throw new Exception("Check digits in Gibraltarian IBANs may contain only numbers");

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            if (!ibanDto.BankCode.ContainsOnlyCharacters())
                throw new Exception("Bank codess in Gibraltarian IBANs may contain only characters");

            ibanDto.AccountNumber = ibanAsString.Substring(8, 15);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Account Numbers in Gibraltarian IBANs may contain only numbers");


            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkDigits,
                ibanDto.BankCode,
                ibanDto.AccountNumber
            });

            return ibanDto;

        }
    }
}
