using AddressBook_dotnet6.Model;
using AddressBook_Dotnet6.View;

var addressBookCommands = new AddressBookCommands<BasePerson>(new AddressBook<BasePerson>(), new ConsoleCommunicator());

var commander = addressBookCommands.GenerateCommander();

commander.Step();