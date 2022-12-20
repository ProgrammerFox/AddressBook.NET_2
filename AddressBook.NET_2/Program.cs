using AddressBook_dotnet6.Model;
using AddressBook_Dotnet6.View;

Console.WriteLine(@"Address Book by
 ____                                                                       _____              
|  _ \  _ __   ___    __ _  _ __   __ _  _ __ ___   _ __ ___    ___  _ __  |  ___|  ___  __  __
| |_) || '__| / _ \  / _` || '__| / _` || '_ ` _ \ | '_ ` _ \  / _ \| '__| | |_    / _ \ \ \/ /
|  __/ | |   | (_) || (_| || |   | (_| || | | | | || | | | | ||  __/| |    |  _|  | (_) | >  < 
|_|    |_|    \___/  \__, ||_|    \__,_||_| |_| |_||_| |_| |_| \___||_|    |_|     \___/ /_/\_\
                     |___/                                                                     ");

var addressBookCommands = new AddressBookCommands<BasePerson>(new AddressBook<BasePerson>(), new ConsoleCommunicator());

var commander = addressBookCommands.GenerateCommander();

commander.Step();