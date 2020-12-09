using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns{
	public class Person{
		private string _sex;
		private string _wiek;
		private string _imie;
		private string _nazwisko;
		
		private string _kodPocztowy;
		private string _miasto;
		private string _ulica;
		private string _nrDomu;
		private string _nrMieszkania;
		
		public string sex{
			get{return(_sex);}
			set{_sex=value;}}
		public string wiek{
			get{return(_wiek);}
			set{_wiek=value;}}
		public string imie{
			get{return(_imie);}
			set{_imie=value;}}
		public string nazwisko{
			get{return(_nazwisko);}
			set{_nazwisko=value;}}
		public string kodPocztowy{
			get{return(_kodPocztowy);}
			set{_kodPocztowy=value;}}
		public string miasto{
			get{return(_miasto);}
			set{_miasto=value;}}
		public string ulica{
			get{return(_ulica);}
			set{_ulica=value;}}
		public string nrDomu{
			get{return(_nrDomu);}
			set{_nrDomu=value;}}
		public string nrMieszkania{
			get{return(_nrMieszkania);}
			set{_nrMieszkania=value;}}}}
