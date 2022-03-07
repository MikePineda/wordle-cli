using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Services;

namespace Wordle
{
    internal class Board
    {

        private char[,] BoardArray;
        private readonly WordleService _wordleService;
        private string word;
        private int currentRow = 0;
        private int numberOfRows;
        public Board(int numberOfTries, WordleService wordleService)
        {
            this._wordleService = wordleService;
            this.word = this.GetWordForThisGame().ToUpper();
            this.BoardArray = this._wordleService.SetupEmptyBoard(numberOfTries, word.Length);
            this.numberOfRows = numberOfTries;
        }

        private string GetWordForThisGame()
        {
            return _wordleService.AvailableWords.ElementAt(new Random().Next(_wordleService.AvailableWords.Count));
        }

        public void UpdateBoard(string inputFromUser)
        {
            inputFromUser = inputFromUser.ToUpper();


            //continue with the board update
            for (int i = 0; i < inputFromUser.Length; i++)
            {
                this.BoardArray[currentRow, i] = inputFromUser[i];
            }

            if (inputFromUser.Equals(word))
            {
                // user won!
                PrintCelebrationMessage();
                exit();
            }

            currentRow++;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < this.BoardArray.GetLength(0); i++)
            {
                for (int j = 0; j < this.BoardArray.GetLength(1); j++)
                {
                    if (this.word[j] == this.BoardArray[i, j])
                    {
                        // letter in correct spot
                        PrintCorrectLetter(this.BoardArray[i, j]);
                    } else if (this.word.Contains(this.BoardArray[i, j]))
                    {
                        // letter in word but wrong spot
                        PrintWrongSpotLetter(this.BoardArray[i, j]);
                    }
                    else
                    {
                        PrintWrongLetter(this.BoardArray[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        public void printInstructions()
        {
            Console.WriteLine($"Guess the WORDLE in {this.numberOfRows} tries. \n");
            Console.WriteLine($"Each guess must be a valid {this.word.Length}-letter word. Hit the enter button to submit. \n");
            Console.WriteLine("After each guess, the color of the tiles will change to show how close your guess was to the word. \n");
            Console.WriteLine("Examples: \n");
            PrintCorrectLetter('W');
            PrintWrongLetter('E');
            PrintWrongLetter('A');
            PrintWrongLetter('R');
            PrintWrongLetter('Y');
            Console.WriteLine("\nThe letter W is in the word and in the correct spot.");
            Console.WriteLine();
            PrintWrongLetter('P');
            PrintWrongSpotLetter('I');
            PrintWrongLetter('L');
            PrintWrongLetter('L');
            PrintWrongLetter('S');
            Console.WriteLine("\n The letter I is in the word but in the wrong spot");
            Console.Write("\n Extra commands: !exit to close program, !help to print again the instructions \n \n \n");
        }



        private void PrintCorrectLetter(char l)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write($"[{l}]");
            Console.ResetColor();
        }

        private void PrintWrongLetter(char l)
        {
            Console.Write($"[{l}]");
            Console.ResetColor();
        }

        private void PrintWrongSpotLetter(char l)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write($"[{l}]");
            Console.ResetColor();
        }

        private void PrintWrongWordMessage()
        {
            Console.Write("\n It looks like the word you just typed is not a valid word! Make sure the lenght of your word is also correct!. If you need help, type !help");
        }

        public bool DidUserStillHasTries()
        {
            return this.currentRow <= this.numberOfRows;
        }

        private void exit()
        {
            System.Environment.Exit(1);
        }

        private void PrintCelebrationMessage()
        {
            string asciiArt = @"          .* *.               `o`o`
         *. .*              o`o`o`o      ^,^,^
           * \               `o`o`     ^,^,^,^,^
              \     ***        |       ^,^,^,^,^
               \   *****       |        /^,^,^
                \   ***        |       /
    ~@~*~@~      \   \         |      /
  ~*~@~*~@~*~     \   \        |     /
  ~*~@smd@~*~      \   \       |    /     #$#$#        .`'.;.
  ~*~@~*~@~*~       \   \      |   /     #$#$#$#   00  .`,.',
    ~@~*~@~ \        \   \     |  /      /#$#$#   /|||  `.,'
_____________\________\___\____|_/______/_________|\/\___||______";

            Console.WriteLine(asciiArt);
            Console.WriteLine("Congratulations! You just won. ");
            Console.WriteLine("If you want to try again please type: !restart");

        }

        public void PrintYouLoseMessage()
        {
            string asciiArt = @"
          _______             _        _______  _______ _________ _ 
|\     /|(  ___  )|\     /|  ( \      (  ___  )(  ____ \\__   __/( )
( \   / )| (   ) || )   ( |  | (      | (   ) || (    \/   ) (   | |
 \ (_) / | |   | || |   | |  | |      | |   | || (_____    | |   | |
  \   /  | |   | || |   | |  | |      | |   | |(_____  )   | |   | |
   ) (   | |   | || |   | |  | |      | |   | |      ) |   | |   (_)
   | |   | (___) || (___) |  | (____/\| (___) |/\____) |   | |    _ 
   \_/   (_______)(_______)  (_______/(_______)\_______)   )_(   (_)
                                                                    

";
            Console.WriteLine(asciiArt);
            Console.WriteLine("Better luck next time! If you want to try again please type: !restart");
            this.currentRow = 0;
        }


        public void ProccessUserInput(string input)
        {
            switch (input)
            {
                case "!exit":
                    this.exit();
                    break;
                case "!help":
                    printInstructions();
                    break;
                case "!restart":
                    this.word = this.GetWordForThisGame().ToUpper();
                    this.BoardArray = this._wordleService.SetupEmptyBoard(this.numberOfRows, word.Length);
                    this.currentRow = 0;
                    PrintBoard();

                    break;
                default:
                    if (_wordleService.IsWordValid(input, this.word.Length))
                    {
                        UpdateBoard(input);
                        PrintBoard();
                    }else
                    {
                        PrintWrongWordMessage();
                    }
                    break;
            }
        }


    }
}
