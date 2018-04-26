/*
 * 
 * AUTHOR: Vilia Wang 
 * 
 * FILENAME: p1.cs
 * 
 * DATE: 29 April 2018
 * 
 * REVISION HISTORY: 
 * - Version 1 - 23 April 2018 (implementation first draft, documentation) 
 * 
 * 
 * DESCRIPTION: 
 * 
 * - Overview: 
 * 
 * This program runs a automated version of the encryptGame by playing 6 rounds of the game with 
 * various string inputs for encryption. 
 * 
 * 
 * - functionality: 
 * 
 * This program is automated, therefore no user input is needed. The program begins with a welcome
 * statement that outlines the game. Next, each round will "play" encryptGame with a different string 
 * input. If the input string is invalid, an error message will be printed. However, if the input string 
 * is valid, the encrypted version of the word will be printed. Next, the automated program will begin
 * to "guess" the shift value, starting 1 and increamenting one until the correct shift value is guessed. 
 * Once the correct shift value is guessed, statistics are printed for the round. 
 * The purpose of this program is to test the various string inputs with the encryptGame.
 * 
 * 
 * - legal states:
 * 
 * None.
 * 
 * 
 * - dependencies: 
 * 
 * Since p1.cs is a game that tests encryptGame.cs, this program is dependant on encryptWord.cs. 
 * 
 * 
 * - anticipated use: 
 * 
 * This program is anticpated to test the functionality of the encryptGame class. 
 * 
 * 
 * - data processed: 
 * 
 * Since this program is automated, no input data will processed. However, the program will use
 * pre-determined string inputs to test the encryptGame. 
 * 
 * 
 * - legal input: 
 * 
 * N/A
 * 
 * 
 * - illegal input: 
 * 
 * N/A
 * 
 * 
 * - output: 
 * 
 * Outputs will be printed to the console. 
 * 
 *
 * 
 * ASSUMPTIONS: 
 * 
 * - interface: 
 * 
 * This is an automated version of the encryptGame. No user input will be accepted. 
 * 
 * - constructor: 
 * 
 * N/A
 * 
 * 
 * - state transitions: 
 * 
 * N/A
 *
 */


using System;

namespace HW1_encrypt {
    
    class p1 {

        /// <summary>
        /// 
        /// Welcomes user to program and gives a brief introduction. 
        /// 
        /// </summary>
        static void welcome () {
            
            Console.WriteLine("Welcome to EncryptWord!\n");

            Console.WriteLine("This program plays the encrypGame with several words: ");

            Console.WriteLine("input 1: \"\"       --> tests an empty string");

            Console.WriteLine("input 2: \"wee\"    --> tests a word that is too short");

            Console.WriteLine("input 3: \"1234\"   --> tests an input with non-alphabet characters"); 

            Console.WriteLine("input 4: \"!@#$%^\" --> tests an input with non-alphabet characters"); 

            Console.WriteLine("input 5: \"HELLO\"  --> tests a valid input with upper-case letters");

            Console.WriteLine("input 6: \"four\"   --> tests a valid input with lower-case letters");

            Console.WriteLine(); 
        }

        /// <summary>
        /// 
        /// Prints the round number.
        /// 
        /// </summary>
        /// 
        /// <param name="i">The index.</param>
        static void printRoundNum(int i) { 
            
            Console.WriteLine("\n\n----------");

            Console.WriteLine("ROUND " + (i + 1));

            Console.WriteLine("----------");

        }

        /// <summary>
        /// 
        /// Tests different shift guesses until the correct shift is guessed. 
        /// 
        /// </summary>
        /// 
        /// <param name="correct">If set to <c>true</c> correct.</param>
        /// <param name="game">Game.</param>
        /// <param name="guessValue">Guess value.</param>
        static void guessShift(ref bool correct, ref encryptGame game, ref int guessValue) {

            while (!correct) {

                correct = game.isShift(guessValue);

                Console.WriteLine("Shift guess:    " + guessValue + "     --> " + correct);

                guessValue++;

            }
        }

        /// <summary>
        /// 
        /// Prints the statistics.
        /// 
        /// </summary>
        /// 
        /// <param name="i">The index.</param>
        /// <param name="e">E.</param>
        static void printStats(int i, ref encryptGame e) {

            Console.WriteLine("\nROUND " + (i + 1) + " STATISITCS");

            Console.WriteLine("Total guesses:       " + e.getTotalQueries());

            Console.WriteLine("Low guesses:         " + e.getLowGuesses());

            Console.WriteLine("High guesses:        " + e.getHighGuesses());

            Console.WriteLine("Average guess value: " + e.getAverageGuess());

        }

        /// <summary>
        /// 
        /// Resets the round in preparation for the next round. 
        /// 
        /// </summary>
        /// 
        /// <param name="e">E.</param>
        /// <param name="correct">If set to <c>true</c> correct.</param>
        /// <param name="guessValue">Guess value.</param>
        static void resetRound(ref encryptGame e, ref bool correct, ref int guessValue) {
            
            e.reset();

            correct = false;

            guessValue = 1;

            Console.WriteLine();
        }

        
        static void Main(string[] args) {
                
            int totalRounds = 6;                    // total number of rounds in game 

            int guessValue = 1;                     // starting value for guessing

            bool correct = false;                   // tracks user's guess

            string encryptedWord = string.Empty;    // holds the encrypted word

            // string array of words for game
            string[] inputWord = { "", "wee", "1234", "!@#$%^", "HELLO", "four"};


            // welcomes user and give brief explaination of program
            welcome(); 

            // loop through rounds of the game for each word in string array
            for (int i = 0; i < totalRounds; i++) {

                // print the round number that is currently executing 
                printRoundNum(i);

                // create a distinct encryptWord object in each round of game 
                encryptGame game = new encryptGame();

                // Enter word to be encrypted
                Console.WriteLine("Entering word:  " + inputWord[i]);
                encryptedWord = game.passWordGetOutput(inputWord[i]);

                // if the inputWord is valid, print encryptedWord
                if (encryptedWord != null) {
                    
                    Console.WriteLine("Encrypted word: " + encryptedWord + "\n");

                    // keep guessing shift value while guess is incorrect
                    guessShift(ref correct, ref game, ref guessValue); 

                    // print statisics to screen 
                    printStats(i, ref game);
                }

                // else, the inputWord is not valid, print an error message 
                else {
                    
                    Console.WriteLine("\"" + inputWord[i] + "\"" + " is not a valid input word!");
                }

                // reset game for next round 
                resetRound(ref game, ref correct, ref guessValue);

            }
        }
    }
}
