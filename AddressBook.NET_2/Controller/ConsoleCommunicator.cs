using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_Dotnet6.Controller
{
    public class ConsoleCommunicator : ICommunicator
    {
        public string Read()
        {
            return Console.ReadLine() ?? "";
        }

        public void Write(string text, string end = "\n")
        {
            Console.Write(text + end);
        }
    }
}
