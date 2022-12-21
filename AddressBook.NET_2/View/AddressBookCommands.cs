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

        public State Show()
        {
            try
            {
                if (!AddressBook.Any()) return State.Ok;

                string bookAsString = AddressBook[0].ToJson()
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace("\"", "")
                    .Replace(" ", "");

                string[] headers = bookAsString.Split(',')
                    .Select(x => x.Split(':')[0])
                    .ToArray();

                List<string[]> bookTable = AddressBook.Select(x => x.ToJson()
                                                .Replace("{", "")
                                                .Replace("}", "")
                                                .Replace("\"", "")
                                                .Split(',')
                                                .Select(x => x.Split(':')[1])
                                                        .ToArray())
                                                .ToList();

                var spaces = headers.Select(x => x.Length).ToArray();

                foreach (var bookRow in bookTable)
                {
                    for (int i = 0; i < bookRow.Length; i++)
                    {
                        spaces[i] = Math.Max(spaces[i], bookRow[i].Length);
                    }
                }

                var headerString = new StringBuilder($"+-{"-".Repeat(spaces.Sum() + (spaces.Length - 1) * 3)}-+\n");

                var newHeaders = headers.Zip(spaces, (r, s) => r + " ".Repeat(s - r.Length)).ToArray();
                headerString.AppendLine($"| {string.Join(" | ", newHeaders)} |");

                headerString.AppendLine($"|-{"-".Repeat(spaces.Sum() + (spaces.Length - 1) * 3)}-|");

                foreach (var bookRow in bookTable)
                {
                    var newBookRow = bookRow.Zip(spaces, (r, s) => r + " ".Repeat(s - r.Length)).ToArray();
                    headerString.AppendLine($"| {string.Join(" | ", newBookRow)} |");
                }

                headerString.AppendLine($"+-{"-".Repeat(spaces.Sum() + (spaces.Length - 1) * 3)}-+");

                Communicator.Write(headerString.ToString());

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
