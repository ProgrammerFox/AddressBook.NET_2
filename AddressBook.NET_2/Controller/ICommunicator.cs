using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_Dotnet6.Controller
{
    public interface ICommunicator
    {
        string Read();

        void Write(string text, string end = "\n");
    }
}
