using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ns{
	public class DumpCommand : ICommand{
		public void Execute(){
			PropertyInfo[] propertyInfos = typeof(Person).GetProperties();
			
			Console.Write("\nDumping Person\n");
			foreach(var obj in Program.people){
				foreach(var info in propertyInfos){
					Console.WriteLine("|"+info.Name);
					Console.WriteLine("> "+info.GetValue(obj)+"\n");}
				Console.Write("\n");}}}}
