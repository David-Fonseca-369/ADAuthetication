using System;
using System.DirectoryServices;

namespace ADAuthetication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string username = "david.fonseca";
            string password = "Password#26";

            DirectoryEntry Entry = new DirectoryEntry("LDAP://eisla.local", username, password);
            DirectorySearcher Searcher = new DirectorySearcher(Entry);
            Searcher.Asynchronous = true;
            SearchResult Results;

            Searcher.SearchScope = SearchScope.Subtree;
            Searcher.ReferralChasing = ReferralChasingOption.All;

            Searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))";
            try
            {
                // Si existe el usuario, entonces buscamos algunos datos almacenados en Active Directory, como son el nombre real y el email
                Results = Searcher.FindOne();
                if (Results != null)
                {
                    string nombreCompleto = Results.GetDirectoryEntry().Properties["DisplayName"].Value.ToString();

                    string email = Results.GetDirectoryEntry().Properties["mail"].Value.ToString();



                    Console.WriteLine("Su nombre es {0} y tu correo es {1}", nombreCompleto, email);
                }
                else
                {
                    Console.WriteLine("Error,usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
