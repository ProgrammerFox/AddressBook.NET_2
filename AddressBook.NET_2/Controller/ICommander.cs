using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_Dotnet6.Controller
{
    public enum State
    {
        None,
        Ok,
        Error,
        Stop
    }

    public delegate State Command();

    public interface ICommander
    {
        void Step();
    }
}
