/*
 * 
 * AUTHOR: Vilia Wang 
 * 
 * FILENAME: encryptGame.cs
 * 
 * DATE: 29 April 2018
 * 
 * REVISION HISTORY: 
 * - Version 1 - 2 April 2018 (implementation first draft, documentation) 
 * - Version 2 - 29 april 2018 (refined implementation) 
 * 
 * 
 * DESCRIPTION: 
 * 
 * - Overview: 
 * 
 * This class provides methods for an encryptGame object.
 * This class encapsulated a private class called encryptWord which is used for this class' functionality.
 * That is, a user is able to create a encryptGame object, pass in a word to encrypt, guess the
 * shift size, and retrieve statistics from the game. 
 * 
 * 
 * - functionality: 
 * 
 * Upon instantiation, the object's variables will be set to default values. The object will also 
 * be put in a proper initial state, where "gameOn" is false.
 * Once a user enter a valid word for encryption, "gameOn" is turned to true. Once "gameOn" is true, the 
 * user may start to guess the shift value". Once the shift value is properly guessed, "gameOn" 
 * becomes false. The user can choose to get statisics from the game including number of total guesses, 
 * low guesses, high guesses and average guess value. At any point in the game, the user may reset the game.
 * 
 * 
 * - legal states:
 * 
 * The object state "gameOn" can either be true or false.
 * "gameOn" is true while the encapsulated encryptWord object is "on". 
 * "gameOn" is false while the encapsulated encryptWord object is not "on". 
 * 
 * 
 * - dependencies: 
 * 
 * encryptGame's state of "gameOn" depends on the state of the encapsulated encrypWord object. 
 * That is, if encryptWord is "on", then "gameOn" is true, and if encryptWord is not "on", then 
 * "gameOn" is false. 
 * encryptGame's encryption functionalities depend on the encapsulated encrypWord object.
 * 
 * 
 * - anticipated use: 
 * 
 * The object is anticpated to be used as a cesear ciper shift game, where a user will play a partial
 * realization of a guessing game. This class encapsulates a private cipher shift object. 
 * 
 * 
 * - data processed: 
 * 
 * This class processes integers and strings that will be used to call on encryptWord functions and 
 * for game statisic gathering purposes. 
 * 
 * 
 * - legal input: 
 * 
 * String input must be at least 4 characters and contain English alphabet characters only. 
 * Integer input must be a positive integer. 
 * 
 * 
 * - illegal input: 
 * 
 * Illegal string input has spaces, symbols or non-English alphabet characters. 
 * Illegal integer input includes any non-integer values.
 * 
 * 
 * - output: 
 * 
 * Outputs will either be a void, a string or an integer. 
 * 
 *
 * 
 * ASSUMPTIONS: 
 * 
 * - interface: 
 * 
 * This game class only shifts English alphabet characters.
 * Upper and lower case English alphabet characters are allows, but all characters will be converted
 * to lowercase before encryption. 
 * Words less than 4 charcters long will not be encrypted. 
 * For guessing the shift, if the encapsulated object is not "on" or if the guess is incorrect the  
 * function will return false. 
 * For entering words to encrypt, if the word is not valid, the function will return null. 
 * If "gameOn" is true, and a user enters a new valid word to encrypt, the game will reset. Then the 
 * new valid word will be encrypted, and the "gameOn" will be turned to true. 
 * 
 * 
 * - constructor: 
 * 
 * The constructor sets all variables to default values and puts object in a proper initial state, 
 * where "gameOn" is false.
 * 
 * 
 * - state transitions: 
 * 
 * Upon instantiation, "gameOn" is false. When the encapsulated encryptWord object is "on", then 
 * "gameOn" is true. When the encapsulated encryptWord object is not "on", then "gameOn" is false. 
 * Also, if a valid word is entered while "GameOn" is true, the object will be reset, meaning "gameOn" is 
 * false. Then the new entered word will be encrypted, making "gameOn" true. 
 *
 */

using System;

namespace HW1_encrypt {
    
    public class encryptGame {

        encryptWord word;                           // encryptWord object 

        private int totalQueries;                   // total # of queries

        private int highGuesses;                    // # of guesses above shift 

        private int lowGuesses;                     // # of guesses below shift

        private int avgGuessValue;                  // holds total guess value

        private string originalWord;                // holds original word

        private string shiftWord;                   // holds word after shift 

        private const int MIN_LETTERS = 97;         // minimum ASCII value

        private const int TOTAL_LETTERS = 26;       // total letters in alphabet

        private const int MIN_WORD_LENGTH = 4;      // minimum word length 

        private const int DEFUALT = 0;              // defult integer value = 0

        private const double DEFAULT_DECIMAL = 0.0; // default deinmal intger value = 0.0

        public const string CORRECT = "CORRECT";    // string flag for a correct guess 

        public const string LOWER = "LOWER";        // string flag for a low guess 

        public const string HIGHER = "HIGHER";      // string flag for a high guess 


        /// <summary>
        /// 
        /// Description: Initializes a new instance of the encryptGame class.
        /// Pre-condition: "gameOn" state is neither false nor true. 
        /// Post-condition: "gameOn" state is false.
        /// 
        /// </summary>
        public encryptGame() {

            this.word = new encryptWord(); 

            this.totalQueries = DEFUALT;

            this.highGuesses = DEFUALT;

            this.lowGuesses = DEFUALT;

            this.avgGuessValue = DEFUALT;

            this.originalWord = String.Empty;  

            this.shiftWord = String.Empty;
        }

