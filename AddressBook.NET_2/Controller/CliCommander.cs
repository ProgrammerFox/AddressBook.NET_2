using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_Dotnet6.Controller
{
    public class CliCommander : ICommander
    {
        protected ICommunicator Communicator { get; set; }

        protected Dictionary<string, Command> Commands { get; set; }

        public CliCommander(ICommunicator communicator, Dictionary<string, Command> commands)
        {
            Communicator = communicator;
            Commands = commands;
        }

        public void Step()
        {
            Communicator.Write("AddressBook>", " ");

            string command = Communicator.Read().ToLower();


            if (Commands.ContainsKey(command))
            {
                State state = Commands[command]();

                switch (state)
                {
                    case State.Error:
                        Communicator.Write("An error occurred while executing the command.");
                        break;
                    case State.Stop:
                        return;
                }
            }
            else if(command != "")
            {
                Communicator.Write($"Error. Command \"{command}\" don't found.");
            }

            Step();
        }
    }
}
