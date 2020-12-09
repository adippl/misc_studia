#include "conf.hpp"


//#define DEFUN(name,fun,args) \
//	cl_def_c_function(c_string_to_object(name), \
//	(cl_objectfn_fixed)fun, args)
 
// Define some variables in C++ that we might wish to access from Lisp
auto elapsed = 0; // seconds elapsed
auto maxtime = 3600; // one hour
 
// Define some accessors.
cl_object runtime() {
	return ecl_make_integer(elapsed);
	}
 
cl_object set_runtime(cl_object i) {
	auto seconds = fix(i);
	elapsed = seconds;
	return ecl_make_integer(elapsed);
	}
 
// Define a function to run arbitrary Lisp expressions
cl_object lisp(const std::wstring & call) {
	using convert_type = std::codecvt_utf8<wchar_t>;
	std::wstring_convert<convert_type, wchar_t> converter;
	std::string converted_str = converter.to_bytes( call );

	 //return cl_safe_eval(c_string_to_object(call.c_str()), Cnil, Cnil);
	 return cl_safe_eval(c_string_to_object(converted_str.c_str()), Cnil, Cnil);
	}
 
// Initialisation does the following
// 1) "Bootstrap" the lisp runtime
// 2) Load an initrc to provide initial
//    configuration for our Lisp runtime
// 3) Make our accessors available to Lisp
// 4) Any In-line Lisp functions for later reference
void initialize(int argc, char **argv) {
	
	// Bootstrap
	cl_boot(argc, argv);
	atexit(cl_shutdown);
	
	// Run initrc script
	lisp(L"(load \"initrc.lisp\")");
	
	// Make C++ functions available to Lisp
	DEFUN("runtime", runtime, 0);
	DEFUN("set_runtime", set_runtime, 1);
	
	// Define some Lisp functions to call from C++
	lisp(L"(defun header () (format t \"Starting program...~%\"))");
	//lisp("(defun makeanumber () 3.2)");
	}
