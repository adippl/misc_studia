using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ns{
	public class AddCommand : ICommand{
		public void Execute(){
			Person person = new Person();
			PropertyInfo[] propertyInfos = typeof(Person).GetProperties();
			foreach (var info in propertyInfos){
				Console.WriteLine("Setting " + info.Name);
				string value = Console.ReadLine();
				info.SetValue(person, Convert.ChangeType(value, info.PropertyType));}
			Program.people.Add(person);}}}
