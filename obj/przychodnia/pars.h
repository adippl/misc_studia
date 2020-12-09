#ifndef _PARS_H_
#define _PARS_H_

#include <stdio.h>
#include <wchar.h>
#include <stdlib.h>
#include <stdint.h>
//#include <stdarg.h>

//#include "conf.hpp"
const char defFile[]="./save.ign";

#define WCHAR_BUFF_SIZE 50

struct struct_wstringPair{
	wchar_t* name;
	wchar_t* val;};

char pars_printExpr(FILE* fp, wchar_t* str1, wchar_t* str2);
char pars_readExpr(FILE* fp, wchar_t* str1, wchar_t* str2);

//char pars_printExprObj(FILE* fp, wchar_t* objName, int num, ...);
char pars_printExprObj(FILE* fp, wchar_t* objName, int num, wchar_t** args);
char pars_readExprObj(FILE* fp, wchar_t* objName, int num, wchar_t** args);

char pars_findNextWChar(FILE* fp,wchar_t* a_wc_char);
char pars_getObjName(FILE* fp, wchar_t* a_wc_objName);

char pars_printRest(FILE* fp);

wchar_t** pars_WStringTableInit(int i_size);
void pars_WStringTableFree(wchar_t** a_p_table,int size);
#endif //_PARS_H_
