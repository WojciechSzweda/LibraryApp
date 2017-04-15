using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Configuration;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLoginOK_Click(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                if (!Login.TryLogin(tbLogin.Text, tbPassword.Password))
                {
                    this.Close();
                }
            }
        }

        private void btnLoginCancel_Click(object sender, RoutedEventArgs e)
        {
            RaiseCustomEvent(this, new CustomEventArgs());
        }

        private void tbPassword_GotMouseCapture(object sender, MouseEventArgs e)
        {
            tbPassword.SelectAll();
        }

        private void RememberMe(string login, string password)
        {

        }


    }

    public class CustomEventArgs : EventArgs
    {
        public string MessageText { get; set; }
        public string Caption { get; set; }
    }
}
