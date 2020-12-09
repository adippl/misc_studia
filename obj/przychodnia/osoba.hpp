#ifndef _OSOBA_H_
#define _OSOBA_H_
#include <string>

extern "C" {
	#include "pars.h"
	}

namespace ex{
	enum class Gender {err,f,m};
	class Osoba{
		protected:
			std::wstring _s_imie;
			std::wstring _s_nazwisko;
			enum Gender _e_gender;
			std::wstring _s_pesel;
			
		public:
			Osoba();
			Osoba(std::wstring _s_imie,
				  std::wstring _s_nazwisko,
				  enum Gender _e_gender,
				  std::wstring _s_pesel);
			
			std::wstring getImie();
			bool setImie(std::wstring a_s_imie);
			std::wstring getNazwisko();
			bool setNazwisko(std::wstring);
			std::wstring getPesel();
			bool setPesel(std::wstring);
			enum Gender getGender();
			bool setGender(enum Gender a_e_gender);
			std::wstring getDataUrodzenia();
			int getWiek();
			
			void setGenderFromPesel();

			bool imieValidate(std::wstring pesel);
			bool nazwiskoValidate(std::wstring pesel);
			bool genderValidate(enum Gender a_e_gender);
			bool peselValidate(std::wstring pesel);

			void WyswietlDane();
			void ZapiszDane(FILE p_file);
			void OdczytajDane(std::fstream);

	};
}


#endif // _OSOBA_H_
