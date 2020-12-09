#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <time.h>
#include <wchar.h>
#include <limits.h>

#define _NULLPE exit(999); 
#define _BTN_ID 0
#define _BTNL_ID 1
#define _BTNI_ID 2
#define _MAXFREQ_LENGTH 4
#define _MAX
#define _STCK_LENGTH 64

#ifdef DEBUG
#define free(PTR) \
	fprintf(stderr,"freeing memory at %p\n",(void*)PTR); \
	free(PTR) 
#endif

#ifdef DEBUG
void* debugCalloc(size_t mult,size_t size){
	void* ptr=calloc(mult,size);
	fprintf(stderr,"allocating %ld bytest of memory at %p\n",(size_t)size*mult,(void*)ptr); \
	return(ptr);}

	#define calloc(mult,size) debugCalloc(mult,size)
#endif


const wchar_t charrange[]=L"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" ;

typedef unsigned int uint_t;

struct bintreenode{
	uint_t freq;
	char type;
	wchar_t ch;
	struct bintreenode* l0_p;
	struct bintreenode* r1_p;
	};
typedef struct bintreenode btn;

struct binTree{
	uint_t str_l;
	btn* treeRoot_p;
	btn* leafs;
	btn* internalNodes;
	btn** freqarr;
	uint_t lastUsedInNode;
	};
typedef struct binTree bt;

void
bt_free(bt** bt_p){		//fixit TODO
	free((*bt_p)->leafs);
	free((*bt_p)->internalNodes);
	free((*bt_p)->freqarr);
	free(*bt_p);
	return;}


struct ch_stack{
	void* self;
	uint_t maxStackLength;
	uint_t currentStackLength;
	char* root;
	char* stack_p;
	
	char (*push)(struct ch_stack* sck_struct_p, char digit);
	char (*pop)(struct ch_stack* sck_struct_p);
	void (*del)(struct ch_stack* sck_struct_p);
	//char (*push)();
	//char (*pop)();
	//void (*del)();

	};
typedef struct ch_stack stck;

stck* ch_stack_init();
void ch_stack_free(stck* st_p);
char ch_stack_pop(stck* st_p);
char ch_stack_push(stck* st_p, char digit);

stck*
ch_stack_init(){
	stck* st_p=calloc(1,sizeof(stck));
	if(!st_p)return(NULL);
	st_p->maxStackLength=_STCK_LENGTH;
	st_p->currentStackLength=0;
	st_p->root=calloc(_STCK_LENGTH,sizeof(char));
	if(!st_p->root){
		free(st_p);
		return(NULL);}
	st_p->stack_p=st_p->root;
	st_p->push=&ch_stack_push;
	st_p->pop=&ch_stack_pop;
	st_p->del=&ch_stack_free;
	
	return(st_p);}
void
ch_stack_free(stck* st_p){
	if(!st_p)return;
	free(st_p->root);
	free(st_p);
	return;}

char
ch_stack_push(stck* st_p, char digit){
	if(!st_p)return(1);
	//if((st_p->currentStackLength)+1<=st_p->maxStackLength)return(1);
	char ret_char=0;
	
	st_p->currentStackLength++;
	st_p->stack_p++;
	*(st_p->stack_p)=digit;
	#ifdef DEBUG
	fprintf(stderr,"stack pop stacklen=%p\n",st_p->currentStackLength);
	#endif
	
	return(ret_char);}
	
char
ch_stack_pop(stck* st_p){
	if(!st_p)return(-1);
	if(st_p->currentStackLength==0)return(-1);
	char retVal=*(st_p->stack_p);
	st_p->currentStackLength--;
	st_p->stack_p--;
	return(retVal);}

int
chStack_getAllBin(stck* st_p){
	int retval=0;
	
	for(int i=st_p->currentStackLength;i>=0;i--){
		retval+=*(st_p->root+i);
		retval=retval<<1;
	}
	
	return(INT_MAX&retval);}

//char chStack_Inspect(stck* st_p){
//	fprintf(stderr,"max length %d\n lengt %d\n root %p\n stack_p %p\n",st_p->maxStackLength,st_p->currentStackLength,st_p->root,st_p->stack_p);}

int
charswap(wchar_t* c1, wchar_t* c2){
	if(!c1||!c2)return(1);
	if(!*c1||!*c2)return(2);
	wchar_t tmp='\0';
	tmp=*c2;
	*c2=*c1;
	*c1=tmp;
	return(0);}

void
scramblestr(wchar_t *str, uint_t *length, uint_t n){
	int pos;
	for(uint_t h=0;h<n;h++){
		for(uint_t i=0;i<*length;i++){
			pos=rand()%*length;
			charswap(str+pos,str+i);}}
	return;}