        /// <summary>
        /// 
        /// Description: Resets this instance. 
        /// Pre-condition: "gameOn" state is true or false.
        /// Post-condition: "GameOn" is false.
        /// 
        /// </summary>
        public void reset() {

            this.word.reset(); 

            this.totalQueries = DEFUALT;

            this.highGuesses = DEFUALT;

            this.lowGuesses = DEFUALT;

            this.avgGuessValue = DEFUALT;

            this.originalWord = String.Empty;

            this.shiftWord = String.Empty;
        }

        /// <summary>
        /// 
        /// Description: This function takes a valid string input and returns the 
        /// encrypted version. If the word is invalid, "null" will be returned. 
        /// Pre-condition: "GameOn" is false. 
        /// Post-condition: "GameOn" is true if encrypWord is "on". "GameOn" 
        /// remains false if encryptWord is not "on".
        /// 
        /// </summary>
        /// 
        /// <returns>The encrypted word or "null".</returns>
        /// 
        /// <param name="s">S.</param>
        public string passWordGetOutput(string s) {

            string result;     // holds result of encryption function 

            // get the result of encryption
            result = word.passWordGetOutput(s);

            // if the input is invalid, return null
            if (result == null) {
                
                return null; 
            }

            // if "gameOn" is already true, reset game and encrypt new word
            if (this.isGameOn()) {
                
                this.reset();
                result = word.passWordGetOutput(s);
            }

            // return the encrypted word
            return result; 

        }

        /// <summary>
        /// 
        /// Description: Returns true if encapsulated encryptWord is "on" and false otherwise. 
        /// Pre-condition: "gameOn" is true or false. 
        /// Post-condition: "gameOn" is true or false. 
        /// 
        /// </summary>
        /// 
        /// <returns><c>true</c>, if encryptWord is "on", <c>false</c> otherwise.</returns>
        public bool isGameOn() {
            
            bool on = word.isOn();

            return on; 

        }


        /// <summary>
        /// 
        /// Description: Checks whether given shift guess is correct or incorrect. 
        /// Updates statisitcs accordingly. 
        /// Pre-condition: "GameOn" is true. 
        /// Post-condition: "GameOn" is true if guess is incorrect. "GameOn" is false if 
        /// guess is correct.
        /// 
        /// </summary>
        /// 
        /// <returns><c>true</c>, if guess is incorrect, <c>false</c> otherwise.</returns>
        /// 
        /// <param name="guess">Guess.</param>
        public bool isShift(int guess) {

            string result = word.isShift(guess);

            updateStats(guess, result);

            // if guess is correct, return true
            if (result == CORRECT) {
                
                return true; 
            }

            // else, guess is incorrect, return false.
            else {
                
                return false; 
            }
        }

        /// <summary>
        /// 
        /// Description: Returns the total number of queries as an integer.
        /// Pre-condition: "GameOn" is true or false. 
        /// Post-condition: "GameOn" is true or false. 
        /// 
        /// </summary>
        /// 
        /// <returns>The total queries.</returns>
        public int getTotalQueries() {
            
            return totalQueries; 

        }


        /// <summary>
        /// 
        /// Description: Returns the high guesses.
        /// Pre-condition: "GameOn" is true or false. 
        /// Post-condition: "GameOn" is true or false. 
        /// 
        /// </summary>
        /// 
        /// <returns>The high guesses.</returns>
        public int getHighGuesses() {
            
            return highGuesses; 

        }


        /// <summary>
        /// 
        /// Description: Returns the number of shift guesses where the guess value is 
        /// below the shift value. 
        /// Pre-condition: "GameOn" is true or false. 
        /// Post-condition: "GameOn" is true or false. 
        /// 
        /// </summary>
        /// 
        /// <returns>Total number of low guesses.</returns>
        public int getLowGuesses() {
            
            return lowGuesses;

        } 

        /// <summary>
        /// 
        /// Description: Returns the average shift guesses value as an integer. 
        /// Pre-condition: "GameOn" is true or false. 
        /// Post-condition: "GameOn" is true or false. 
        /// 
        /// </summary>
        /// 
        /// <returns>The average guess.</returns>
        public double getAverageGuess() {

            double average;        // stores the average guess value

            // if no queries have been made, the average guess value is zero 
            if (totalQueries == DEFUALT) {
                
                average = DEFAULT_DECIMAL;
                return average;

            }

            // else, calcuate and return the average guess value
            average = (avgGuessValue + DEFAULT_DECIMAL) / totalQueries;
            return average;

        }


        /// <summary>
        /// 
        /// Desciption: Updates the statistics. 
        /// Precondition: "GameOn" is true.
        /// Post-condition: "GameOn" is true.
        /// 
        /// </summary>
        /// 
        /// <param name="guess">Guess.</param>
        private void updateStats(int guess, string result) {

            // update the total number of queries
            this.totalQueries++;

            // update the total value of queries
            avgGuessValue += guess; 

            // update number of low guesses
            if (result == LOWER) {
                
                this.lowGuesses++;
            }

            // update number of high guesses
            if (result == HIGHER) {
                
                this.highGuesses++;
            }
        }
    }
}
