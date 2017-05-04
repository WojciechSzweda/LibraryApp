using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        Regex regexNumbers = new Regex("[^0-9]+");
        Regex regexNumbersAndMinus = new Regex("[^0-9-]+");

        LoginWindow loginWnd;
        ViewModel viewModel;

        private event EventHandler<CustomEventArgs> ShowGraphEvent;

        public MainWindow()
        {
            InitializeComponent();
            DataBaseContext.ErrorMessageBoxEvent += new EventHandler<CustomEventArgs>(ShowErrorMessageBox);
            DataBaseContext.InfoMessageBoxEvent += new EventHandler<CustomEventArgs>(ShowInfoMessageBox);
            ShowGraphEvent += new EventHandler<CustomEventArgs>(ClearGraph);
            ShowGraphEvent += new EventHandler<CustomEventArgs>(ShowGraph);

            viewModel = new ViewModel();
            this.DataContext = viewModel;

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            var settingWnd = new Settings();
            settingWnd.MaximizeEvent += new EventHandler<CustomEventArgs>(SetWindowState);
            settingWnd.Show();
        }

        private void btnShowRentedBooks_Click(object sender, RoutedEventArgs e)
        {
            ShowRentedBooks();
        }

        private async void ShowRentedBooks()
        {
            await Task.Run(() =>
            {
                var RentedBooks = DataBaseContext.ShowRentedBooks(Dispatcher.Invoke(() => tbNrCard.Text));
                Dispatcher.Invoke(() =>
                {
                    dgRentedBooks.ItemsSource = RentedBooks;

                    if (RentedBooks == null)
                    {
                        ClearGraph(this, new CustomEventArgs());
                        return;
                    }

                    ShowGraphEvent?.Invoke(this, new CustomEventArgs());
                });
            });
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
                return;

            DataBaseContext.Return((int)(sender as Button).CommandParameter);
            ShowRentedBooks();
        }


        private void tbNrCard_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regexNumbers.IsMatch(e.Text);
        }

        private void tbPCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regexNumbersAndMinus.IsMatch(e.Text);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext.Registration(tbFName.Text, tbLName.Text, tbPhoneNr.Text, tbPCode.Text, tbCity.Text, tbStreet.Text, tbHouseNr.Text, tbApartNr.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowState(this, new CustomEventArgs());
            LoginScreen();

        }

        private void LoginScreen()
        {
            loginWnd = new LoginWindow();
            loginWnd.RaiseCustomEvent += new EventHandler<CustomEventArgs>(Login_Canceled);
            loginWnd.ShowDialog();
            this.IsEnabled = true;

        }

        void ShowInfoMessageBox(object sender, CustomEventArgs e)
        {
            MessageBox.Show(e.MessageText, e.Caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void ShowErrorMessageBox(object sender, CustomEventArgs e)
        {
            MessageBox.Show(e.MessageText, e.Caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }


        void Login_Canceled(object sender, CustomEventArgs e)
        {
            loginWnd.Close();
            this.Close();
        }


        private void Stan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regexNumbers.IsMatch(e.Text);
        }


        private void btnRentBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowCopyInfo_Click(object sender, RoutedEventArgs e)
        {
            dgShowCopyInfo.ItemsSource = DataBaseContext.ShowBookCopyInfo(tbBRcopyID.Text);
        }

        private void Maximize(object sender, CustomEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void SetWindowState(object sender, CustomEventArgs e)
        {
            if (Properties.Settings.Default.Fullscreen)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void ShowGraph(object sender, CustomEventArgs e)
        {
            Task.Run(() =>
            {
                var GraphData = DataBaseContext.GetDataForGraph(Dispatcher.Invoke(() => tbNrCard.Text));
                Dispatcher.Invoke(() => viewModel.Add(GraphData));
            });
        }

        private void ClearGraph(object sender, CustomEventArgs e)
        {
            viewModel.ValueList.Clear();
        }

        private void btnShowStatsGraph_Checked(object sender, RoutedEventArgs e)
        {
            //ShowGraphEvent += new EventHandler<CustomEventArgs>(ShowGraph);
            lineChart.Visibility = Visibility.Visible;
        }

        private void btnShowStatsGraph_Unchecked(object sender, RoutedEventArgs e)
        {
            //ShowGraphEvent -= new EventHandler<CustomEventArgs>(ShowGraph);
            lineChart.Visibility = Visibility.Collapsed;
        }
    }
}
