#include	<iostream>

#include	"osoba.hpp"
#include	"termin.hpp"
#include	"conf.hpp"

//#include	"pars.h"
//#include	<stdio.h>

extern "C" {
	#include "pars.h"
	}

auto clelapsed = 0; // seconds elapsed
auto clmaxtime = 3600; // one hour

int
main(int argc, char * argv[]){
	
	std::cout<<"Hello world\n";

	initialize(argc,argv);
	lisp(L"(header)");
	lisp(L"(load \"./pesel.lisp\")");
	

	FILE* fp=NULL;
	fp=fopen(defFile,"w+");
	std::cout<<"fp="<<fp<<"\n";
	printf("ąęółśćźż\n");



	return(0);}
