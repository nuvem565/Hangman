using System;
using System.IO;
using System.Diagnostics;
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
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string countriesFilePath = projectDirectory + "\\" + "countries_and_capitals.txt";
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

            // END OF PREPARING THE DATA


            // GLOBAL (not exactly) VARIABLES, METHODS AND FLAGS

            // Flags and required global variables
            bool wannaPlayAgain = false;
            bool areYouWinningSon = false;
            string expectedCapital, expectedCountry;
            string inputAnswer;
            string inputLetter;
            char pressedButtonLW;
            int actualLives = 5;
            int roundCounter = 1;
            int guessingTries = 1;
            List<string> notInWord = new List<string>();
            List<string> correctlyGuessed = new List<string>();
            
            //Declaring the stopwatch object
            Stopwatch stoper = new Stopwatch();
            // To start counting guessing time
            stoper.Start();

            // Method for choosing random capital (with country)
            string randomCapital(string[] countriesWithCapitals)
            {
                Random r = new Random();
                int countryIndex = r.Next(countriesWithCapitals.Length);
                return countriesWithCapitals[countryIndex];
            }

            string hiddenAnswer(List<string> correctLetters, string hiddenCapital)
            {
                string output = "";
                foreach (var letterToHide in hiddenCapital)
                {
                    if (correctLetters.Any(l => l == letterToHide.ToString().ToUpper()))
                        output += (letterToHide.ToString().ToUpper() + " ");
                    else
                        output += "_ ";
                }
                return output;
            }

            void printLives(int hearts)
            {
                for(int i = 0; i < hearts; i++)
                    Console.Write(" _  _ ");
                Console.WriteLine();
                for (int i = 0; i < hearts; i++)
                    Console.Write("/ \\/ \\");
                Console.WriteLine();
                for (int i = 0; i < hearts; i++)
                    Console.Write("\\    /");
                Console.WriteLine();
                for (int i = 0; i < hearts; i++)
                    Console.Write(" \\  / ");
                Console.WriteLine();
                for (int i = 0; i < hearts; i++)
                    Console.Write("  \\/  ");
                Console.WriteLine();
            }

            char askForAgain()
            {
                Console.WriteLine("Do you want to try again? [Y/N]");
                char wantsToPlayAgain;
                do
                {
                    wantsToPlayAgain = Console.ReadKey().KeyChar;
                } while (wantsToPlayAgain != 'y' && wantsToPlayAgain != 'Y' && wantsToPlayAgain != 'n' && wantsToPlayAgain != 'N');
                return wantsToPlayAgain;
            }

            // END OF GLOBAL VARIABLES, METHODS AND FLAGS



            // MAIN PROGRAM LOOP

            // Procedure of the game - the game main loop
            do
            {

                string[] dividedCountryString = randomCapital(europeans).Split(new char[] { '|' },2);
                // for debug only
                Console.WriteLine(randomCapital(europeans));
                expectedCountry = dividedCountryString[0].Trim();
                expectedCapital = dividedCountryString[1].Trim();
                Console.WriteLine("Welcome in the Hangman game!");
                // maybe some ASCII art?
                Console.WriteLine("Try to guess the european country capital city we have in mind. ");
                Console.WriteLine("You have 5 lives. First, you decide whether you want to guess a single letter or the whole answer typing l or w accordingly.");
                Console.WriteLine("If you mistake guessing the letter, you lose one life. If you guessing the whole capital, you lose two lives.");
                Console.WriteLine();


                // THE GAME RUNTIME

                do
                {

                    // Prints separation bar, actual number of lives
                    Console.WriteLine("---------- ROUND {0} ----------", roundCounter);
                    Console.WriteLine();
                    printLives(actualLives);
                    Console.WriteLine();

                    // display the hidden answer and "not-in-word" list
                    Console.WriteLine("Used letters: ");
                    foreach (var letter in notInWord)
                        Console.Write(letter + " ");
                    Console.WriteLine();
                    Console.WriteLine("Secret word: {0}", hiddenAnswer(correctlyGuessed, expectedCapital));
                    Console.WriteLine();

                    // Reading the input of l/w in infinite loop
                    do
                    {
                        Console.WriteLine("You want to guess the letter or the whole capital name? Type l or w:");
                        pressedButtonLW = Console.ReadKey(true).KeyChar;
                        // Check if user typed a proper letter and proceed
                    } while (!(new char[] { 'l', 'w', 'L', 'W' }.Any(ch => pressedButtonLW == ch)));

                    if (pressedButtonLW == 'l' || pressedButtonLW == 'L')
                    {
                        // Taking the input letter guessed by player
                        Console.WriteLine("Now, guess the letter:");
                        inputLetter = Console.ReadKey(false).KeyChar.ToString().ToUpper();
                        Console.WriteLine();
                        Console.WriteLine();
                        // Check whether the letter fits into expected word and whether to update the correct letters or "not-in-word" array with lives decrement
                        if (expectedCapital.Any( ch => ch.ToString().ToUpper() == inputLetter ))
                        {
                            correctlyGuessed.Add(inputLetter.ToUpper());
                            // check whether the player actually wins
                            if( expectedCapital.All(letterOfCapital => correctlyGuessed.Any( correctLetter => correctLetter == letterOfCapital.ToString().ToUpper() )) )
                            {
                                areYouWinningSon = true;
                            }
                        }
                        else
                        {
                            --actualLives;
                            notInWord.Add(inputLetter.ToUpper());
                        }
                    }
                    else
                    {
                        // Taking the input answer (word) guessed by player
                        Console.WriteLine("So, you know what do I think? Don't push yourself, we'll hang on. ");
                        inputAnswer = Console.ReadLine();
                        // Check whether it is good answer
                        if (inputAnswer.ToUpper() == expectedCapital.ToUpper())
                        {
                            areYouWinningSon = true;
                        }
                        else
                        {
                            actualLives -= 2;
                        }
                    }

                    // go to next round - increment the index
                    roundCounter++;

                } while (actualLives > 0 && areYouWinningSon == false);


                // GAMEOVER

                if (areYouWinningSon == true)
                {
                    // The guessed answer is correct
                    Console.WriteLine();
                    Console.WriteLine("Congratulations! You guessed the correct answer.");
                    Console.WriteLine();
                    Console.WriteLine("   {0}, the capital city of {1}", expectedCapital.ToUpper(), expectedCountry.ToUpper());
                    Console.WriteLine();

                    // Stops the timer, gets elapsed time
                    stoper.Stop();
                    TimeSpan elapsedTime = stoper.Elapsed;
                    // Write the elapsed time of the game to the console
                    Console.WriteLine("You have completed the game in: {0} hours, {1} minutes, {2}.{3:000} seconds", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
                    
                    // Ask player for his/her name
                    Console.WriteLine("Please, enter your name:");
                    string playerName = Console.ReadLine();

                    // Format the string to be recorded
                    string formattedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
                    string[] record = new string[] { playerName + " | " +  DateTime.Today.ToShortDateString() + " | " + formattedTime + " | " + guessingTries.ToString() + " | " + expectedCapital };
                    
                    // Write the name, date, elapsed time, tries and the answered capital to the file
                    File.AppendAllLines( (projectDirectory + "\\" + "scores.txt"), record);

                    Console.WriteLine();
                    char playAgainInput = askForAgain();
                    if (playAgainInput == 'y' || playAgainInput == 'Y')
                    {
                        wannaPlayAgain = true;
                        // restarts the stopwatch again after won game
                        stoper.Restart();
                    }
                    else
                        wannaPlayAgain = false;
                }
                else
                {
                    // The game is lost
                    Console.WriteLine();
                    Console.WriteLine("You lose! The correct answer is:");
                    Console.WriteLine();
                    Console.WriteLine("   {0}, the capital city of {1}", expectedCapital.ToUpper(), expectedCountry.ToUpper());
                    Console.WriteLine();
                    char playAgainInput = askForAgain();
                    if (playAgainInput == 'y' || playAgainInput == 'Y')
                        wannaPlayAgain = true;
                    else
                        wannaPlayAgain = false;
                }

                // Clearing the flags and variables before the next play (wannaPlayAgain == true)
                if(wannaPlayAgain)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    notInWord.Clear();
                    correctlyGuessed.Clear();
                    // increments the guessing tries counter after each lose game
                    if (!areYouWinningSon)
                        guessingTries++;
                    // to change to false only after guessing tries increment!
                    areYouWinningSon = false;
                    actualLives = 5;
                    roundCounter = 1;
                }
                // END OF GAMEOVER

                // END OF THE GAME RUNTIME

            } while (wannaPlayAgain);

            // END OF MAIN PROGRAM LOOP
        }
    }
}
