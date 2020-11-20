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



            // filtering european countries - europeans with name and capitals as in the input array
            string[] europeans =
                Array.FindAll(countriesInput, x =>
                    longVars.europeanCountries.Any(anyEuropeanCountry => x.StartsWith(anyEuropeanCountry))
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
                    longVars.printLives(actualLives);
                    Console.WriteLine();

                    // display the hidden answer and "not-in-word" list
                    Console.Write("Used letters: ");
                    foreach (var letter in notInWord)
                        Console.Write(letter + " ");
                    Console.WriteLine();
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
                            // Display a joke
                            Console.WriteLine("{0}", longVars.dadJokesGenerator());
                            Console.WriteLine();
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
                            // Display a joke
                            Console.WriteLine();
                            Console.WriteLine("{0}", longVars.dadJokesGenerator());
                            Console.WriteLine();
                        }
                    }

                    // go to next round - increment the index
                    roundCounter++;

                } while (actualLives > 0 && areYouWinningSon == false);


                // GAMEOVER

                if (areYouWinningSon == true)
                {
                    // AFTER WON GAME

                    Console.WriteLine();
                    Console.WriteLine("Congratulations! You guessed the correct answer.");
                    Console.WriteLine();
                    Console.WriteLine("   {0}, the capital city of {1}", expectedCapital.ToUpper(), expectedCountry.ToUpper());
                    Console.WriteLine();

                    // Stops the timer, gets elapsed time
                    stoper.Stop();
                    TimeSpan elapsedTime = stoper.Elapsed;
                    // Write the elapsed time of the game to the console
                    Console.WriteLine("You have completed the game in {0} seconds", elapsedTime.TotalSeconds.ToString("F3"));
                    
                    // Ask player for his/her name
                    Console.WriteLine("Please, enter your name:");
                    string playerName = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine();

                    // Format the string to be recorded
                    string formattedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
                    string[] newRecord = new string[] { playerName + " | " +  DateTime.Today.ToShortDateString() + " | " + formattedTime + " | " + guessingTries.ToString() + " | " + expectedCapital };
                    
                    // Write the name, date, elapsed time, tries and the answered capital to the file
                    File.AppendAllLines( (projectDirectory + "\\" + "scores.txt"), newRecord);

                    // Write the record to the 10 highest scores file
                    if (File.Exists(projectDirectory + "\\" + "high_score.txt"))
                    {
                        List<string> storedRecords = File.ReadLines(projectDirectory + "\\" + "high_score.txt").ToList();
                        storedRecords.Add(newRecord[0]);
                        // If the textfile is incorrectly formatted, skip this and write only new record
                        try
                        {
                            // Sorts the records by ascending order of: 1) number of tries, 2) elapsed time of the game
                            storedRecords = storedRecords.OrderBy(record => (record.Split(new char[] { '|' })[3]).Trim())
                                                     .ThenBy(record => TimeSpan.Parse((record.Split(new char[] { '|' })[2]).Trim()))
                                                     .ToList();
                        }
                        catch(IndexOutOfRangeException)
                        {
                            ;
                        }
                        // Display 10 best records to the console
                        Console.Write("Player name | Date | Guessing time (hh:mm:ss) | Number of tries | Correct answer");
                        foreach( var rec in storedRecords.Take(10))
                            Console.WriteLine(rec);
                        // Takes first 10 records and appends it to the same file
                        File.WriteAllLines(projectDirectory + "\\" + "high_score.txt", storedRecords.Take(10));
                    }
                    else
                    {
                        File.WriteAllLines(projectDirectory + "\\" + "high_score.txt", (newRecord) );
                    }

                    Console.WriteLine();
                    // Time for ASCII art
                    longVars.sans();
                    Console.WriteLine();
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

                    // END OF AFTER WON GAME
                }
                else
                {
                    // AFTER LOST GAME

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

                    // END OF AFTER LOST GAME
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
                    else
                        guessingTries = 1;
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
