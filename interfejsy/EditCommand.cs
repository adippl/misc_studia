using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns{
	public class EditCommand : ICommand{
		public void Execute(){
			PropertyInfo[] propertyInfos = typeof(Person).GetProperties();

			string searchString="";
			Console.Write("Enter name for query (matching records will be deleted )\n");
			searchString=Console.ReadLine();
			for(int i=Program.people.Count-1;i>=0;i--){
				if(String.Equals(searchString,Program.people[i].imie)){
					foreach(var info in propertyInfos){
						Console.WriteLine("Setting " + info.Name);
						string value = Console.ReadLine();
						info.SetValue(Program.people[i], Convert.ChangeType(value, info.PropertyType));}}}}}}
