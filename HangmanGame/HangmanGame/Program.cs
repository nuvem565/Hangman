using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // import the array of countries with capitals
            string workingDirectory = Environment.CurrentDirectory;
            string countriesFilePath = Directory.GetParent(workingDirectory).Parent.FullName + "\\" + "countries_and_capitals.txt";
            string[] countriesInput = File.ReadAllLines(countriesFilePath);

            // the array of european countries
            string[] europeanCountries = new string[] {
                "Albania",
                "Andorra",
                "Armenia",
                "Austria",
                "Belarus",
                "Belgium",
                "Bosnia and Herzegovina",
                "Bulgaria",
                "Croatia",
                "Cyprus",
                "Czech Republic",
                "Denmark",
                "Estonia",
                "Finland",
                "France",
                "Germany",
                "Greece",
                "Hungary",
                "Iceland",
                "Ireland",
                "Italy",
                "Kosovo",
                "Latvia",
                "Liechtenstein",
                "Lithuania",
                "Luxembourg",
                "Macedonia",
                "Malta",
                "Moldova",
                "Monaco",
                "Montenegro",
                "The Netherlands",
                "Norway",
                "Poland",
                "Portugal",
                "Romania",
                "Russia",
                "San Marino",
                "Serbia",
                "Slovakia",
                "Slovenia",
                "Spain",
                "Sweden",
                "Switzerland",
                "Ukraine",
                "United Kingdom",
                "Vatican City" };

            // filtering european countries - europeans with name and capitals as in the input array
            string[] europeans =
                Array.FindAll(countriesInput, x =>
                    europeanCountries.Any(anyEuropeanCountry => x.StartsWith(anyEuropeanCountry))
                );
            // debug
            for (int i = 0; i < europeans.Length; i++)
                Console.Write(europeans[i] + "\r\n");

            // Flags and required global variables
            bool wannaPlayAgain = false;
            string expectedCapital, expectedCountry;

            // Method for choosing random capital (with country)
            string randomCapital(string[] countriesWithCapitals)
            {
                Random r = new Random();
                int countryIndex = r.Next(countriesWithCapitals.Length);
                return countriesWithCapitals[countryIndex];
            }

            

        }
    }
}
