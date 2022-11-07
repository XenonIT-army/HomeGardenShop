using System;
using PhoneNumbers;

namespace HomeGardenShop.Helps.Validation
{
    class GenericAdressVerifier
    {
        //private Regex _prhoneReg = new Regex("^\\+\\d{10,14}$", RegexOptions.Compiled, new TimeSpan(0, 0, 10));
        private PhoneNumberUtil _phoneUtil = PhoneNumberUtil.GetInstance();

        public bool VerifyPhone(string phone, out string error)
        {
            bool isVerify = false;
            PhoneNumber number;
            if (string.IsNullOrEmpty(phone))
            {
                error = "error";
                return false;
            }
            try
            {
                number = _phoneUtil.Parse(phone, "ZZ");
                if (_phoneUtil.GetNumberType(number) == PhoneNumberType.MOBILE)
                {
                    isVerify = true;
                    error = "";
                }
                else
                {
                    error = "error";
                    isVerify = false;
                }
            }
            catch (Exception ex)
            {
                isVerify = false;
                error = "error";
                //Translator.TranslatorInstance["New_Subscriptions_PhoneError"];
            }
            return isVerify;
        }
        public bool IsValidEmail(string email, out string error)
        {
            if (string.IsNullOrEmpty(email))
            {
                error = "error";
                return false;
            }
            if (email.Trim().EndsWith("."))
            {
                error = "error";
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                bool res = addr.Address == email;
                if (res)
                {
                    error = "";
                    return true;
                }
                else
                {
                    error = "error";
                    return false;
                }
            }
            catch
            {
                error = "error";
                return false;
            }
        }
    }
}

