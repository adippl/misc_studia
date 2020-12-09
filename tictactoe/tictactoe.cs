/*	TicTacToe terminal game with simple AI
 *	Copyright (C) 2019  Adam Prycki (email: <-REDACTED-> )
 
 *	This program is free software: you can redistribute it and/or modify
 *	it under the terms of the GNU General Public License as published by
 *	the Free Software Foundation, either version 2 of the License, or
 *	(at your option) any later version.
 *	
 *	This program is distributed in the hope that it will be useful,
 *	but WITHOUT ANY WARRANTY; without even the implied warranty of
 *	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *	GNU General Public License for more details.
 *	
 *	You should have received a copy of the GNU General Public License
 *	along with this program.  If not, see <http://www.gnu.org/licenses/>
 *	
 *	Niniejszy program jest wolnym oprogramowaniem; możesz go
 *	rozprowadzać dalej i/lub modyfikować na warunkach Powszechnej
 *	Licencji Publicznej GNU, wydanej przez Fundację Wolnego
 *	Oprogramowania - według wersji 2 tej Licencji lub (według twojego
 *	wyboru) którejś z późniejszych wersji.
 *	
 *	Niniejszy program rozpowszechniany jest z nadzieją, iż będzie on
 *	użyteczny - jednak BEZ JAKIEJKOLWIEK GWARANCJI, nawet domyślnej
 *	gwarancji PRZYDATNOŚCI HANDLOWEJ albo PRZYDATNOŚCI DO OKREŚLONYCH
 *	ZASTOSOWAŃ. W celu uzyskania bliższych informacji sięgnij do
 *	Powszechnej Licencji Publicznej GNU.
 *	
 *	Z pewnością wraz z niniejszym programem otrzymałeś też egzemplarz
 *	Powszechnej Licencji Publicznej GNU (GNU General Public License);
 *	jeśli nie - zobacz <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace tictactoe{
	class ticERR : Exception {
		public ticERR(){} 
		public ticERR(string message):base(message){}}
	class EjAjERR : Exception {
		public EjAjERR(){} 
		public EjAjERR(string message):base(message){}}
	
	class EjAj{	//AI
		//AI event weithgs
		private const int weightMyWin=90;
		private const int weightEnemyWin=75;
		private const int weightMyFork=40;
		private const int weightEnemyFork=30;
		private const int weightPotentialRow=10;
		private const int weightXOXTrap=12;

		
		private int[,] table=null;
		private int[,] tableHeatmap=null;
		private int[] rowWeight=null;
		private int[] myRowWeight=null;
		private int[] opponentRowWeight=null;
		private Random rnd=null;
		private int irritation=0;

		private int evaluatedPlayer=0;
		
		public void poke(){
			this.irritation++;
			Console.Write("F**K off!!\n");
			throw new EjAjERR("you have too much free time.");}

		public EjAj(ref int[,] table){
			this.table=table;
			this.tableHeatmap=new int[3,3];
			this.rowWeight=new int[8];
			this.myRowWeight=new int[8];
			this.opponentRowWeight=new int[8];
			this.irritation=0;
			this.rnd = new Random();}

		private void debugDrawTable(){
			Console.Write("DEBUG table values\n");
			for(int y=0;y<3;y++){
				for(int x=0;x<3;x++){
					Console.Write("{0}\t",this.table[x,y]);}
				Console.Write("\n");}}

		private void debugDrawHeatTable(){
			Console.Write("DEBUG heatMap\n");
			for(int y=0;y<3;y++){
				for(int x=0;x<3;x++){
					Console.Write("{0}\t",this.tableHeatmap[x,y]);}
				Console.Write("\n");}}

		private void debugPrintRows(){
			Console.Write("DEBUG rows info\n");
			for(int i=0;i<8;i++){
				Console.Write("rowWeight[{0}]\t\t={1}\n",i,this.rowWeight[i]);
				Console.Write("myRowWeight[{0}]\t\t={1}\n",i,this.myRowWeight[i]);
				Console.Write("opponentRowWeight[{0}]\t={1}\n",i,this.opponentRowWeight[i]);}}

		private int rowAdder(int rowNumber, int x, int y){
			if(x<0||x>2||y<0||y>2)throw new EjAjERR("ai coordinates problem");
			int isFieldTaken=0;
			int fieldWeight=this.table[x,y];
			if(fieldWeight==-1){
				isFieldTaken=1;
				this.myRowWeight[rowNumber]--;}
			if(fieldWeight==1){
				isFieldTaken=1;
				this.opponentRowWeight[rowNumber]++;}
			this.rowWeight[rowNumber]+=fieldWeight;
			return(isFieldTaken);}

		private void checkRows(){
			int sum=0;
			//test for vertical
			int rowNumber=0;
			for(int x=0;x<3;x++){
				for(int y=0;y<3;y++){
					sum+=rowAdder(rowNumber,x,y);}
				if(sum==3)this.rowWeight[rowNumber]=0;
				sum=0;
				rowNumber++;}
			//test for horizontal
			for(int y=0;y<3;y++){
				for(int x=0;x<3;x++){
					sum+=rowAdder(rowNumber,x,y);}
				if(sum==3)this.rowWeight[rowNumber]=0;
				sum=0;
				rowNumber++;}
			sum=0;
			//test for diagonal (\)
			sum+=rowAdder(rowNumber,0,0);
			sum+=rowAdder(rowNumber,1,1);
			sum+=rowAdder(rowNumber,2,2);
			if(sum==3)this.rowWeight[rowNumber]=0;
			sum=0;
			rowNumber++;
			//test for diagonal (/)
			sum+=rowAdder(rowNumber,2,0);
			sum+=rowAdder(rowNumber,1,1);
			sum+=rowAdder(rowNumber,0,2);
			if(sum==3)this.rowWeight[rowNumber]=0;
			sum=0;
			rowNumber++;}

		private void addHeatToField(int x, int y, ref int addInt){
			if(this.table[x,y]==0)this.tableHeatmap[x,y]+=addInt;}

		private void addIntToHeatMap(int rowNumber, int addInt){
			if(rowNumber>7||rowNumber<0)throw new EjAjERR("invalid rowNumber");
			switch(rowNumber){
				case 0:
					for(int y=0;y<3;y++){
						int x=0;
						this.addHeatToField(x,y,ref addInt);}
					break;
				case 1:
					for(int y=0;y<3;y++){
						int x=1;
						this.addHeatToField(x,y,ref addInt);}
					break;
				case 2:
					for(int y=0;y<3;y++){
						int x=2;
						this.addHeatToField(x,y,ref addInt);}
					break;
				case 3:
					for(int x=0;x<3;x++){
						int y=0;
						this.addHeatToField(x,y,ref addInt);}
					break;
				case 4:
					for(int x=0;x<3;x++){
						int y=1;
						this.addHeatToField(x,y,ref addInt);}
					break;
				case 5:
					for(int x=0;x<3;x++){
						int y=2;
						this.addHeatToField(x,y,ref addInt);}
					break;
				case 6:
					this.addHeatToField(0,0,ref addInt);
					this.addHeatToField(1,1,ref addInt);
					this.addHeatToField(2,2,ref addInt);
					break;
				case 7:
					this.addHeatToField(2,0,ref addInt);
					this.addHeatToField(1,1,ref addInt);
					this.addHeatToField(0,2,ref addInt);
					break;
				//default:	//compiler flagged as unreachable
				//	throw new EjAjERR("addHeat() rowNumber err");
				//	break;
				}}

		private void flipRows(out int[] myRowF, out int[] enRowF, int flip){
			switch(flip){
				case 1:
					myRowF=this.opponentRowWeight;
					enRowF=this.myRowWeight;
					break;
				case -1:
					myRowF=this.myRowWeight;
					enRowF=this.opponentRowWeight;
					break;
				default:
					throw new EjAjERR("something's wrong with flipper");}}

		private void scanForWin(ref int[] myRow,ref int[] enemyRow, int playerId, int weight){
			for(int i=0;i<8;i++){
				if(myRow[i]==playerId*2&&enemyRow[i]==0)addIntToHeatMap(i,weight);
				#if DEBUG
				if(myRow[i]==playerId*2&&enemyRow[i]==0)Console.Write("WIN CONDITION for player {0} detected in row {1}\n",playerId,i);
				#endif
				}}

		private void scanForFork(ref int[] myRow,ref int[] enemyRow, int playerId,int weight){
			//scan for 110x100 fork
			for(int i=0;i<8;i++){
				if(myRow[i]==playerId*2&&enemyRow[i]==0){
						for(int j=0;j<8;j++){
							if(myRow[j]==playerId&&enemyRow[j]==0)addIntToHeatMap(j,weight);
							#if DEBUG
							if(myRow[j]==playerId&&enemyRow[j]==0)Console.Write("FORK CONDITION for player {0} detected in row {1}\n",playerId,j);
							#endif
							}}}}
		private void scanForEarlyDiagonalFork(ref int[] myRow,ref int[] enemyRow, int playerId,int weight){
			int x=0;
			int y=0;
			int i=0;
			i=6;
			if(this.myRowWeight[i]==0 && this.opponentRowWeight[i]==playerId*-1){
				Console.Write("ALAAAAARM ALLARM\n");
				x=0;
				y=0;
				if(this.table[x,y]==0)this.tableHeatmap[x,y]+=weight;
				x=2;
				y=2;
				if(this.table[x,y]==0)this.tableHeatmap[x,y]+=weight;}
			i=7;
			if(this.myRowWeight[i]==0 && this.opponentRowWeight[i]==playerId*-1){
				Console.Write("ALAAAAARM ALLARM\n");
				x=2;
				y=0;
				if(this.table[x,y]==0)this.tableHeatmap[x,y]+=weight;
				x=0;
				y=2;
				if(this.table[x,y]==0)this.tableHeatmap[x,y]+=weight;}
		}
		private void addHeatToEdges(ref int weight, int playerId){
			this.addHeatToField(1,0,ref weight);
			this.addHeatToField(0,1,ref weight);
			this.addHeatToField(2,1,ref weight);
			this.addHeatToField(1,2,ref weight);
		}
		private void scanForearlyXOXTrap(ref int[] myRow,ref int[] enemyRow, int playerId,int weight){
			int i=0;

			i=6;
			if(this.myRowWeight[i]==playerId && this.opponentRowWeight[i]==playerId*-1*2)addHeatToEdges(ref weight,playerId);
			i=7;
			if(this.myRowWeight[i]==playerId && this.opponentRowWeight[i]==playerId*-1*2)addHeatToEdges(ref weight,playerId);
		}

		private void strengthenPotentialRows(ref int[] myRow,ref int[] enemyRow, int playerId,int weight){
			for(int i=0;i<8;i++){
				if(myRow[i]==playerId*2&&enemyRow[i]==0){
					return;
					}
				}
			for(int i=0;i<8;i++){
				if(myRow[i]==playerId&&enemyRow[i]==0){
					addIntToHeatMap(i,weight);}}}

		private void scanRowsForOpportunities(){
			int[] myRowF=null;
			int[] enRowF=null;
			this.flipRows(out myRowF, out enRowF, this.evaluatedPlayer);

			this.scanForWin(ref myRowF, ref enRowF, this.evaluatedPlayer, weightMyWin);
			this.scanForWin(ref enRowF, ref myRowF, -1*this.evaluatedPlayer, weightEnemyWin);

			// scan for my fork opportunity. function scanForFork() writes to heatmap.
			this.scanForFork(ref myRowF, ref enRowF,this.evaluatedPlayer,weightMyFork);
			this.scanForFork(ref enRowF, ref myRowF,-1*this.evaluatedPlayer,weightEnemyFork);

			strengthenPotentialRows(ref myRowF, ref enRowF, this.evaluatedPlayer, weightPotentialRow);
			strengthenPotentialRows(ref enRowF, ref myRowF, -1*this.evaluatedPlayer, weightPotentialRow);
			
//			scanForEarlyDiagonalFork(ref myRowF, ref enRowF, this.evaluatedPlayer, 1);
			scanForearlyXOXTrap(ref myRowF, ref enRowF, this.evaluatedPlayer, weightXOXTrap);
			}

		private void resetRowsAndHeatMap(){
			this.tableHeatmap=new int[3,3];
			this.rowWeight=new int[8];
			this.myRowWeight=new int[8];
			this.opponentRowWeight=new int[8];}
		private void generateHeatMap(){

		for(int i=0;i<8;i++){
			// initialize heatmap, adding 1 to every empty field in a row.
			// rows are overlaping creating higher values
			this.addIntToHeatMap(i,1);}
		this.checkRows();//sum weight in rows
		// scan rows for opportunities/dangers
		this.scanRowsForOpportunities();}

		private void selectBestMove(out int xOut, out int yOut){
			int[,] solutions=new int[9,3];
			int tableIndex=0;
			int biggestWeight=0;
			for(int x=0;x<3;x++){
				for(int y=0;y<3;y++){
						if(this.tableHeatmap[x,y]==biggestWeight&&this.tableHeatmap[x,y]!=0){
							tableIndex++;
							solutions[tableIndex,0]=this.tableHeatmap[x,y];
							solutions[tableIndex,1]=x;
							solutions[tableIndex,2]=y;
							}
						if(this.tableHeatmap[x,y]>biggestWeight&&this.tableHeatmap[x,y]!=0){
							tableIndex=0;
							biggestWeight=this.tableHeatmap[x,y];
							solutions=new int[9,3];
							solutions[tableIndex,0]=this.tableHeatmap[x,y];
							solutions[tableIndex,1]=x;
							solutions[tableIndex,2]=y;}}}
			int selectedSolution=this.rnd.Next(0,tableIndex);
			xOut=solutions[selectedSolution,1];
			yOut=solutions[selectedSolution,2];
			#if DEBUG
			Console.Write("nr of solutions {0}\n",tableIndex);
			Console.Write("solution nr {0}\n",selectedSolution);
			Console.Write("x {0}\n",xOut);
			Console.Write("y {0}\n",yOut);
			#endif
			}

		public void evaluateMove(out int x, out int y, int playerId){
			#if DEBUG
			Console.Write("evaluating player {0}\n",playerId);
			#endif
			x=0;
			y=0;
			this.evaluatedPlayer=playerId;
			this.resetRowsAndHeatMap();
			this.generateHeatMap();
			#if DEBUG
			this.debugDrawTable();
			this.debugDrawHeatTable();
			this.debugPrintRows();
			#endif
			this.selectBestMove(out x,out y);
			
		}
	}

	class tic{
		private int[,] table=null;
		private int turn;
		private int victory;
		private int gamemode=0;

		private EjAj AI=null;
		#if sound
		private Audio Audio=null;
		#endif
		
		public tic(){
			this.table=new int[3,3];
			this.turn=0;
			this.victory=0;
			AI=new EjAj(ref this.table);
			#if sound
			this.Audio=new Audio();
			#endif
			}
			
		public void resetGame(){
			this.table=new int[3,3];
			this.turn=0;
			this.victory=0;
			AI=new EjAj(ref this.table);
			this.gamemode=0;
			//System.GC.Collect();	//test
			}

		public void getVictoryState(){
			switch(this.victory){
				case 0:
					Console.Write("Draw!\n");
					break;
				case 1:
					Console.Write("Player 1 won!\n");
					break;
				case -1:
					Console.Write("Player -1 won!\n");
					break;}}

		private int i(string txt){
			int a=0;
			do{
				Console.Write("{0}",txt);
			}while(int.TryParse(Console.ReadLine(), out a)==false);
			return a;}

		private int iWithRange(string txt,int gre, int less){
			int a=0;
			do{
				a=this.i(txt);
			}while( !(a>gre && a<less) );
			return a;}

		private void testCoordinates(int x, int y){
			if(x<0||x>3)throw new ticERR("coordinates out of band, somehow \\o/");
			if(y<0||y>3)throw new ticERR("coordinates out of band, somehow \\o/");}

		private int checkForWin(){
			//test for horizontal
			int sum=0;
			for(int x=0;x<3;x++){
				for(int y=0;y<3;y++){
					sum+=this.table[x,y];}
				if(sum==3||sum==-3){
					this.victory=sum/3;
				#if sound
				this.Audio.playEndGame();
				#endif
					return(this.victory);}
				sum=0;
				}
			
			//test for vertical
			sum=0;
			for(int y=0;y<3;y++){
				for(int x=0;x<3;x++){
					sum+=this.table[x,y];}
					
				if(sum==3||sum==-3){
					this.victory=sum/3;
				#if sound
				this.Audio.playEndGame();
				#endif
					return(this.victory);}
				sum=0;
				}

			//test for diagonal (\)
			sum=0;
			sum+=this.table[0,0];
			sum+=this.table[1,1];
			sum+=this.table[2,2];
			if(sum==3||sum==-3){
				this.victory=sum/3;
				#if sound
				this.Audio.playEndGame();
				#endif
				return(this.victory);}

			//test for diagonal (/)
			sum=0;
			sum+=this.table[2,0];
			sum+=this.table[1,1];
			sum+=this.table[0,2];
			if(sum==3||sum==-3){
				this.victory=sum/3;
				#if sound
				this.Audio.playEndGame();
				#endif
				return(this.victory);}
			return(0);}

		private int sumFreeFields(){
			int sum=0;
			for(int i=0;i<3;i++){
				for(int j=0;j<3;j++){
					if(this.table[i,j]==0)sum++;
				}
			}
			return(sum);}

		private char playerIdToChar(int playerId){
			#if colour
			#endif
			if(playerId==0)return(' ');
			#if colour
			#endif
			if(playerId==1)return('X');
			if(playerId==-1)return('O');
			#if colour
			Console.ForegroundColor=ConsoleColor.Magenta;
			#endif
			return('E');}

		
		public void printTable(){
			// ─│┌┐└┘├┤┬┴┼
			Console.Write("┌");
			for(int i=0;i<2;i++){
				Console.Write("─┬");}
			Console.Write("─┐\n");
			
			for(int y=0;y<3;y++){
				Console.Write("│");
				for(int x=0;x<3;x++){
					Console.Write("{0}│",this.playerIdToChar(this.table[x,y]));}
				Console.Write("\n");

				if(y!=2){
					Console.Write("├");
					for(int i=0;i<2;i++){
						Console.Write("─┼");}
						Console.Write("─┤\n");}
				}
			
			Console.Write("└");
			for(int i=0;i<2;i++){
				Console.Write("─┴");}
			Console.Write("─┘\n");}

		private void makeAMove(int x, int y){
			if(this.victory!=0);
			this.testCoordinates(x,y); //just to be sure
			this.table[x,y]=this.turn;
			this.turn*=-1;}

		private void askUserAndMove(){
			Console.Write("Input coordinates for your move. (you are {0}) \n",this.playerIdToChar(this.turn));
			bool exitcond=false;
			while(exitcond==false){
				int x=this.iWithRange("Input value for X (int >=0 <3) ",-1,3);
				int y=this.iWithRange("Input value for Y (int >=0 <3) ",-1,3);
				if(this.table[x,y]!=0){
					Console.Write("Field is not empty!\n");
					}else{
					exitcond=true;
					this.makeAMove(x,y);}}}
		private void askAiAndMove(){
			int x=0;
			int y=0;
			this.AI.evaluateMove(out x,out y, this.turn);
			Console.Write("AI ({0})  selects field x={1} y={2}\n",this.playerIdToChar(this.turn),x,y);
			this.makeAMove(x,y);}

		private void nextMove(){
			switch(this.gamemode){
				case 0:
					switch(this.turn){
						case 1:
							this.askUserAndMove();
							break;
						case -1:
							this.askAiAndMove();
							break;}
					break;
				case 1:
					switch(this.turn){
						case 1:
							this.askAiAndMove();
							break;
						case -1:
							this.askAiAndMove();
							break;}
					break;
				case 2:
					switch(this.turn){
						case 1:
							this.askUserAndMove();
							break;
						case -1:
							this.askUserAndMove();
							break;}
					break;}
					}
		private void endMessage(){
			switch(this.victory){
				case 0:
					Console.Write("Draw!\n");
					break;
				default:
					Console.Write("{0} Won the game!\n",this.playerIdToChar(this.victory));
					#if sound
					switch(this.victory){
						case 1:
							this.Audio.playVictory();
							break;
						case -1:
							this.Audio.playLost();
							break;
						}
					#endif
					break;}
			Thread.Sleep(1000);}
		private void startGameRealMethod(){
		Console.Write("Let's play a game\n");
			this.turn=1;
			#if DEBUG
			int x=0;//debug vars for ai method
			int y=0;//debug vars for ai method method
			#endif
			//main loop
			do{
				this.printTable();
				#if DEBUG
				this.AI.evaluateMove(out x,out y, this.turn);	//debug
				#endif
				//this.askUserAndMove();
				this.nextMove();
				this.checkForWin();
				if(this.sumFreeFields()==0)break;
			}while(this.victory==0);
			this.printTable();
			this.endMessage();}

		public void startGame(int arg){
			this.resetGame();	//just in case
			//if(this.victory!=0||this.turn!=0)this.resetGame();
			if(arg==1)this.gamemode=1;
			if(arg==2)this.gamemode=2;
			this.startGameRealMethod();
			}

		public void startGame(){	//argument overload for main method
			this.resetGame();	//just in case
			this.startGameRealMethod();
			}
	}
	#if sound
	class Audio{
		//mpv configuration
		private const String bgfile="./sound/d_e1m1.ogg";
		private const String mpvDefArg=" --really-quiet --no-input-terminal ";
		private const String mpvArgLoop=" --loop-file=inf ";
		private const String mpvArgVol=" --volume=150 ";
		private const String defSoundDir="./sound/cstrike/sound/misc/ut2004/announcermale2k4/";
		
		//random sound effect rangers
		private const int rangeEndLow=1;
		private const int rangeEndHigh=11+1;
		private const int rangeVicLow=50;
		private const int rangeVicHigh=51+1;
		private const int rangeLosLow=60;
		private const int rangeLosHigh=61+1;

		private Process mpvBg=null;
		private Process mpvAnnouncer=null;
		private Random rnd=null;
		private String mpvBindArg="";

		public Audio(){
			
			this.rnd=new Random();
			this.mpvBindArg= " " + this.mpvDefArg + this.mpvArgVol + this.defSoundDir;

			this.mpvBg=new Process();
			this.mpvBg.StartInfo.UseShellExecute=false;
			this.mpvBg.StartInfo.RedirectStandardInput=true;
			this.mpvBg.StartInfo.RedirectStandardOutput=true;
			this.mpvBg.StartInfo.RedirectStandardError=true;
			this.mpvBg.StartInfo.FileName="/usr/bin/mpv";
			this.mpvBg.StartInfo.Arguments=this.mpvDefArg + this.mpvArgLoop + this.bgfile;
			this.mpvBg.Start();

			this.mpvAnnouncer=new Process();
			this.mpvAnnouncer.StartInfo.UseShellExecute=false;
			this.mpvAnnouncer.StartInfo.RedirectStandardInput=true;
			this.mpvAnnouncer.StartInfo.RedirectStandardOutput=true;
			this.mpvAnnouncer.StartInfo.RedirectStandardError=true;
			this.mpvAnnouncer.StartInfo.FileName="/usr/bin/mpv";
			this.mpvAnnouncer.StartInfo.Arguments=this.mpvDefArg;
		}
		private void playSound(int nr){
			//Console.Write("PLAYSOUND INT NR={0}\n",nr);
			switch(nr){
				case 1:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "UltraKill.wav";
					this.mpvAnnouncer.Start();
					break;
				case 2:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "Dominating.wav";
					this.mpvAnnouncer.Start();
					break;
				case 3:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "Combowhore.wav";
					this.mpvAnnouncer.Start();
					break;
				case 4:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "SKAARJannihilation.wav";
					this.mpvAnnouncer.Start();
					break;
				case 5:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "UltraKill.wav";
					this.mpvAnnouncer.Start();
					break;
				case 6:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "Unstoppable.wav";
					this.mpvAnnouncer.Start();
					break;
				case 7:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "Road_Kill.wav";
					this.mpvAnnouncer.Start();
					break;
				case 8:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "Vehicular_manslaughter.wav";
					this.mpvAnnouncer.Start();
					break;
				case 9:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "SKAARJerradication.wav";
					this.mpvAnnouncer.Start();
					break;
				case 10:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "SKAARJbloodbath.wav";
					this.mpvAnnouncer.Start();
					break;
				case 11:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "HolyShit_F.wav";
					this.mpvAnnouncer.Start();
					Thread.Sleep(1000);
					break;
				case 50:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "Flawless_victory.wav";
					this.mpvAnnouncer.Start();
					break;
				case 51:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "You_Have_Won_the_Match.wav";
					this.mpvAnnouncer.Start();
					break;
				case 52:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "blue_team_wins_the_round.wav";
					this.mpvAnnouncer.Start();
					break;
				case 60:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "Humiliating_defeat.wav";
					this.mpvAnnouncer.Start();
					break;
				case 61:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "You_Have_Lost_the_Match.wav";
					this.mpvAnnouncer.Start();
					break;
				case 62:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "red_team_wins_the_round.wav";
					this.mpvAnnouncer.Start();
					break;
				case 70:
					this.mpvAnnouncer.StartInfo.Arguments=this.mpvBindArg + "HolyShit_F.wav";
					this.mpvAnnouncer.Start();
					break;
				default:
					return;}}
		public void playEndGame(){
			this.playSound(this.rnd.Next(this.rangeEndLow,this.rangeEndHigh));
			Thread.Sleep(2000);}
		public void playVictory(){
			this.playSound(this.rnd.Next(this.rangeVicLow,this.rangeVicHigh));}
		public void playLost(){
			this.playSound(this.rnd.Next(this.rangeLosLow,this.rangeLosHigh));}
		public void playTest(int i){
			this.playSound(i);}
	}
	#endif
}