uint_t
attachNodes(bt* tree_p,uint_t nodeA, uint_t nodeB){
	if(!tree_p)_NULLPE
	if(nodeA==nodeB)return(1);

	btn** fa=tree_p->freqarr;
	btn* node_p=(tree_p->internalNodes+tree_p->lastUsedInNode);
	char somethingHadChanged=0;

	if( ((!nodeB) && !*(fa+nodeB)) || ((!nodeB) && !*(fa+nodeB)) )return(3);	/* FIXIT if true something went wrong, or program finished building the heap */

	if(node_p->type!=_BTNI_ID)return(2);

	node_p->l0_p=*(fa+nodeA);
	node_p->freq+=(*(fa+nodeA))->freq;
	(*(fa+nodeA))->freq=0;
	*(fa+nodeA)=NULL;


	node_p->r1_p=*(fa+nodeB);
	node_p->freq+=(*(fa+nodeB))->freq;
	(*(fa+nodeB))->freq=0;
	somethingHadChanged=1;
	(*(fa+nodeB))=NULL;

	if(somethingHadChanged) *(fa+nodeA)=node_p;

	tree_p->lastUsedInNode++;
	tree_p->treeRoot_p=node_p;

	return(0);}



uint_t
attachSmallestNodes(bt* tree_p){
	if(!tree_p)_NULLPE
	
	//btn** fa=tree_p->freqarr;
	btn** fa=tree_p->freqarr;

	//uint_t smallestFreq=(*fa)->freq;
	uint_t smallestFreq=999999;
	uint_t skip_position=tree_p->str_l+1;
	uint_t smallA_pos=0;
	uint_t smallB_pos=0;

#ifdef DEBUG
for(uint_t i=0;i<tree_p->str_l;i++){
	if(*(fa+i)!=NULL){
		printf("i=%d F=%d\n",i,(*(fa+i))->freq);
		}
	}
#endif

	
	for(uint_t i=0;i<tree_p->str_l;i++){
		if(i!=skip_position){
			if(*(fa+i)!=NULL){
				if((*(fa+i))->freq < smallestFreq && (*(fa+i))->freq!=0 ){
					smallestFreq=(*(fa+i))->freq;
					smallA_pos=i;
					}
				}
			}
		} 

	skip_position=smallA_pos;
	smallestFreq=999999;	//FIXIT TODO

	for(uint_t i=0;i<tree_p->str_l;i++){
		if(i!=skip_position){
			if(*(fa+i)!=NULL){
				if((*(fa+i))->freq < smallestFreq && (*(fa+i))->freq!=0 ){
					smallestFreq=(*(fa+i))->freq;
					smallB_pos=i;
					}
				}
			}
		} 

#ifdef DEBUG
	fprintf(stderr,"minA=%d\n",smallA_pos);
	fprintf(stderr,"minB=%d\n",smallB_pos);
	fprintf(stderr,"\n");
#endif
	
	//attachNodes(tree_p,smallA_pos,smallB_pos);
	if( attachNodes(tree_p,smallA_pos,smallB_pos) == 3) return(3);

	return(0);}

bt*
gentree(wchar_t* inpstr){
	if(!inpstr)_NULLPE
	wchar_t (*str)[]=(void*)inpstr;
	uint_t str_l=wcslen(inpstr);
//	btn* rootReturn_p=NULL;

	btn* leafs=calloc(str_l,sizeof(btn));
	for(uint_t i=0;i<str_l;i++){
		(leafs+i)->type=_BTNL_ID;
		(leafs+i)->ch=(*str)[i];
		(leafs+i)->freq=i+1;	// temporary frequency testing values TODO
		}

	btn* btn_p=calloc(str_l,sizeof(btn));
	for(uint_t i=0;i<str_l;i++){
		(btn_p+i)->type=_BTNI_ID;
		}

	btn** freqarr=calloc(str_l,sizeof(btn*));
	for(uint_t i=0;i<str_l;i++){
		*(freqarr+i)=(btn*)leafs+i;
		}

	bt* tree_p=calloc(1,sizeof(bt));
	tree_p->str_l=str_l;
	tree_p->leafs=leafs;
	tree_p->internalNodes=btn_p;
	tree_p->freqarr=freqarr;
	tree_p->lastUsedInNode=0;

	for(uint_t i=0;i<tree_p->str_l;i++){
		if( attachSmallestNodes(tree_p) ==3)break;
		}

	return((bt*)tree_p);}

struct test{ uint_t encoding; wchar_t string[32];};

void*
gentable(bt* bt_p){
	stck* st_p=ch_stack_init();
	if(!st_p)return(NULL);

	
	st_p->push(st_p,1);
	st_p->push(st_p,0);
	st_p->push(st_p,1);
	st_p->push(st_p,0);
	st_p->push(st_p,1);
	st_p->push(st_p,0);
	st_p->push(st_p,1);
	fprintf(stderr,"dump %x\n",chStack_getAllBin(st_p));

	//btn* currentNode=bt_p->
	
	//tree traversal
	


	st_p->del(st_p);
	return(NULL);}


int
main(){

	time_t tt;
	srand(time(&tt));
	
	printf("\n");
	printf("%ls\n",charrange);
	
	unsigned int strlength=sizeof(charrange);
	wchar_t* scrmbled_string=calloc(strlength,sizeof(wchar_t));
	memcpy(scrmbled_string,&charrange,strlength);
	printf("%ls\n",scrmbled_string);
	
	scramblestr(scrmbled_string,&strlength,4);

	printf("%ls\n",scrmbled_string);
	printf("lol\n");

	bt* tree_p=gentree(scrmbled_string);


	void* test=gentable(tree_p);
	free(tree_p);
	return(0);}

// vim: set ts=4 sw=4 ft=c
// vm: syntax keyword cType binode:
