/*
 * 
 * AUTHOR: Vilia Wang 
 * 
 * FILENAME: encryptWord.cs
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
 * This class provides methods for an encryptWord object with an undisclosed Caesar cipher shift.
 * For example, with a shift of '3', the letter 'a' will be encrypted as 'd'.
 * 
 * 
 * - functionality: 
 * 
 * Upon instantiation, the object's variables will be set to default values. The object will also 
 * be put in a proper initial state, where "on" is false.
 * At this point, a user may enter a valid word for encryption. Once a valid word is encrypted, the
 * state "on" is true. Then, the user can guess the shift value. Once the correct shift value is 
 * guessed, the state "on" is false. At any point, a user may reset the object which resets all 
 * variables to default values and puts the object back into a proper initial state, where "on" is 
 * false.
 * 
 * 
 * - legal states: 
 * 
 * The object can either be "on" or not "on". 
 * The object is "on" while the user has entered a word to encrypt and while the user is trying to 
 * guess the correct shift size. 
 * The object is not "on" if no word has been entered for encryption and as soon as the correct 
 * shift has been guessed. 
 * 
 * 
 * - dependencies: 
 * 
 * The object's state has dependencies. If passWordGetOutput(string) has not been called with a valid
 * word, meaning no word has been encrypted, the state "on" is false. As soon as a valid word has been 
 * encrypted, the state "on" is true. While the user incorrectly guesses the shift size, the state "on"
 * remains true. As soon as the correct shift size is guessed, the state "on" becomes false.
 * 
 * 
 * - anticipated use: 
 * 
 * The object is anticpated to be used as a part of a cesear ciper shift game, where a user will 
 * play a partial realization of a guessing game. 
 * 
 * 
 * - data processed: 
 * 
 * This class processes integers and strings for the encryption and guessing functionality. 
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
 * This class only shifts English alphabet characters.
 * If the input word contains captial characters, these characters will be turned to lowercase
 * characters before encryption. 
 * Words less than 4 charcters long will not be encrypted. 
 * For guessing the shift, the object will return "CORRECT" if the guess is correct. 
 * For guessing the shift, the object will return "LOWER" if the guess is lower than the shift. 
 * For guessing the shift, the object will return "HIGHER" if the guess is higher than the shift. 
 * For entering words to encrypt, if the word is not valid, the function will return null. 
 * If object is "on", meaning a word has been encrypted, and the user enters another valid word for 
 * encryption, the object first first be reset, and then the word will be encrypted. 
 * 
 * 
 * - constructor: 
 * 
 * The constructor sets all variables to default values and puts object in a proper initial state, 
 * where "on" is false.
 * 
 * 
 * - state transitions: 
 * 
 * Upon instantiation, the object's state "on" is false. Once a valid word has been encrypted, the 
 * state "on" is turned to true. Once the correct shift value is guessed, the state "on" turns to  
 * false. Once the reset() function is called the state "on" will be set to false. 
 * If the object is reset at any point, the state "on" will be changed to false. 
 * If a valid word is entered while the object is "on", the object will be reset, meaning it is 
 * not "on". Then the new entered word will be encrypted, making the object "on".  
 *
 */

using System;

namespace HW1_encrypt {
    
    public class encryptWord {

        private int shift;                      // secret cipher shift 

        private string originalWord;            // holds original word

        private string shiftWord;               // holds word after shift 

        private const int MIN_LETTERS = 97;     // minimum ASCII value

        private const int MAX_LETTERS = 122;    // maximum ASCII value   

        private const int TOTAL_LETTERS = 26;   // total letters in alphabet

        private const int MIN_WORD_LENGTH = 4;  // minimum word length 

        private const int DEFUALT = 0;          // defult integer value = 0

        public const string CORRECT = "CORRECT";// string flag for correct guess 

        public const string LOWER = "LOWER";    // string flag for a low guess 

        public const string HIGHER = "HIGHER";  // string flag for a high guess 

        Random rnd = new Random();              // create Random object 


        /// <summary>
        /// 
        /// Description: Initializes a new instance of the encrptWord class.
        /// Pre-condition: object is neither "on" nor not "on".
        /// Post-condition: object is not "on".
        /// 
        /// </summary>
        public encryptWord() {

            this.shift = calculateShift();  

            this.originalWord = String.Empty;  

            this.shiftWord = String.Empty;      
        }


        /// <summary>
        /// 
        /// Description: Resets this instance. 
        /// Pre-condition: object is "on" or not "on".
        /// Post-condition: object is not "on".
        /// 
        /// </summary>
        public void reset() {

            this.shift = calculateShift();  

            this.originalWord = String.Empty;   

            this.shiftWord = String.Empty;      
        }


