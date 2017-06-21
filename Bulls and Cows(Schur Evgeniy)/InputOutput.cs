using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulls_and_Cows
{
    
    class InputOutput:IReadLine,IPrintLine
    {
        GameLogic Logic = new GameLogic();
        public string ReadLine(int module)
        {
           var answer =  Console.ReadLine();
           return Logic.Errors(answer, module) ? "Error" : answer;
        }
        public void PrintLine(object o) => Console.WriteLine($"* {o} *");

    }
}
