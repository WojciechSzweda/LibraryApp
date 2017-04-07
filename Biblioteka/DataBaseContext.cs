using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    static public class DataBaseContext
    {
        static private int GetCardID(TextBox tbNrCard, TextBlock tbError)
        {
            int cardNr;
            if (!int.TryParse(tbNrCard.Text, out cardNr))
            {
                tbError.Text = "type an ID";
                return 0;
            }
            tbError.Text = "";
            return cardNr;
        }

        static public void ShowRentedBooks(TextBox tbNrCard, TextBlock tbError, DataGrid dataGrid)
        {
            var cardNr = GetCardID(tbNrCard, tbError);
            if (cardNr == 0)
            {
                //TODO: ERROR info
                return;
            }

            using (var context = new LibraryEntities())
            {
                var query = from karta in context.Karta
                            join wyp in context.Wypożyczenie on karta.ID equals wyp.ID_Karta
                            join kopia in context.Kopia on wyp.ID_Kopia equals kopia.ID
                            join book in context.Książka on kopia.ID_Książka equals book.ID
                            join oddanie in context.Oddanie on wyp.ID equals oddanie.ID_wypożyczenie
                            where karta.ID == cardNr
                            select new { wyp.ID, wyp.Data_wypożyczenia, wyp.Oczekiwana_data_zwrotu, oddanie.Data_oddania, book.Tytuł };

                dataGrid.ItemsSource = query.ToList();
            }
        }


        static public void Registration(TextBlock tbError, TextBox tbFName, TextBox tbLName, TextBox tbPhoneNr, TextBox tbPCode, TextBox tbCity, TextBox tbHouseNr, TextBox tbStreet, TextBox tbApartNr )
        {
            //int newCardID;
            tbError.Text = "";
            try
            {
                using (var context = new LibraryEntities())
                {
                    var card = new Karta
                    {
                        Imię = tbFName.Text,
                        Nazwisko = tbLName.Text,
                        Nr_kontaktowy = tbPhoneNr.Text,
                        Miejscowość = tbCity.Text,
                        Kod_pocztowy = tbPCode.Text,
                        Ulica = tbStreet.Text,
                        Nr_domu = tbHouseNr.Text,
                        Nr_mieszkania = tbApartNr.Text,
                        Stan = 1
                    };
                    context.Karta.Add(card);
                    context.SaveChanges();
                    //newCardID = card.ID;
                    RegisterCompleted(card.ID); //TODO: set up event
                }
            }
            catch (Exception ex)
            {
                tbError.Text = ex.ToString();
            }
        }

        private static void RegisterCompleted(int id)
        {
            MessageBox.Show($"New ID: {id}", "Registration successful", MessageBoxButton.OK);
        }

        static public void ShowBookCopyInfo(TextBox tbCopyID, DataGrid dgCopyInfo)
        {
            int cardNr;
            if (int.TryParse(tbCopyID.Text, out cardNr)) { //TODO: method
                using (var context = new LibraryEntities())
                {
                    var copyInfo = from c in context.Kopia
                                   join b in context.Książka on c.ID_Książka equals b.ID
                                   where c.ID == cardNr
                                   select new { b.Tytuł, b.Rok_wydania, b.ISBN, c.Stan_Książki };

                    dgCopyInfo.ItemsSource = copyInfo.ToList();
            }
            }
        }
    }
}
