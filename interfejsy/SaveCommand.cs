using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ns{
	public class SaveCommand : ICommand{
		public void Execute(){
			var ser = new XmlSerializer(typeof(List<Person>));
			FileStream file = File.Create("save.xml");
			ser.Serialize(file,Program.people);
			file.Close();}}
}
