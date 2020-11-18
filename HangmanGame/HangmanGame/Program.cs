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
            
            // PREPARING THE DATA

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

            // END OF PREPARING THE DATA


            // GLOBAL (not exactly) VARIABLES, METHODS AND FLAGS

            // Flags and required global variables
            bool wannaPlayAgain = false;
            string expectedCapital, expectedCountry;
            char pressedButtonLW;

            // Method for choosing random capital (with country)
            string randomCapital(string[] countriesWithCapitals)
            {
                Random r = new Random();
                int countryIndex = r.Next(countriesWithCapitals.Length);
                return countriesWithCapitals[countryIndex];
            }

            // END OF GLOBAL VARIABLES, METHODS AND FLAGS


            // MAIN PROGRAM LOOP

            // Procedure of the game - the game main loop
            do
            {

                string[] dividedCountryString = randomCapital(europeans).Split(new char['|'], 2);
                expectedCountry = dividedCountryString[0].Trim();
                expectedCapital = dividedCountryString[1].Trim();
                Console.WriteLine("Welcome in the Hangman game!");
                // maybe some ASCII art?
                Console.WriteLine("Try to guess the european country capital city we have in mind. ");
                Console.WriteLine("You have 5 lives. First, you decide whether you want to guess a single letter or the whole answer typing l or w accordingly.");
                Console.WriteLine("If you mistake guessing the letter, you lose one life. If you guessing the whole capital, you lose two lives.");


                // THE GAME RUNTIME

                // Reading the input of l/w in infinite loop
                do
                {
                    Console.WriteLine("You want to guess the letter or the whole capital name? Type l or w:");
                    pressedButtonLW = Console.ReadKey(true).KeyChar;
                    // Check if user typed a proper letter and proceed
                } while ( !(new char[] { 'l', 'w', 'L', 'W' }.Any(ch => pressedButtonLW == ch)) );

                // END OF THE GAME RUNTIME

            } while (wannaPlayAgain);

            // END OF MAIN PROGRAM LOOP
        }
    }
}
