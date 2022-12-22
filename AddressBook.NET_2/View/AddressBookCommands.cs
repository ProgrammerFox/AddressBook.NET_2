using AddressBook_dotnet6.Model;
using AddressBook_Dotnet6.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_Dotnet6.View
{
    public class AddressBookCommands<T>
        where T : BasePerson, new()
    {
        public AddressBook<T> AddressBook { get; set; }

        public ICommunicator Communicator { get; set; }

        public AddressBookCommands(AddressBook<T> addressBook, ICommunicator communicator)
        {
            AddressBook = addressBook;
            Communicator = communicator;
        }

        public Dictionary<string, Command> Commands =>
        new()
        {
            { "add", Add },
            { "show", Show },
            { "delete", Delete },
            { "exit", Exit }
        };

        public State Add()
        {
            try
            {
                AddressBook.Add(new T 
                {
                    Name = Communicator.Read("Name: "),
                    Surname = Communicator.Read("Surname: "),
                    Address = Communicator.Read("Address: ")
                });

                return State.Ok;
            }
            catch
            {
                return State.Error;
            }
            
        }

        public State Show()
        {
            try
            {
                Communicator.Write(AddressBook.AsTable());

                return State.Ok;
            }
            catch
            {
                return State.Error;
            }
        }

        public State Delete()
        {
            try
            {
                AddressBook.RemoveAt(int.Parse(Communicator.Read("Id: ")) - 1);

                return State.Ok;
            }
            catch
            {
                return State.Error;
            }
        }

        public State Exit()
        {
            return State.Stop;
        }

        public ICommander GenerateCommander()
        {
            return new CliCommander(Communicator, Commands);
        }
    }
}
