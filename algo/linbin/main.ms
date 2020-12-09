.mso latin2.tmac


.de acc*over-def
.ds \\$1 \Z'\v'(u;\w'x'*0+\En[rst]-\En[.cht])'\
\h'(u;-\En[skw]+(-\En[.w]-\w'\\$2'/2)+\En[.csk])'\\$2'
..
.de acc*under-def
.ds \\$1 \Z'\v'\En[.cdp]u'\h'(u;-\En[.w]-\w'\\$2'/2)'\\$2'
..


.acc*over-def ' \'
.acc*over-def dot \[a.]
.char \[ogon] \h'0.5m'\[ho]
.acc*under-def og \[ogon]
.char \[A,] A\*[og]
.char \[a,] a\*[og]
.char \[e,] e\*[og]
.char \[N'] N\*'
.char \[n'] n\*'
.char \[s'] s\*'
.char \[S'] S\*'
.char \[o'] o\*'
.char \[Z.] Z\*[dot]
.char \[z.] z\*[dot]

\"\[A,]\0\0\[N']\0\0\[Z.]
\".LP
\"\[a,]
\"\[e,]
\"\[n']
\"\[o']
\"\[l/]
\"
\".LP
\"ąęćłóśćń
.TL
	Porównanie wszukiwania liniowegi i binarnego.
.AU
Adam Prycki

.NH
Kod motod wyszukiwania
.NH 2
Kod metod szukania binarnego

.LP
private int bsr(int x,int n, int r){
.LP
	int i_hr=(int)Math.Floor((decimal)(r/2));
.LP
	if(r==0)return(-1);
.LP
	if(this.ia_arr[n]==x){return(n);}
.LP
	else if(this.ia_arr[n]<x){return(bsr(x,n+i_hr,i_hr));}
.LP
	else if(this.ia_arr[n]>x){return(bsr(x,n-i_hr,i_hr));}
.LP
	else{return(-1);}}
.LP
public int bsearch(int x){
.LP
	int r=this.ia_arr.Length;
.LP
	if(this.ia_arr[this.ia_arr.Length-1]<x)return(-1);
.LP
	int i_hr=(int)Math.Floor((decimal)(r)/2);
.LP
	return(bsr(x,i_hr,i_hr));}

.NH 2
Kod metody szukania liniowego
.LP
public int linsearch(int x){
.LP
	for(int i=0;i<this.ia_arr.Length;i++){
.LP
		if(this.ia_arr[i]==x){
.LP
			return(i);}}
.LP
	return(-1);}

.NH
Metoda testowania
.PP
Program tworzy nowy obiekt z tablic\[a,] okre\[s']lonego rozmiaru, i wypełnia j\[a,] liczbami z zakresy od 0 do array.Length-1. Program losuje liczb\[e,] z takiego samego poprzedzialu i szuka jaj w tablicy za pomoca dwóch funkcji: binsearch (szukanie binarne) i linsearch(szukanie liniowe)

.bp
.NH
Wynik porównania
.PP
Os ox w skali logarytmicznej, jednoskta: ms
.PP
Os oy: nr testu

.EQ
delim $$
.EN
.NH 2
Rozmiar tablicy: $2 sup 4 = 16$
.PSPIC pic/4.eps

.SH
Srednia
.LP
.ti +1m
wyszukiwanie binarne:		0,0190ms
.ti +1m
wyszukiwanie liniowe:		0,0008ms


.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0001ms
.ti +2m
maksymalny czas		0,0001ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,0001ms
.ti +2m
maksymalny czas		0,0001ms


.bp
.NH 2
Rozmiar tablicy: $2 sup 8 = 256$
.PSPIC pic/8.eps

.SH
Srednia
.PP
.ti +1m
wyszukiwanie binarne:		0,0110ms
.ti +1m
wyszukiwanie liniowe:		0,0006ms

.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0002ms
.ti +2m
maksymalny czas		0,0007ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,0001ms
.ti +2m
maksymalny czas		0,0006ms


.bp
.NH 2
Rozmiar tablicy: $2 sup 12 = 4096$
.PSPIC pic/12.eps

.SH
Srednia
.PP
.ti +1m
wyszukiwanie binarne:		0,0114ms
.ti +1m
wyszukiwanie liniowe:		0,0020ms

.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0003ms
.ti +2m
maksymalny czas		0,0078ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,001ms
.ti +2m
maksymalny czas		0,082ms


.bp
.NH 2
Rozmiar tablicy: $2 sup 16 = 65536$
.PSPIC pic/16.eps

.SH
Srednia
.PP
.ti +1m
wyszukiwanie binarne:		0,0118ms
.ti +1m
wyszukiwanie liniowe:		0,0272ms

.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0004ms
.ti +2m
maksymalny czas		0,00864ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,0001ms
.ti +2m
maksymalny czas		0,0921ms


.bp
.NH 2
Rozmiar tablicy: $2 sup 20 = 1048576$
.PSPIC pic/20.eps

.SH
Srednia
.PP
.ti +1m
wyszukiwanie binarne:		0,0153ms
.ti +1m
wyszukiwanie liniowe:		0,4456ms

.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0021ms
.ti +2m
maksymalny czas		1,3630ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,0001ms
.ti +2m
maksymalny czas		1,4290ms


.bp
.NH 2
Rozmiar tablicy: $2 sup 24 = 16777216$
.PSPIC pic/24.eps

.SH
Srednia
.PP
.ti +1m
wyszukiwanie binarne:		0,0163ms
.ti +1m
wyszukiwanie liniowe:		7,3850ms

.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0016ms
.ti +2m
maksymalny czas		22,3701ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,0001ms
.ti +2m
maksymalny czas		22,4501ms


.bp
.NH 2
Rozmiar tablicy: $2 sup 28 = 268435456$
.PSPIC pic/28.eps

.SH
Srednia
.PP
.ti +1m
wyszukiwanie binarne:		0,0175ms
.ti +1m
wyszukiwanie liniowe:		111,5705ms

.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0017ms
.ti +2m
maksymalny czas		352,4249ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,0002ms
.ti +2m
maksymalny czas		342,3601ms


.bp
.NH 2
Rozmiar tablicy: $2 sup 30 = 1073741824$
.PSPIC pic/30.eps

.SH
Srednia
.PP
.ti +1m
wyszukiwanie binarne:		0,0232ms
.ti +1m
wyszukiwanie liniowe:		531,0550ms

.SH
Skrajane przypadki
.PP
.ti +1m
wyszukiwanie binarne:
.ti +2m
minimalny czas		0,0026ms
.ti +2m
maksymalny czas		1377,8446ms

.PP
.ti +1m
wyszukiwanie liniowe:
.ti +2m
minimalny czas		0,001ms
.ti +2m
maksymalny czas		1391,2934ms
.EQ
delim off
.EN

.bp
.NH 2
Sredni czas szukania losowego elementu
.PP
Os ox w skali logarytmicznej, jednoskta: ms
.PP
Os oy w skali logarytmicznej, jednostka: wykładnik rozmiary tablicy o podstawie 2
.EQ
2 sup x = rozmiar
.EN

.PSPIC pic/avg.eps

.NH
Wynik
.PP
Funkcja wyszukiwania liniowego zachowóje sie w sposób liniowy od x=13. Spełnia oczekiwania, jej zlo\[z.]ono\[s']ć liniowa, czyli O(n). (obie osie wykresu s\[a,] w skali logarytmicznej).
.PP
Czas wykonania funkcji wyszukiwania binarnego ro\[s']nie bardzo powoli. Podniesienie rozmiaru tablicy do pot\[e,]gi dodaje tylko jedn\[a,] iteracje do wykonania. 
Jest to zło\[z.]ono\[s']ć logarytmiczna. O(log n)
.PP
W najgorszym przypadku obie funkcje zachowój\[a,] sie w sposób liniowy O(n).  


.bp
.PP
.PSPIC pic/avg2.eps
.ti +6
Wykres \[s']redniej pr\[e,]dko\[s']ci wyszukiwania binarnego w zale\[z.]no\[s']ci od wielko\[s']ci tablicy.


.bp
.NH 
Kod zródłowy programu
.LP
using System;
.LP
using System.Diagnostics;
.LP
using System.Threading;
.LP

.LP
namespace std{
.LP
	class Arr{
.LP
		private int[] ia_arr=null;
.LP
		
.LP
		public Arr(int size){
.LP
			this.ia_arr = new int[size];}
.LP
		public void fill_incr(){
.LP
			for(int i=0;i<this.ia_arr.Length;i++){
.LP
			this.ia_arr[i]=i;}}
.LP
		public int get_size(){return(this.ia_arr.Length);}
.LP
		public void piirnt(){
.LP
			for(int i=0;i<this.ia_arr.Length;i++){
.LP
				Console.Write("{0}\n",this.ia_arr[i]);}}
.LP
		public int linsearch(int x){
.LP
			for(int i=0;i<this.ia_arr.Length;i++){
.LP
				if(this.ia_arr[i]==x){
.LP
					return(i);}}
.LP
			return(-1);}
.LP
		private int bsr(int x,int n, int r){
.LP
			int i_hr=(int)Math.Floor((decimal)(r/2));
.LP
			if(r==0)return(-1);
.LP
			if(this.ia_arr[n]==x){return(n);}
.LP
			else if(this.ia_arr[n]<x){return(bsr(x,n+i_hr,i_hr));}
.LP
			else if(this.ia_arr[n]>x){return(bsr(x,n-i_hr,i_hr));}
.LP
			else{return(-1);}
.LP
		}
.LP
		public int bsearch(int x){
.LP
			int r=this.ia_arr.Length;
.LP
			if(this.ia_arr[this.ia_arr.Length-1]<x)return(-1);
.LP
			int i_hr=(int)Math.Floor((decimal)(r)/2);
.LP
			return(bsr(x,i_hr,i_hr));}
.LP
	}
.LP
	class std{
.LP
		static void dostuff(int size){
.LP
			Random rnd=new Random();
.LP
			Arr kek = new Arr(2<<size);
.LP

.LP
			for(int i=0;i<100;i++){
.LP
				int i_rnd=rnd.Next(kek.get_size())-1;
.LP
				Console.Write("{0}   \t{1}\t",i_rnd,kek.get_size());
.LP
				kek.fill_incr();
.LP
				Stopwatch sw = Stopwatch.StartNew();
.LP
				kek.bsearch(i_rnd);
.LP
				sw.Stop();
.LP
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);
.LP
				sw.Restart();
.LP
				kek.linsearch(i_rnd);
.LP
				sw.Stop();
.LP
				Console.Write("{0}\n",sw.Elapsed.TotalMilliseconds);
.LP
			}}
.LP
		static void dostuff_minmax(int size){
.LP
			Arr kek = new Arr(2<<size);
.LP

.LP
			for(int i=0;i<100;i++){
.LP
				Console.Write("{0}\t",kek.get_size());
.LP
				kek.fill_incr();
.LP

.LP
				Stopwatch sw = Stopwatch.StartNew();
.LP
				kek.bsearch(kek.get_size()/2);
.LP
				sw.Stop();
.LP
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);
.LP

.LP
				sw.Restart();
.LP
				kek.bsearch(kek.get_size()-1);
.LP
				sw.Stop();
.LP
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);
.LP

.LP
				sw.Restart();
.LP
				kek.linsearch(0);
.LP
				sw.Stop();
.LP
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);
.LP

.LP
				sw.Restart();
.LP
				kek.linsearch(kek.get_size()-1);
.LP
				sw.Stop();
.LP
				Console.Write("{0}\n",sw.Elapsed.TotalMilliseconds);
.LP
			}}
.LP
		static void Main(string[] args){
.LP
		        if(args==null)Console.WriteLine("errr -2");
.LP
			int size=int.Parse(args[0]);
.LP
			int type=int.Parse(args[1]);
.LP
			switch(type){
.LP
				case 0: dostuff(size-1);break;
.LP
				case 1: dostuff_other_stuff(size-1);break;
.LP
			break;
.LP
			}
.LP
			
.LP
		}
.LP
	}
.LP
}
