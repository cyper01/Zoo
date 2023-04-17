using Zoo.View;
using Zoo.ZooShopDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zoo.ViewModel
{
    
    public class AuthVM : BaseVm
    {
        private User _user;
        public string _LoginValue;
        public string _password;
        private string _BtnVhod = "войти";

        #region присвоение значений
        public string LoginValue
        {
            get => _LoginValue;
            set
            {
                _LoginValue = value;
                OnpropertyChanged(nameof(LoginValue));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnpropertyChanged(nameof(Password));
            }
        }
        public string BtnVhod
        {
            get => _BtnVhod;
            set
            {
                _BtnVhod= value;
                OnpropertyChanged(nameof(BtnVhod));
            }
        }
        #endregion
    
    private async Task<bool> Authotize(string LoginValue, string Password)
        {
            try
            {
                var result = await DbStorage.Db_s.User.FirstOrDefaultAsync(User => User.UserPassword == Password && User.UserLogin == LoginValue);
                _user= result;
                if (result != null)
                {
                    MessageBox.Show("авторизация прошла успешно", "авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
                }
                else
                {
                    MessageBox.Show("неверный логин или пароль", "авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
               
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "необработанное исключение", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
        }

        public async void AuthInApp()
        {
            if (await Authotize(LoginValue, Password))
            {
                var dataWindow = new DataGridProduct();
                dataWindow.Show();
            }
        }
    }
}
