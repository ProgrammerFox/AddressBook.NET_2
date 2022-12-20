using AddressBook_dotnet6.Model;
using AddressBook_Dotnet6.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
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
            { "exit", Exit }
        };

        public State Add()
        {
            try
            {
                var person = new T();

                Communicator.Write("Name: ", "");
                person.Name = Communicator.Read();

                Communicator.Write("Surname: ", "");
                person.Surname = Communicator.Read();

                Communicator.Write("Address: ", "");
                person.Address = Communicator.Read();

                AddressBook.Add(person);

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
