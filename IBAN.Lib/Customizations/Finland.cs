using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace IBANEU.Lib.Customizations
{
    internal class Finland : CustomizationBase
    {
        protected override int IBANLength => 18;

        protected override string CountryCode => "FI";

        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"French IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checkSum = ibanAsString.Substring(2, 2);

            if (!checkSum.ContainsOnlyNumbers())
                throw new Exception("Finnish IBAN Checksums may contain only numbers");

            ibanDto.BankCode = ibanAsString.Substring(4, 6);
            ibanDto.BranchCode = ibanDto.BankCode;

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Finnish Bank and Branch codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(10, 8);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Finnish Account Numbers may contain only numbers.");

            ibanDto.AsStringWithSpaces = FormatIBANString(new List<string>
            {
                checkSum, ibanDto.BankCode, ibanDto.AccountNumber
            });


            return ibanDto;

        }


    }
}