        /// <summary>
        /// 
        /// Description: This function takes a valid string input, encrypts it, and returns the 
        /// encrypted word. If the string input is invalid the function will return "null". 
        /// Pre-condition: object is not "on". 
        /// Post-condition: object is "on" or not "on".
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Returns "null" for invalid string input. Returns the shifted word if the 
        /// input is valid. 
        /// </returns>
        /// 
        /// <param name="word">Word.</param>
        public string passWordGetOutput(string word) {

            // ensures that words less than 4 charcters are not encrypted 
            if (word.Length < MIN_WORD_LENGTH) {
                
                return null;

            }

            // ensures words containing non-English alphabet characters are not encrypted 
            string inputWord = word.ToLower();
            for (int i = 0; i < inputWord.Length; i++) {

                int asciiOfChar = (int)inputWord[i];

                if (asciiOfChar < MIN_LETTERS || asciiOfChar > MAX_LETTERS) {
                    
                    return null; 
                }

            }            

            // if the object is already "on", reset object first
            if (this.isOn()) {
                
                this.reset();
            }


            // Now, the word is valid. Therefore, calcuate the shift word and return shiftWord.
            this.originalWord = word.ToLower();
            this.shiftWord = cipherShift(originalWord);
            return shiftWord;

        }


        /// <summary>
        /// 
        /// Description: Returns true is the object is "on" and false otherwise. 
        /// Pre-condition: object is "on" or not "on". 
        /// Post-condition: object is "on" or not "on".  
        /// 
        /// </summary>
        /// 
        /// <returns><c>true</c>, if "on", <c>false</c> otherwise.</returns>
        public bool isOn() {

            return originalWord != string.Empty; 

        }


        /// <summary>
        /// 
        /// Description: This function checks whether the shift guess is correct, lower
        /// or higher. 
        /// Pre-condition: object must be "on"
        /// Post-condition: If the guess is correct "on" becomes false. If the guess is 
        /// incorrect, "on" is still true. 
        /// 
        /// </summary>
        /// 
        /// <returns> 
        /// 
        /// returns "CORRECT", if guess is correct, "LOWER" if guess is lower, and
        /// "HIGHER" if guess is higher.
        /// 
        /// </returns>
        /// 
        /// <param name="guess">Guess.</param>
        public string isShift(int guess) {
            
            // if the guess matches the shift, change state and return HIGHER
            if (guess == shift) {
                
                // change state "on" to false if shift guessed correctly
                this.originalWord = string.Empty;   
                return CORRECT;

            }

            // else if the guess is lower, return LOWER
            else if (guess < shift) {
                
                return LOWER;
            }

            // else the guess is higher, return HIGHER
            else {
                
                return HIGHER; 
            }
        }


        ///// <summary>
        ///// 
        ///// Description: Checks if guess value is lower than shift.
        ///// Pre-condition: object is "on" or not "on".
        ///// Post-condition: object is "on" or not "on".
        ///// 
        ///// </summary>
        ///// 
        ///// <returns><c>true</c>, if guess is lower, <c>false</c> otherwise.</returns>
        ///// 
        ///// <param name="guess">Guess.</param>
        //public bool isLower(int guess) {

        //    if (guess < shift) {
                
        //        return true; 
        //    }

        //    else {
                
        //        return false; 
        //    }
        //}


        ///// <summary>
        ///// 
        ///// Description: Checks if guess value is higher than shift.
        ///// Pre-condition: object is "on" or not "on".
        ///// Post-condition: object is "on" or not "on".
        ///// 
        ///// </summary>
        ///// 
        ///// <returns><c>true</c>, if guess is higher, <c>false</c> otherwise.</returns>
        ///// 
        ///// <param name="guess">Guess.</param>
        //public bool isHigher(int guess) {

        //    if (guess > shift) {

        //        return true;
        //    }

        //    else {

        //        return false;
        //    }
        //}


        /// <summary>
        /// 
        /// Description: Private function that calculates the shift value. A number between 
        /// 1-25 is generated and returned. 
        /// Pre-condition: object is either "on" or not "on".
        /// Post-condition: object is either "on" or not "on".
        /// 
        /// </summary>
        /// 
        /// <returns>The shift size.</returns>
        private int calculateShift() {

            int randomNum = rnd.Next(1, 25);

            return randomNum;

        }

        /// <summary>
        /// 
        /// Description: Private funtion that shifts the given word.
        /// Pre-condition: object is "on".
        /// Post-condition: object is "on".
        /// 
        /// </summary>
        /// 
        /// <returns>The shifted word.</returns>
        /// 
        /// <param name="word">Word.</param>
        private string cipherShift(string word) {

            int code = 0;                   // the current ASCII code

            int newCode = 0;                // the shifted ASCII code 

            string newWord = string.Empty;  // the new shifted word

            // for each letter in the word, apply a ciper shift
            for (int i = 0; i < word.Length; i++) {

                // calculate the ascii code for each character 
                code = (int)word[i];

                // shift each ascii code to new, shifted ascii
                newCode = (code + shift - MIN_LETTERS) %
                          TOTAL_LETTERS + MIN_LETTERS;

                // convert ascii code to corresponding character
                newWord += (char)newCode;
            }

            return newWord;
        }
    }
}
