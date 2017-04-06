using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    static public class Login
    {

        public static void SaveWords(string str)
        {
            File.AppendAllText(@"data.txt", str + Environment.NewLine + HashPassword(str) + Environment.NewLine);
        }

        public static bool TryLogin(string login, string password)
        {
                using (var context = new LibraryEntities())
                {
                    var userData = context.Pracownik.FirstOrDefault(x => x.Login == login);

                    if (userData != null)
                        if (ValidatePassword(password, userData.Haslo))
                        {
                            return true;
                        }
                }
                return false;
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
        }

        public static bool ValidatePassword(string password, string correctPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctPasswordHash);
        }

        static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public static string ProtectLocalPassword(string str)
        {
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            byte[] data = Encoding.ASCII.GetBytes(str);
            return Convert.ToBase64String(ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser));
        }

        public static string UnprotectLocalPassword(string str)
        {
            byte[] protectedData = Convert.FromBase64String(str);
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            return Encoding.ASCII.GetString(ProtectedData.Unprotect(protectedData, entropy, DataProtectionScope.CurrentUser));
        }
    }
}
