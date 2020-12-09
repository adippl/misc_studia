#include "pars.h"

//struct struct_wstringPair{
//	wchar_t* name;
//	wchar_t* val;};

char
pars_printExpr(FILE* fp, wchar_t* str1, wchar_t* str2){
	fwprintf(fp,L"( %ls %ls )",str1,str2);
	return(0);}
char
pars_readExpr(FILE* fp, wchar_t* str1, wchar_t* str2){
	fwscanf(fp,L"( %45ls %45ls )",str1,str2);
	return(0);}

//char pars_printExprObj(FILE* fp, wchar_t* objName, int num, ...){
//	va_list valist;
//	va_start(valist,num);
//	long int filepos=ftell(fp);
//	fwprintf(fp,"( %ls ",objName);
//
//
//	struct struct_wstringPair* args=NULL;
//	for(int i=0;i<num;i++){
//	   args=va_arg(valist, struct struct_wstringPair*);
//	}
//
//	}

char
pars_printExprObj(FILE* fp, wchar_t* objName, int num, wchar_t** args){
	//long int startingFilePos=ftell(fp);
	fwprintf(fp,L"( %ls ",objName);
	for(int i=0;i<(num*2);i+=2){
		pars_printExpr(fp, *(args+i), *(args+i+1));}
	fwprintf(fp,L" )");
	return(0);}

char
pars_readExprObj(FILE* fp, wchar_t* objName, int num, wchar_t** args){
	//long int startingFilePos=ftell(fp);
	wchar_t buffer[50]={0};
	//fwscanf(fp,L"( %45ls ",buffer);
	fwscanf(fp,L"( %45ls ",objName);
	for(int i=0;i<(num*2);i+=2){
		pars_readExpr(fp, *(args+i), *(args+i+1));}
	fwscanf(fp,L" )",buffer);
	//if(fwscanf(fp,L" )",buffer)!=2){
	//	fseek(fp,startingFilePos,0);
	//	return(-1);
	//}
	return(0);}

wchar_t**
pars_WStringTableInit(int i_size){
	wchar_t** out=(wchar_t**)calloc(i_size,(sizeof(wchar_t**)));
	for(int i=0;i<i_size;i++){
		*(out+i)=(wchar_t*)calloc(WCHAR_BUFF_SIZE,sizeof(wchar_t));}
	return(out);}
void
pars_WStringTableFree(wchar_t** a_p_table,int size){
	for(int i=0;i<size;i++){
		free(*(a_p_table+i));}
	free(a_p_table);
	return;}
 
char
pars_findNextWChar(FILE* fp,wchar_t* a_wc_char){
	wchar_t buffer[5]={0};
	//int64_t startingFilePos=ftell(fp);
	
	while(( buffer[0]=fgetwc(fp))!=EOF ){
		//putwchar(buffer[0]);
		if(buffer[0]==*a_wc_char)fseek(fp,-1,SEEK_CUR);
		break;
	}
	return(0);}

char
pars_getObjName(FILE* fp, wchar_t* p_wc_objName){
	//wchar_t buffer[WCHAR_BUFF_SIZE]={0};
	int64_t startingFilePos=ftell(fp);
	fwscanf(fp,L"( %45ls ",p_wc_objName);
	//printf("\nstartingFilePos=%ld\n",startingFilePos);
	fseek(fp,startingFilePos,0);
	return(0);}

char
pars_printRest(FILE* fp){
	wchar_t buffer[5]={0};
	int64_t startingFilePos=ftell(fp);
	
	while(( buffer[0]=fgetwc(fp))!=EOF ){
		putwchar(buffer[0]);
		//if(buffer[0]==*a_wc_char)fseek(fp,-1,SEEK_CUR);
	}
	fseek(fp,startingFilePos,0);
	return(0);}

//char pars_findQuoteString(wchar* buffer, )
//	return(0);}
