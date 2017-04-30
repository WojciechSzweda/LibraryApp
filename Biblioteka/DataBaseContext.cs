using System;
using System.Collections.Generic;
using System.Linq;

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


        public class GetRentedBooks_Result
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public DateTime RentDate { get; set; }
            public DateTime RentUntil { get; set; }
            public DateTime? ReturnDate { get; set; }
        }

        static public List<GetRentedBooks_Result> ShowRentedBooks(string NrCardText)
        {
            var cardNr = GetCardID(NrCardText);
            if (cardNr == 0)
                return null;

            using (var context = new LibraryModel())
            {
                var query = from rent in context.Rent
                            where rent.ID_Card == cardNr
                            select new GetRentedBooks_Result() { ID = rent.ID, ReturnDate = rent.Return.Return_Date, RentDate = rent.Rent_Date, RentUntil = rent.Expected_Return_Date, Title = rent.Copy.Book.Title };
                return query.ToList();
            }
        }

        static public void Registration(string _FirstName, string _LastName, string _PhoneNumber, string _PostalCode, string _City, string _Street, string _HouseNr, string _ApartmentNr)
        {
            try
            {
                using (var context = new LibraryModel())
                {
                    var card = new Card
                    {
                        First_Name = _FirstName,
                        Last_Name = _LastName,
                        Phone_Number = _PhoneNumber,
                        City = _City,
                        Postal_Code = _PostalCode,
                        Street = _Street,
                        House_Number = _HouseNr,
                        Apartment_Nuber = _ApartmentNr,
                        State = 1
                    };

                    var fieldsList = UIChecker.RegisterFieldsDictionary(_FirstName, _LastName, _PhoneNumber, _PostalCode, _City, _Street, _HouseNr);
                    if (UIChecker.CheckForEmptyFields(fieldsList))
                    {
                        ErrorMessageBoxEvent(null, new CustomEventArgs { MessageText = UIChecker.GetEmptyFieldsName(fieldsList) });
                        return;
                    }
                    context.Card.Add(card);
                    context.SaveChanges();
                    InfoMessageBoxEvent(null, new CustomEventArgs { MessageText = $"Registration Successful\nNew ID: {card.ID}", Caption = "Success" });
                }
            }
            catch (Exception)
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
                using (var context = new LibraryModel())
                {
                    var copyInfo = from copy in context.Copy
                                   join book in context.Book on copy.ID_Book equals book.ID
                                   where copy.ID == copyID
                                   select new BookCopyInfo { Title = book.Title, PublishDate = book.Release_Date, ISBN = book.ISBN, BookCopyState = copy.Book_State };
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
