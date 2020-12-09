using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ns{
	public class DelCommand : ICommand{
		public void Execute(){
			string searchString="";
			Console.Write("Enter name for query (matching records will be deleted )\n");
			searchString=Console.ReadLine();
			for(int i=Program.people.Count-1;i>=0;i--){
				if(String.Equals(searchString,Program.people[i].imie))
					Program.people.RemoveAt(i);}}}}
