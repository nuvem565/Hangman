using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    class longVars
    {
        // the array of european countries
        public static string[] europeanCountries = new string[] {
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


        // dad jokes array
        public static string[] comicSans =
            {
                "When does a skeleton laugh? \r\n - When someone tickles his funny bone!",
                "Why don't skeletons fight each other? \r\n - They don't have the guts",
                "What do skeletons say before they begin dining? \r\n - Bone-Appetit!",
                "Skulls are always single because they have no body",
                "Why do skeletons makes bad miners? \r\n - Because they only go 6 FOOT UNDER GROUND",
                "You wanna know why skeletons are terrible liars? \r\n - Everyone can see right through them!",
                "I'm not fat. \r\n - I'm just big boned!",
                "I hate double standards. Burn a body at a crematorium, you're \"being a respectful friend\". \r\n - Do it at home and you're \"destroying evidence\".",
                "Why can't skeletons play church music? \r\n - Because they have no organs.",
                "What band do skeletons like listening to? \r\n - Boney M",
                "Why did the ghost took the elevator? \r\n - To lift his spirit up",
                "How do French skeletons greet each other? \r\n - Bone-jour!"
            };

        public static string dadJokesGenerator()
        {
            Random rand = new Random();
            int jokeIndex = rand.Next(comicSans.Length);
            return comicSans[jokeIndex];
        }

        public static void sans()
        {
            Console.WriteLine("                sssssssssssssssss               \r\n       sssSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSsss\r\n     SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS\r\n    SSSSSSS        SSSSSSSSSSSSSSS        SSSSSSS\r\n   SSSSSS           SSSSSSSSSSSSS           SSSSSS\r\n   SSSSS       S    SSSSS   SSSSSS    S       SSSSS\r\n    SSSSSS        SSSSSS     SSSSSSS        SSSSSSS\r\n    SSSSSSSSSSSSSSSSSSS       SSSSSSSSSSSSSSSSSSSS\r\n    SSS SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS SSS\r\n   SSS  S SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS S  SSS\r\n   SSS  SS SS  SSSSSSSSSSSSSSSSSSSSSSSS SS SSS  SSS\r\n   SSSS SS SSS SSSS SSSS SSSS SSSS SSS SSS SSS SSSS\r\n    SSSS S SSS SSSS SSSS SSSS SSSS SSS SSS SS SSSS\r\n      SSSS SSS SSSS SSSS SSSS SSSS SSS SSS  SSSS\r\n         SSSSSSSSSS SSSS SSSS SSSS SSSSSSSSSS\r\n               SSSSSSSSSSSSSSSSSSSSSSSS      ");
        }
    }
}
