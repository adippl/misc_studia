using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ns{
	public class HelpCommand : ICommand{
		public void Execute(){
			Console.Write("komendy: \n help - wyswietla ten ekran\n");
			Console.Write("  add - dodaje rekord\n");
			Console.Write("  del - pyta o imie i usuwa (wszystkie pasujące rekordy) \n");
			Console.Write("  save - zapisuje(overwrites) list rekordów do pliku save.xml \n");
			Console.Write("  load - wczytuje list rekordów z pliku save.xml (kasuje liste x pamięci) \n");
			Console.Write("  search - pyta o imie i wyświetla wszystkie recordy z pasującymi polami (case insensitive) \n");
			Console.Write("  edit - pyta o imie i edytuje wszystkie pola rekordu(dla wszystkich pasujących rekordów) \n");
			Console.Write("  exit - wyjscie z programu (nie zapisuje) \n");
			Console.Write("  print - wypisuje całą liste rekordów do konsoli \n");}}}
