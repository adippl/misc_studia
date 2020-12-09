#include "osoba.hpp"
#include "conf.hpp"

namespace ex{
	bool Osoba::imieValidate(std::wstring pesel){return(true);}
	bool Osoba::nazwiskoValidate(std::wstring pesel){
		std::wstring arg=L"(pesel-test \"" + pesel + L"\")";
		if(ecl_to_bool(lisp(arg))){
			return(true);
		}else{
			throw ExceStr("pesel error");}
		}

	bool Osoba::genderValidate(enum Gender a_e_gender){return(true);}
	bool Osoba::peselValidate(std::wstring pesel){return(true);}


	
	std::wstring Osoba::getImie(){return(this->_s_imie);}
	bool Osoba::setImie(std::wstring a_s_imie){
		if(this->imieValidate(a_s_imie)){
			this->_s_imie=a_s_imie;
		}else{
			throw ExceStr("setImie error");
			}
		return(false);}
	
	std::wstring Osoba::getNazwisko(){return(this->_s_nazwisko);}
	bool Osoba::setNazwisko(std::wstring a_s_nazwisko){
		if(this->nazwiskoValidate(a_s_nazwisko)){
			this->_s_nazwisko=a_s_nazwisko;
		}else{
			throw ExceStr("setNazwisko error");
			}
		return(false);}
	
	std::wstring Osoba::getPesel(){return(this->_s_pesel);}
	bool Osoba::setPesel(std::wstring a_s_pesel){
		if(this->peselValidate(a_s_pesel)){
			this->_s_pesel=a_s_pesel;
		}else{
			throw ExceStr("setPesel error");
			}
		return(false);}
	
	enum Gender Osoba::getGender(){return(this->_e_gender);}
	bool Osoba::setGender(enum Gender a_e_gender){
		if(this->genderValidate(a_e_gender)){
			this->_e_gender=a_e_gender;
		}else{
			throw ExceStr("setGender error");
			}
		return(false);}

	std::wstring Osoba::getDataUrodzenia(){
		std::wstring str(reinterpret_cast<wchar_t*>(
			lisp(L"(pesel-birth-date \"" + this->_s_pesel + L"\")"
			)->base_string.self));	//horrible
		return(str);
		}
	int Osoba::getWiek(){
		int retval=ecl_to_int(lisp(L"(pesel-age \""+this->_s_pesel+L"\")"));
		if(retval>=0){
			return(retval);
		}else{
			throw ExceStr("getWiek negative value");}
	}

	void Osoba::setGenderFromPesel(){
		char g=ecl_to_char(
			lisp(L"(pesel-get-gender \""+this->_s_pesel+L"\")"));
		switch(g){
			case 'm':
				this->_e_gender=Gender::m;
				break;
			case 'f':
				this->_e_gender=Gender::f;
				break;
			default:
				throw ExceStr("setGenderFromPeselError");
				break;}
		}

//	void Osoba::ZapiszDane(FILE p_file){
//		wchar_t** args=pars_WStringTableInit(4);
//		*(args+0)=this->_s_imie.c_str();
//		pars_printExpr
//		void pars_WStringTableFree(args,4);
//		}

//		void WyswietlDane();
//		void ZapiszDane(std::fstream);
//		void OdczytajDane(std::fstream);


}
