using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using IBANEU.Lib.Helper;
using System;

namespace IBANEU.Lib.Customizations
{
    internal class Luxembourg
    {
        public IBANDto ParseIbanFromString(string ibanAsString)
        {
            var space = " ";
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 20)
                throw new Exception("Luxembourgish IBANs must have 20 characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.CountryCode = "LU";
            ibanDto.Country = CountryHelper.GetCountryFromCode(ibanDto.CountryCode);

            var checksum = ibanAsString.Substring(2, 2);

            if (!checksum.ContainsOnlyNumbers())
                throw new Exception("Luxembourgisch Checksums may contain only numbers.");

            ibanDto.BankCode = ibanAsString.Substring(4, 3);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Luxembourgisch Bankcode may contain only numbers.");

            ibanDto.BranchCode = string.Empty;

            ibanDto.AccountNumber = ibanAsString.Substring(7, 13);


            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = ibanDto.CountryCode + space + checksum + space + ibanDto.BankCode
                                         + space + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
