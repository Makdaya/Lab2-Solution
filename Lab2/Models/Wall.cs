using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Lab2.Models
{
    class Wall:LevelElement
    {
        public Wall()
        {
            Symbol = '#';
            Color = ConsoleColor.Gray;       
        }
    }
}
