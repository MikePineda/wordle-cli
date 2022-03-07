using Wordle;
using Wordle.Services;


// initial setup
WordleService service = new WordleService(); // DI?
int NumberOfTries = 6; // Current value in wordle game
// setup board
Board board = new Board(NumberOfTries, service);



string? userInput;
board.printInstructions();
board.PrintBoard();
while (true)
{
    if (board.DidUserStillHasTries())
    {
        Console.WriteLine("\n Type the word you think it could be");
        userInput = Console.ReadLine();
        if(userInput == null)
        {
            continue;
        }
        board.ProccessUserInput(userInput);
    }
    else
    {
        board.PrintYouLoseMessage();
    }

}
