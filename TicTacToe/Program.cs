using System;
using System.Linq;

namespace TicTacToe
{
    enum Player
    {
        X = 'X',
        O = 'O'
    }

    class TicTacToe
    {
        public static void Main()
        {
            char[,] board = {
                {' ', ' ', ' '},
                {' ', ' ', ' '},
                {' ', ' ', ' '}
            };
            Console.Write("\u001b[33mPlayer 1 [O or X]: \u001b[m\u001b[32m");
            char player = Convert.ToChar(Console.ReadLine().ToUpper());
            while (!Enum.IsDefined(typeof(Player), (int)player))
            {
                Console.Write("\u001b[33mPlayer 1 [O or X]: \u001b[m\u001b[32m");
                player = Convert.ToChar(Console.ReadLine().ToUpper());
            }

            bool loopCompleted = true;
            short play;
            for (int i = 0; i < 9; i++)
            {
                PrintGame(board);

                Console.Write("\u001b[34m- Enter a location \u001b[m\u001b[31m(1-9)\u001b[m\u001b[34m: \u001b[m\u001b[32m");
                play = (short)(Convert.ToInt16(Console.ReadLine()) - 1);

                while (!ValidLocal(board, play))
                {
                    Console.WriteLine("\u001b[31mInvalid location, please try again.\u001b[m");
                    Console.Write("\u001b[34m- Enter a location \u001b[m\u001b[31m(1-9)\u001b[m\u001b[34m: \u001b[m\u001b[32m");
                    play = (short)(Convert.ToInt16(Console.ReadLine()) - 1);
                }

                board[play / 3, play % 3] = player;
                if (CheckWin(board))
                {
                    PrintGame(board);
                    Console.WriteLine($"\u001b[36m - Game finished! \u001b[m\n \u001b[32m- The Winner was: \u001b[m\u001b[33m{player}\u001b[m");
                    loopCompleted = false;
                    break;
                }
                player = player == (char)Player.X ? (char)Player.O : (char)Player.X;
            }

            if (loopCompleted)
            {
                PrintGame(board);
                Console.WriteLine("\u001b[30m - It's a tie!\u001b[m");
            }
        }

        public static bool CheckWin(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ') return true;

                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' ') return true;
            }

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ') return true;
            
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[2, 0] != ' ') return true;

            return false;
        }

        public static bool ValidLocal(char[,] board, short play)
        {
            return 9 > play && play >= 0 && board[play / 3, play % 3] == ' ';
        }

        public static void PrintGame(char[,] board)
        {
            for (int row = 0; row < 3; row++)
            {
                Console.WriteLine($"\u001b[30m{string.Join(" | ", Enumerable.Range(0, 3).Select(col => board[row, col]))}\u001b[m");
                Console.WriteLine($"\u001b[30m{string.Concat(Enumerable.Repeat("-", 9))}\u001b[m");
            }
        }
    }
}