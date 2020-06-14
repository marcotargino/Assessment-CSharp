using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

namespace Aniversario
{
    class Program
    {
        public static void Main(string[] args)
        {
            Presentation.MainMenu();
        }

        /////////////////////////CHANGE/////////////////////////
        public static void Shift()
        {
            Clear();


        }
        /////////////////////////EXIT/////////////////////////
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
