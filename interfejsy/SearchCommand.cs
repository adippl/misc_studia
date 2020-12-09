using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ns{
	public class SearchCommand : ICommand{
		private List<Person> search(string s){
			var comp=StringComparison.OrdinalIgnoreCase;
			var query = from person in Program.people
						where person.imie.Contains(s,comp) || 
						person.nazwisko.Contains(s,comp) ||
						person.sex.Contains(s,comp) ||
						person.wiek.Contains(s,comp) ||
						person.kodPocztowy.Contains(s,comp) ||
						person.miasto.Contains(s,comp) ||
						person.ulica.Contains(s,comp) ||
						person.nrDomu.Contains(s,comp) ||
						person.nrMieszkania.Contains(s,comp) 
						select person;
			List<Person> list=query.ToList<Person>();
			return(list);}
		
		private void print(List<Person> l){
			PropertyInfo[] propertyInfos = typeof(Person).GetProperties();
			Console.Write("\nPrinting Person\n");
			foreach(var obj in l){
				foreach(var info in propertyInfos){
					Console.WriteLine(info.Name);
					Console.WriteLine(info.GetValue(obj));}
				Console.Write("\nEndOfObj\n\n");}}

		public void Execute(){
			string searchString="";
			Console.Write("Enter a string for query\n");
			searchString=Console.ReadLine();
			print(search(searchString));}}}
