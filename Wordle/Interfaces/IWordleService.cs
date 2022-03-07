using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Interfaces
{
    internal interface IWordleService
    {
        bool IsWordValid(string word, int desiredLength);
        char[,] SetupEmptyBoard(int x, int y);



    }
}
