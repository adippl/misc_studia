
using System;
using System.Diagnostics;
using System.Threading;

namespace std{
	class std{
		static bool p_basic(uint n){
			if(n<2)return(false);
			else if(n<4)return(true);
			else if(n%2==0)return(false);
			else for(uint i=3; i<n/2; i+=2)
				if(n%i==0)return(false);
			return(true);}
		static bool p_2(uint n){
			if(n<2)return(false);
			else if(n<4)return(true);
			else if(n%2==0)return(false);
			else for(uint i=3; i*i<n; i+=2)
				if(n%i==0)return(false);
			return(true);}

		static void dostuff_avg(int size){

				sw.Restart();
				kek.linsearch(0);
				sw.Stop();
				Console.Write("{0}\t",sw.Elapsed.TotalMilliseconds);


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
