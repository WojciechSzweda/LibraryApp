using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        LoginViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            viewModel = new LoginViewModel() { ErrorVisibility = false };
            DataContext = viewModel;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.666);
            timer.Tick += WaitAnimation;

        }
        DispatcherTimer timer;
        RotateTransform rotation = new RotateTransform();
        private void btnLoginOK_Click(object sender, RoutedEventArgs e)
        {
            //using (new WaitCursor())
            {
                Task.Run(() =>
                {
                    timer.Start();
                    if (Login.TryLogin(Dispatcher.Invoke(() => tbLogin.Text), Dispatcher.Invoke(() => tbPassword.Password)))
                    {
                        timer.Stop();
                        Dispatcher.Invoke(() => this.Close());
                    }
                    else
                    {
                        timer.Stop();
                        //Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => lblLoginError.Visibility = Visibility.Visible));
                        //Dispatcher.Invoke(() => lblLoginError.Visibility = Visibility.Visible);
                        //Dispatcher.Invoke(() => viewModel.ErrorVisibility = Visibility.Visible);
                        //Dispatcher.Invoke(() => (this.DataContext as LoginViewModel).ErrorVisibility = Visibility.Visible);
                        viewModel.ErrorVisibility = true;
                    }

                });
            }
        }


        private void WaitAnimation(object sender, EventArgs e)
        {
            rotation.Angle += 10;

            Dispatcher.Invoke(() => WaitSpyro.RenderTransform = rotation);

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

        private void tb_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            viewModel.ErrorVisibility = false;
        }

        private void tbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.ErrorVisibility = false;
        }
    }

    public class CustomEventArgs : EventArgs
    {
        public string MessageText { get; set; }
        public string Caption { get; set; }
    }
}
