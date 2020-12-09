#ifndef _CONF_H_
#define _CONF_H_
#include <ecl/ecl.h>
#include <iostream>
#include <unistd.h>
//#include <wchar.t>
#include <wchar.h>
#include <stdio.h>



#include <locale>
#include <codecvt>

#define DEFUN(name,fun,args) \
	cl_def_c_function(c_string_to_object(name), \
	(cl_objectfn_fixed)fun, args)
 
// Define some variables in C++ that we might wish to access from Lisp
//auto elapsed = 0; // seconds elapsed
//auto maxtime = 3600; // one hour
cl_object runtime();
cl_object set_runtime(cl_object i);
cl_object lisp(const std::wstring & call);
void initialize(int argc, char **argv);


struct ExceStr : public std::exception{
	std::string s;
	ExceStr(std::string ss) : s(ss) {}
	~ExceStr() throw () {} // Updated
	const char* what() const throw() { return s.c_str(); }
	};


//xtern const bool prettysyntax=1;
#endif //_CONF_H_

