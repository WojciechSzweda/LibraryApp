using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Regex regexNumbers = new Regex("[^0-9]+");
        Regex regexNumbersAndMinus = new Regex("[^0-9]+");

        LoginWindow loginWnd;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            new Settings().Show();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext.ShowRentedBooks(tbNrCard, tbError, dataGrid);
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            //int index = list.ElementAt(dataGrid.SelectedIndex).ID;
            //Return(index);
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
            DataBaseContext.Registration(tbRegisterError, tbFName, tbLName, tbPhoneNr, tbPCode, tbCity, tbHouseNr, tbStreet, tbApartNr);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginScreen();
        }

        private void LoginScreen()
        {
            loginWnd = new LoginWindow();
            loginWnd.RaiseCustomEvent += new EventHandler<CustomEventArgs>(Login_Canceled);
            loginWnd.ShowDialog();
            this.IsEnabled = true;

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

        #region oldsql
        //private void TestConnection()
        //{
        //    using (new WaitCursor())
        //    {
        //        try
        //        {
        //            conn.Open();
        //            conn.Close();
        //        }
        //        catch (SqlException e)
        //        {
        //            if (conn != null)
        //                conn.Dispose();
        //            tbError.Text = "Nie udało się połączyć z serwerem. Nieprawidłowa nazwa serwera lub bazy.";
        //            btnShow.IsEnabled = false;
        //            btnRegister.IsEnabled = false;
        //            return;
        //        }
        //        btnRegister.IsEnabled = true;
        //        btnShow.IsEnabled = true;
        //        tbError.Text = "Połączono!";
        //    }
        //}

        //private void RegisterCart()
        //{
        //    tbError2.Text = "";



        //    try
        //    {
        //        using (SqlCommand command = new SqlCommand(@"INSERT INTO Karta (Imię, Nazwisko, Miejscowość, [Kod pocztowy], Ulica, [Nr domu], [Nr mieszkania],[Nr kontaktowy]) 
        //                                                 OUTPUT INSERTED.ID VALUES (@imie, @nazwisko, @city, @kod, @ulica, @nrd, @nrm, @nrtel)", conn))
        //        {
        //            conn.Open();
        //            command.Parameters.AddWithValue("@imie", tbImię.Text);
        //            command.Parameters.AddWithValue("@nazwisko", tbNazwisko.Text);
        //            command.Parameters.AddWithValue("@city", tbCity.Text);
        //            command.Parameters.AddWithValue("@kod", tbPCode.Text);
        //            command.Parameters.AddWithValue("@ulica", tbUlica.Text);
        //            command.Parameters.AddWithValue("@nrd", tbNrDomu.Text);
        //            string nrM = tbNrMiesz.Text;
        //            if (nrM == "")
        //            {
        //                command.Parameters.AddWithValue("@nrm", DBNull.Value);
        //            }
        //            else
        //            {
        //                command.Parameters.AddWithValue("@nrm", nrM);
        //            }
        //            command.Parameters.AddWithValue("@nrtel", tbNrTel.Text);
        //            command.ExecuteScalar();
        //            //Int32 newId = (Int32)command.ExecuteScalar();
        //            //return newId;
        //            conn.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        tbError2.Text = e.Message;
        //    }


        //}

        //private int GetStan(int index)
        //{
        //    int stan = 0;
        //    var command = new SqlCommand();
        //    var query = @"SELECT [Stan Książki].ID
        //                  FROM Kopia
        //                  JOIN Wypożyczenie on Kopia.ID = Wypożyczenie.ID_Kopia
        //                  JOIN[Stan Książki] on Kopia.[Stan Książki] = [Stan Książki].ID
        //                  WHERE Wypożyczenie.ID = " + index;
        //    command.CommandText = query;
        //    command.Connection = conn;

        //    using (var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            stan = reader.GetInt32(0);
        //        }
        //    }
        //    return stan;
        //}

        //private int GetStanAfter(int stan)
        //{
        //    //DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex
        //    //     (dataGrid.SelectedIndex) as DataGridRow;
        //    //var i = dataGrid.Columns.Single(c => c.Header.ToString() == "Stan").DisplayIndex; /// Specify your column index here.
        //    //////EDIT
        //    //TextBox ele = ((ContentPresenter)(dataGrid.Columns[i].GetCellContent(row))).Content as TextBox;
        //    //if (ele.Text == "")
        //    //{
        //    //    ele.Text = stan.ToString();
        //    //}
        //    return rnd.Next(stan, 4);
        //}

        //private void Return(int index)
        //{
        //    string query = @"IF NOT EXISTS (SELECT * FROM Wypożyczenie 
        //      JOIN Oddanie on Wypożyczenie.ID = Oddanie.ID_wypożyczenie
        //      WHERE Wypożyczenie.ID = " + index + @")
        //                     INSERT INTO Oddanie([Stan przed oddaniem],[Stan po oddaniu], ID_wypożyczenie) values (@stanPre,@stanAfter,@wypID)";
        //    conn.Open();
        //    SqlCommand comm = new SqlCommand();
        //    comm.CommandText = query;
        //    comm.Connection = conn;
        //    int stan = GetStan(index);
        //    comm.Parameters.AddWithValue("stanPre", stan);
        //    comm.Parameters.AddWithValue("stanAfter", GetStanAfter(stan));
        //    comm.Parameters.AddWithValue("wypID", Convert.ToInt32(index));
        //    tbError.Text = index.ToString();
        //    var updated = comm.ExecuteNonQuery();
        //    if (updated > 0)
        //    {
        //        MessageBox.Show("Zaktualizowano!");
        //    }
        //    conn.Close();
        //    ShowQuery();
        //}

        //public void ShowQuery()
        //{
        //    tbError.Text = "";
        //    list = new List<Books>();
        //    var tbBrush = tbNrCard.BorderBrush;
        //    if (!int.TryParse(tbNrCard.Text, out nrKarty))
        //    {
        //        tbError.Text = "zle dane";

        //        tbNrCard.BorderBrush = Brushes.Red;
        //        return;
        //    }
        //    tbNrCard.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFABADB3");

        //    var commandRead = new SqlCommand(@"SELECT Wypożyczenie.ID, Wypożyczenie.[Data wypożyczenia], Wypożyczenie.[Oczekiwana data zwrotu], Oddanie.[Data oddania], Książka.Tytuł 
        //                                        FROM Karta 
        //                                        JOIN Wypożyczenie on Karta.ID = Wypożyczenie.ID_Karta 
        //                                        JOIN Kopia on Wypożyczenie.ID_Kopia = Kopia.ID 
        //                                        JOIN Książka on Kopia.ID_Książka = Książka.ID 
        //                                        FULL JOIN Oddanie on Wypożyczenie.ID = Oddanie.ID_wypożyczenie
        //                                        WHERE Karta.ID = " + nrKarty, conn);
        //    try
        //    {
        //        conn.Open();

        //        using (var reader = commandRead.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                object sqlDateTime = reader[3];
        //                DateTime? dt = (sqlDateTime == System.DBNull.Value)
        //                    ? (DateTime?)null
        //                    : Convert.ToDateTime(sqlDateTime);
        //                list.Add(new Books { ID = reader.GetInt32(0), Title = reader.GetString(4), DateRent = reader.GetDateTime(1), DateToReturn = reader.GetDateTime(2), DateReturn = dt });

        //            }
        //        }

        //        dataGrid.ItemsSource = list;
        //    }
        //    catch (Exception e)
        //    {
        //        tbError.Text = e.Message;
        //    }
        //    conn.Close();
        //}
        #endregion


    }
}
