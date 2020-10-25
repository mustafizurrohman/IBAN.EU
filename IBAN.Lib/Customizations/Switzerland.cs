using IBANEU.Lib.Core;
using System;

namespace IBANEU.Lib.Customizations
{
    internal class Switzerland
    {
        internal IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.Replace(" ", string.Empty);

            if (ibanAsString.Length != 21)
                throw new Exception("Swiss IBANs must have 21 characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.Country = "Switzerland";
            ibanDto.CountryCode = "CH";

            ibanDto.BLZ = ibanAsString.Substring(4, 5);
            ibanDto.BankCode = ibanDto.BLZ;

            ibanDto.AccountNumber = ibanAsString.Substring(9, 12);

            var checkSum = ibanAsString.Substring(2, 2);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "CH" + " " + checkSum + " " + ibanDto.BLZ + " " + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
