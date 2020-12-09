using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ns{
	class Program{
		public static List<Person> people = new List<Person>();
		static void Main(string[] args){
			Dictionary<string, ICommand> commands = new Dictionary <string, ICommand>();
			commands["add"]=new AddCommand();
			commands["del"]=new DelCommand();
			commands["edit"]=new EditCommand();
			commands["save"]=new SaveCommand();
			commands["load"]=new LoadCommand();
			commands["search"]=new SearchCommand();
			commands["help"]=new HelpCommand();
			commands["print"]=new DumpCommand();
			
			ICommand inte;
			commands.TryGetValue("help",out inte);
			inte.Execute();
			string currentCommand = "";
			while (currentCommand != "exit"){
				Console.Write("→ ");
				currentCommand = Console.ReadLine();
				Console.Clear();
				if(commands.TryGetValue(currentCommand,out inte)){
					inte.Execute();}
				else{
					Console.Write("Zła komenda!!!\n użyj komendy help żeby wyswietlic pomoc.\n");}}}}}

