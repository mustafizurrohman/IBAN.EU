using IBANEU.Lib.Core;
using IBANEU.Lib.ExtensionMethods;
using System;

namespace IBANEU.Lib.Customizations
{
    internal class Switzerland : CustomizationBase
    {

        internal override int IBANLength => 21;

        internal override IBANDto ParseIbanFromString(string ibanAsString)
        {
            ibanAsString = ibanAsString.RemoveSpaces();

            if (ibanAsString.Length != 21)
                throw new Exception($"Swiss IBANs must have {IBANLength} characters");

            // ReSharper disable once UseObjectOrCollectionInitializer
            IBANDto ibanDto = new IBANDto();

            ibanDto.AssignCountryAndCode(ibanAsString);

            ibanDto.BranchCode = ibanAsString.Substring(4, 5);
            ibanDto.BankCode = ibanDto.BranchCode;

            if (!ibanDto.BankCode.ContainsOnlyNumbers())
                throw new Exception("Swiss Bank codes may contain only numbers.");

            ibanDto.AccountNumber = ibanAsString.Substring(9, 12);

            if (!ibanDto.AccountNumber.ContainsOnlyNumbers())
                throw new Exception("Swiss Account numbers may contain only numbers.");

            var checkSum = ibanAsString.Substring(2, 2);

            ibanDto.AsString = ibanAsString;
            ibanDto.AsStringWithSpaces = "CH" + " " + checkSum + " " + ibanDto.BranchCode + " " + ibanDto.AccountNumber;

            return ibanDto;
        }
    }
}
