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
using System.Diagnostics;


namespace cs{
	class std{
		static void Main(string[] args)
		{
			tictactoe.tic ti=new tictactoe.tic();
			
			#if DEBUG
			Console.Write("TEST AI vs AI game\n");
			ti.startGame(1);
			#endif

			#if colour
			//Console.BackgroundColor=ConsoleColor.Magenta;
			Console.ForegroundColor=ConsoleColor.Magenta;
			#endif
			
			//Console.Write("Player vs AI game\n");
			while(true){
				for(int i=0;i<24;i++){
					Console.Write("\n");
					Thread.Sleep(50);
				}
				Console.Write("-=-=-=-=-=-=- NEW GAME -=-=-=-=-=-=-\n");
				ti.startGame();
				#if audio
				Thread.Sleep(5000);
				#endif
			}
        }
    }
}
// vim: set ts=4 sw=4 ft=cs:
