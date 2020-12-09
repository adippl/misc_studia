using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ns{
	public class LoadCommand : ICommand{
		public void Execute(){
			Program.people.Clear();
			var ser = new XmlSerializer(typeof(List<Person>));
			var file=new FileStream("save.xml", FileMode.Open);
			Program.people=(List<Person>)ser.Deserialize(file);
			file.Close();}}}
