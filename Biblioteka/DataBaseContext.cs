using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteka
{
    static public class DataBaseContext
    {
        static public event EventHandler<CustomEventArgs> ErrorMessageBoxEvent;
        static public event EventHandler<CustomEventArgs> InfoMessageBoxEvent;

        static private int GetCardID(string tbNrCardText)
        {
            int cardNr;
            if (!int.TryParse(tbNrCardText, out cardNr))
            {
                ErrorMessageBoxEvent(null, new CustomEventArgs { MessageText = "Type an ID \n(number)", Caption = "Wrong ID" });
                return 0;
            }
            return cardNr;
        }


        static public List<GetRentedBooks_Result> ShowRentedBooks(string NrCardText)
        {
            var cardNr = GetCardID(NrCardText);
            if (cardNr == 0)
                return null;

            using (var context = new LibraryEntities())
            {
                return context.GetRentedBooks(cardNr).ToList();
            }
        }

        static public void Registration(string FirstName, string LastName, string PhoneNumber, string PostalCode, string City, string Street, string HouseNr, string ApartmentNr)
        {
            try
            {
                using (var context = new LibraryEntities())
                {
                    var card = new Karta
                    {
                        Imię = FirstName,
                        Nazwisko = LastName,
                        Nr_kontaktowy = PhoneNumber,
                        Miejscowość = City,
                        Kod_pocztowy = PostalCode,
                        Ulica = Street,
                        Nr_domu = HouseNr,
                        Nr_mieszkania = ApartmentNr,
                        Stan = 1
                    };

                    var fieldsList = UIChecker.RegisterFieldsDictionary(FirstName, LastName, PhoneNumber, PostalCode, City, Street, HouseNr);
                    if (UIChecker.CheckForEmptyFields(fieldsList))
                    {
                        ErrorMessageBoxEvent(null, new CustomEventArgs { MessageText = UIChecker.GetEmptyFieldsName(fieldsList) });
                        return;
                    }
                    context.Karta.Add(card);
                    context.SaveChanges();
                    InfoMessageBoxEvent(null, new CustomEventArgs { MessageText = $"Registration Successful\nNew ID: {card.ID}", Caption = "Success" });
                }
            }
            catch(Exception)
            {
                ErrorMessageBoxEvent(null, new CustomEventArgs { MessageText = "Unknown error occurred", Caption = "ERROR" });
            }
        }

        public class BookCopyInfo
        {
            public string Title { get; set; }
            public DateTime PublishDate { get; set; }
            public string ISBN { get; set; }
            public int BookCopyState { get; set; }
        }

        static public List<BookCopyInfo> ShowBookCopyInfo(string tbCopyID)
        {
            int copyID;
            if (int.TryParse(tbCopyID, out copyID))
            {
                using (var context = new LibraryEntities())
                {
                    var copyInfo = from copy in context.Kopia
                                   join book in context.Książka on copy.ID_Książka equals book.ID
                                   where copy.ID == copyID
                                   select new BookCopyInfo { Title = book.Tytuł, PublishDate = book.Rok_wydania, ISBN = book.ISBN, BookCopyState = copy.Stan_Książki };
                    return copyInfo.ToList();
                }
            }
            return null;
        }

        #region query old
        //public class RentedBooks
        //{
        //    public int ID { get; set; }
        //    public DateTime RentDate { get; set; }
        //    public DateTime RentUntil { get; set; }
        //    public DateTime? ReturnDate { get; set; }
        //    public string Title { get; set; }
        //}
        //static public List<RentedBooks> ShowRentedBooks(TextBox NrCard, TextBlock tbError)
        //{
        //    var cardNr = GetCardID(NrCard, tbError);
        //    if (cardNr == 0)
        //    {
        //        //TODO: ERROR info
        //        return null;
        //    }

        //    using (var context = new LibraryEntities())
        //    {
        //        //var query = from karta in context.Karta
        //        //            join wyp in context.Wypożyczenie on karta.ID equals wyp.ID_Karta
        //        //            join kopia in context.Kopia on wyp.ID_Kopia equals kopia.ID
        //        //            join book in context.Książka on kopia.ID_Książka equals book.ID
        //        //            join oddanie in context.Oddanie on wyp.ID equals oddanie.ID_wypożyczenie
        //        //            where karta.ID == cardNr
        //        //select new RentedBooks
        //        //{
        //        //    ID = wyp.ID,
        //        //    RentDate = wyp.Data_wypożyczenia,
        //        //    RentUntil = wyp.Oczekiwana_data_zwrotu,
        //        //    ReturnDate = oddanie.Data_oddania,
        //        //    Title = book.Tytuł
        //        //};
        //        var query = from oddanie in context.Oddanie
        //                    join wyp in context.Wypożyczenie on oddanie.ID_wypożyczenie equals wyp.ID into full
        //                    from wyp in full.DefaultIfEmpty()

        //                    where wyp.Oddanie == null
        //                    select new RentedBooks { ID = wyp.ID, RentDate = wyp.Data_wypożyczenia,
        //                                            RentUntil = wyp.Oczekiwana_data_zwrotu, ReturnDate = oddanie.Data_oddania, Title = "wd"};
        //        tbError.Text = query.ToString();
        //        return query.ToList();
        //    }
        //}
        #endregion

    }
}
