using System;
using System;
using System.Diagnostics;
using System.Threading;

namespace std{
	class Arr{
using System.Diagnostics;
using System.Threading;

namespace std{
	class Arr{
		private int[] ia_arr=null;
		
		public Arr(int size){
			this.ia_arr = new int[size];}
		public void fill_incr(){
			for(int i=0;i<this.ia_arr.Length;i++){
			this.ia_arr[i]=i;}}
		public int get_size(){return(this.ia_arr.Length);}
		public void piirnt(){
			for(int i=0;i<this.ia_arr.Length;i++){
				Console.Write("{0}\n",this.ia_arr[i]);}}
		public int linsearch(int x){
			for(int i=0;i<this.ia_arr.Length;i++){
				if(this.ia_arr[i]==x){
					return(i);}}
			return(-1);}
		private int bsr(int x,int n, int r){
			int i_hr=(int)Math.Floor((decimal)(r/2));
			//	Console.Write("x={0}\tn={1}\tr={2}\ti-hr={3} arr[n]={4}\n",x,n,r,i_hr,this.ia_arr[n]);
			if(r==0)return(-1);
			if(this.ia_arr[n]==x){return(n);}
			else if(this.ia_arr[n]<x){return(bsr(x,n+i_hr,i_hr));}
			else if(this.ia_arr[n]>x){return(bsr(x,n-i_hr,i_hr));}
			else{return(-1);}
		}
		public int bsearch(int x){
			int r=this.ia_arr.Length;
			if(this.ia_arr[this.ia_arr.Length-1]<x)return(-1);
			int i_hr=(int)Math.Floor((decimal)(r)/2);
			return(bsr(x,i_hr,i_hr));}
	}
	class std{
		static void dostuff(int size){
			Random rnd=new Random();
			Arr kek = new Arr(2<<size);

			for(int i=0;i<100;i++){
				int i_rnd=rnd.Next(kek.get_size())-1;
				Console.Write("{0}   \t{1}\t",i_rnd,kek.get_size());
				kek.fill_incr();
				Stopwatch sw = Stopwatch.StartNew();
				kek.bsearch(i_rnd);
				sw.Stop();
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);
				sw.Restart();
				kek.linsearch(i_rnd);
				sw.Stop();
				Console.Write("{0}\n",sw.Elapsed.TotalMilliseconds);
			}}
		static void dostuff_avg(int size){
			Random rnd=new Random();
			Arr kek = new Arr(2<<size);
			double[,] f_ar = new double[2,100];

			for(int i=0;i<100;i++){
				int i_rnd=rnd.Next(kek.get_size())-1;
				//Console.Write("{0}   \t{1}\t",i_rnd,kek.get_size());
				kek.fill_incr();
				Stopwatch sw = Stopwatch.StartNew();
				kek.bsearch(i_rnd);
				sw.Stop();
				//Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);
				f_ar[0,i]=sw.Elapsed.TotalMilliseconds;
				sw.Restart();
				kek.linsearch(i_rnd);
				sw.Stop();
				//Console.Write("{0}\n",sw.Elapsed.TotalMilliseconds);
				f_ar[1,i]=sw.Elapsed.TotalMilliseconds;
			}
			double tmp=0.0;
			for(int i=0;i<100;i++){
				tmp=tmp+f_ar[0,i];
			}
			Console.Write("{0}\t",tmp/100);
			
			tmp=0.0;
			for(int i=0;i<100;i++){
				tmp=tmp+f_ar[1,i];
			}
			Console.Write("{0}\n",tmp/100);
			}
		static void dostuff_minmax(int size){
			Arr kek = new Arr(2<<size);

			for(int i=0;i<100;i++){
				Console.Write("{0}\t",kek.get_size());
				kek.fill_incr();

				Stopwatch sw = Stopwatch.StartNew();
				kek.bsearch(kek.get_size()/2);
				sw.Stop();
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);

				sw.Restart();
				kek.bsearch(kek.get_size()-1);
				sw.Stop();
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);

				sw.Restart();
				kek.linsearch(0);
				sw.Stop();
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);

				sw.Restart();
				kek.linsearch(kek.get_size()-1);
				sw.Stop();
				Console.Write("{0}\n",sw.Elapsed.TotalMilliseconds);


			}}
		static void Main(string[] args){
		        if(args==null)Console.WriteLine("errr -2");
			int size=int.Parse(args[0]);
			int type=int.Parse(args[1]);
			switch(type){
				case 0: dostuff(size-1);break;
				case 1: dostuff_minmax(size-1);break;
				case 2: dostuff_avg(size-1);break;
			break;
			}
			
		}
	}
}
