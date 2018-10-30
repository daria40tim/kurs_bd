using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
    public class LoginViewModel: DependencyObject
    {
        static public event Action<bool> OnClose;

        public LoginViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            OKCommand = new BaseCommand(ConfirmEnter);
        }

        private void ConfirmEnter()
        {
            MainWindow new_MainWindow = new MainWindow();
            new_MainWindow.Show();
            //MessageBox.Show("1");
        }

        public BaseCommand OKCommand
        {
            get { return (BaseCommand)GetValue(OKCommandProperty); }
            set { SetValue(OKCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OKCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OKCommandProperty =
            DependencyProperty.Register("OKCommand", typeof(BaseCommand), typeof(LoginViewModel), new PropertyMetadata(null));


        public string UserPassword
        {
            get { return (string)GetValue(UserPasswordProperty); }
            set { SetValue(UserPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserPasswordProperty =
            DependencyProperty.Register("UserPassword", typeof(string), typeof(LoginViewModel), new PropertyMetadata(null));


        public string UserLogin
        {
            get { return (string)GetValue(UserLoginProperty); }
            set { SetValue(UserLoginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserLoginProperty =
            DependencyProperty.Register("UserLogin", typeof(string), typeof(LoginViewModel), new PropertyMetadata(null));


    }
}
