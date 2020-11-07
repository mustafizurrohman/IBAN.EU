using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;

namespace IBANEU.Lib.Customizations
{
    internal class Denmark : CustomizationBase
    {
        protected override int IBANLength => 18;

        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != IBANLength)
                throw new Exception($"German IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            var checksum = ibanAsString.Substring(2, 2);

            ibanDto.BranchCode = string.Empty;

            ibanDto.BankCode = ibanAsString.Substring(4, 4);

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Danish Bank Codes may contain only numbers");

            ibanDto.AccountNumber = ibanAsString.Substring(8, 10);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Danish Account Numbers may cotain only numbers");

            ibanDto.AsString = ibanAsString;

            ibanDto.AsStringWithSpaces =
                "DK" + Space + checksum + Space + ibanDto.BankCode + Space + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
