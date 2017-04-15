using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public static class UIChecker
    {
        static public bool CheckForEmptyFields(Dictionary<string, string> list)
        {
            foreach (var field in list)
            {
                if (field.Value == string.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        static public string GetEmptyFieldsName(Dictionary<string,string> list)
        {
            var sb = new StringBuilder();
            foreach (var field in list)
            {
                if (field.Value == string.Empty)
                {
                    sb.AppendLine(field.Key);
                }
            }
            return sb.ToString();
        }

        static public Dictionary<string, string> RegisterFieldsDictionary(string FirstName, string LastName, string PhoneNumber, string PostalCode, string City, string Street, string HouseNr)
        {
            var registerFieldList = new Dictionary<string, string>();
            registerFieldList.Add("First Name", FirstName);
            registerFieldList.Add("Last Name", LastName);
            registerFieldList.Add("Phone Number", PhoneNumber);
            registerFieldList.Add("Postal Code", PostalCode);
            registerFieldList.Add("City", City);
            registerFieldList.Add("Street", Street);
            registerFieldList.Add("House Number", HouseNr);
            return registerFieldList;
        }
    }

    
}
