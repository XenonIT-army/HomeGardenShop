using System;
using System.Linq;
using HomeGardenShop.Helps.Validation;
using HomeGardenShop.Models;
using Plugin.ValidationRules.Formatters;
using Prism.Commands;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace HomeGardenShop.ViewModels.DialogViewModels
{
	public class EditUserInfoViewModel : ViewModelBase
    {
        private DelegateCommand _saveUserInfoCommand;
        private DelegateCommand _closeCommand;
        private GenericAdressVerifier verifier = new GenericAdressVerifier();
        private DelegateCommand _validatePhoneCommand;
        private DelegateCommand _validateMailCommand;
        private DelegateCommand _validateNameCommand;
        private DelegateCommand _validateAddressCommand;
        private PopupPage _page;
        private User _userEdit = new User();
        private bool _isStart;
        private string _errorPhone;
        private string _errorMail;
        private string _errorName;
        private string _errorAddress;

        public EditUserInfoViewModel(User user, PopupPage page)
        {
            _page = page;

            PhoneCountry = new string[]
            {
                "+380",
                "+7",
                "+370",
                "+371",
                "+372",
                "+373",
                "+374",
                "+375",
                "+381",
                "+359",
                "+40",
                "+44",
                "+48",
                "+420",
                "+90",
                "+992",
                "+993",
                "+994",
                "+995",
                "+996",
                "+998",
                "+1",
            };

            if (user != null)
            {
                UserEdit.SetValue(user);
                if (UserEdit.CodePhone != "")
                {
                    int index = 0;
                    foreach (var item in PhoneCountry)
                    {
                        if (item == UserEdit.CodePhone)
                        {
                            SelectedIndex = index;
                            break;
                        }
                        index++;
                    }
                }
                else
                {
                    UserEdit.Phone.Formatter = new MaskFormatter("XX XXX-XX-XX");
                }
            }
            _isStart = true;
        }
        public string ErrorPhone
        {
            get => _errorPhone;
            set
            {
                SetProperty(ref _errorPhone, value);
            }
        }

        public string ErrorMail
        {
            get => _errorMail;
            set
            {
                SetProperty(ref _errorMail, value);
            }
        }
        public string ErrorName
        {
            get => _errorName;
            set
            {
                SetProperty(ref _errorName, value);
            }
        }

        public string ErrorAddress
        {
            get => _errorAddress;
            set
            {
                SetProperty(ref _errorAddress, value);
            }
        }
        
        public User UserEdit
        {
            get
            {

                return _userEdit;
            }
            set
            {
                if (_userEdit != value)
                {
                     SetProperty(ref _userEdit, value);
                }
            }
        }
        private string[] _phoneCountry;
        public string[] PhoneCountry
        {
            get => _phoneCountry;
            set
            {
                SetProperty(ref _phoneCountry, value);
            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex != value)
                {
                    if (_isStart)
                    {
                        SetPhoneFormat(value);
                        UserEdit.Phone.Value = "";
                    }
                    SetProperty(ref _selectedIndex, value);
                }
            }
        }
        public DelegateCommand SaveUserInfoCommand =>
       _saveUserInfoCommand ?? (_saveUserInfoCommand = new DelegateCommand(async () =>
       {
           string _error;
           string _error2;

           UserEdit.CodePhone = PhoneCountry.ElementAt(SelectedIndex);
           bool isPhone = verifier.VerifyPhone($"{UserEdit.CodePhone} {UserEdit.Phone.Value}", out _error);


           bool isMail = verifier.IsValidEmail(UserEdit.Mail, out _error2);
           bool isNameEmpty = string.IsNullOrEmpty(UserEdit.Name);
           if (isNameEmpty)
           {
               ErrorName = "Empty data";
           }
           else
           {
               ErrorName = "";
           }
           bool isAddressEmpty = string.IsNullOrEmpty(UserEdit.Address);
           if (isAddressEmpty)
           {
               ErrorAddress = "Empty data";
           }
           else
           {
               ErrorAddress = "";
           }
           ErrorPhone = _error;
           ErrorMail = _error2;

           if (isPhone && isMail && !isNameEmpty && !isAddressEmpty)
           {
               // DBManager.UserDb.UserInfoUpdate(AccountEdit);
               App.AppModel.User.SetValue(UserEdit);
               bool res = await App.GreeterService.RegistrUser(App.AppModel.User);
               if (res)
               {
                   await PopupNavigation.Instance.RemovePageAsync(_page);
                 
               }
           }
           //else
           //{
           //    await PageDialogService.DisplayAlertAsync("Message",
           //                                                          "Данные не заполнены", "Ok");
           //}


       }));
        public DelegateCommand CloseCommand =>
       _closeCommand ?? (_closeCommand = new DelegateCommand(async() =>
       {
         
           await PopupNavigation.Instance.RemovePageAsync(_page);
       }));

        
        public DelegateCommand ValidatePhoneCommand =>
        _validatePhoneCommand ?? (_validatePhoneCommand = new DelegateCommand(() =>
        {

            UserEdit.Phone.Validate();
            ErrorPhone = UserEdit.Phone.Error;
        }));
        public DelegateCommand ValidateAddressCommand =>
      _validateAddressCommand ?? (_validateAddressCommand = new DelegateCommand(() =>
      {

          if (string.IsNullOrEmpty(UserEdit.Address))
          {
              ErrorAddress = "Empty data";
          }
      }));
        public DelegateCommand ValidateMailCommand =>
         _validateMailCommand ?? (_validateMailCommand = new DelegateCommand(() =>
         {

             if (string.IsNullOrEmpty(UserEdit.Mail))
             {
                 ErrorMail = "Empty data";
             }
         }));
        public DelegateCommand ValidateNameCommand =>
         _validateNameCommand ?? (_validateNameCommand = new DelegateCommand(() =>
         {
             if (string.IsNullOrEmpty(UserEdit.Name))
             {
                 ErrorName = "Empty data";
             }
         }));

        private void SetPhoneFormat(int index)
        {
            if (index == 0)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XXX-XX-XX");
            }
            if (index == 1)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("(XXX) XXX-XX-XX");
            }
            if (index == 2)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXXXXXXX");
            }
            if (index == 3)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XXX XXX");
            }
            if (index == 4)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXXX XXXXXXXX");
            }
            if (index == 5)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XXXXX");
            }
            if (index == 6)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XX-XX-XX");
            }
            if (index == 7)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XXX-XX-XX");
            }
            if (index == 8)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XXX XXXX");
            }
            if (index == 9)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XXX XXXX");
            }
            if (index == 10)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XXX XXX");
            }
            if (index == 11)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXXXXXXXXX");
            }
            if (index == 12)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX-XXX-XXX");
            }
            if (index == 13)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX-XXX-XXX");
            }
            if (index == 14)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XXX XXXX");
            }
            if (index == 15)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XX-XX-XX");
            }
            if (index == 16)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XXXXXX");
            }
            if (index == 17)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XX XXXXXXX");
            }
            if (index == 18)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XX-XX-XX");
            }
            if (index == 19)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XX-XX-XX");
            }
            if (index == 20)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XX-XX-XX");
            }
            if (index == 21)
            {
                UserEdit.Phone.Formatter = new MaskFormatter("XXX XX-XX-XXX");
            }


        }
    }
}

