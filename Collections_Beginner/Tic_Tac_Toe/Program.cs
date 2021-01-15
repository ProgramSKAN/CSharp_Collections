using System;

namespace Tic_Tac_Toe
{
    //JAGGED ARRAY : An array of arrays Square[][].it is called jagged because inner array may not be of same size.
    class Program
    {

        static void Main(string[] args)
        {
            Game game = new Game();
            game.PlayGame();
            Console.WriteLine("Game over");
        }
    }

}
