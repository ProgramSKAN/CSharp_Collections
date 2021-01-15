using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    public enum Player
    {
        None=0,
        Noughts,
        Crosses
    }

    //struct takes their default values without newing up
    public struct Square
    {
        public Player Owner { get; }
        public Square(Player owner)
        {
            this.Owner = owner;
        }
        public override string ToString()
        {
            switch (Owner)
            {
                case Player.None:
                    return ".";
                case Player.Crosses:
                    return "X";
                case Player.Noughts:
                    return "O";
                default:
                    throw new Exception("Invalid State");
            }
        }
    }
}
