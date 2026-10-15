using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.Models
{
    internal class Dice
        
    {
        private int numberOfDice;
        private int sidesPerDice;
        private int modifier;
        private static readonly Random random = new Random();
            
        public Dice(int numberOfDice, int sidesPerDice, int modifier)
        {
            this.numberOfDice = numberOfDice;
            this.sidesPerDice = sidesPerDice;
            this.modifier = modifier;
            
        }
        public int Throw()
        {
            int total = 0;
            
            for (int i = 0; i < numberOfDice; i++)
            {
                int roll = random.Next(1, sidesPerDice + 1);
                total += roll;
            }
            return total+ modifier;
        }
    }
}
