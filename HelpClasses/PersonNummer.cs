/*
0. Sista siffran i numren �r checksiffran.
Ta bort den om du ska kolla checksiffran.

1. Ta siffrorna i numret FR�N H�GER.
a. Multiplicera siffran med 2 f�rsta g�ngen.
Multiplicera sedan n�sta med 1 och 2 varannan g�ng.
b. Om resultatet av multiplikationen blir st�rre �n 9
s� l�gger man ihop siffrorna i resultatet och anv�nder det ist�llet
(exvis 2 x 9 = 18 som blir 1 + 8 = 9)
c. L�gg ihop alla resultaten.

2. Om summan slutar p� noll (0) s� �r checksiffran ocks� noll.
Om inte:
a. Ber�kna n�rmast st�rre tiotal.
(exvis 53 blir 60)
b. Dra bort summan fr�n detta tal.
(exvis f�r 53: 60 - 53 = 7)
Denna siffra �r checksiffran.

Exemplens personnummer, organisationsnummer, bank- och postgironummer
l�g p� n�tet. Jag letade upp dem med Altavista.


-=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=--=+=-
Exempel 		Personnummer (f.n. ej f�rdig person)
---------

Pnr 981001-1230	 http://millennium.sisu.se/wb/news2000/messages/102.html

3 * 2 =	 6 -> s =  0 + 0 + 6  =	 6
2 * 1 =	 2 -> s =  6 + 0 + 2  =	 8
1 * 2 =	 2 -> s =  8 + 0 + 2  = 10
1 * 1 =	 1 -> s = 10 + 0 + 1  = 11
0 * 2 =	 0 -> s = 11 + 0 + 0  = 11
0 * 1 =	 0 -> s = 11 + 0 + 0  = 11
1 * 2 =	 2 -> s = 11 + 0 + 2  = 13
8 * 1 =	 8 -> s = 13 + 0 + 8  = 21
9 * 2 = 18 -> s = 21 + 1 + 8  = 30
-----------
S u m = 30 -> c = 30 - 30 = 0 [OK]

*/

using System;

namespace Ortoped.HelpClasses
{
	/// <summary>
	/// Summary description for PersonNummer.
	/// </summary>
	public class PersonNummer
	{
		public PersonNummer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Kontrollerar att cehcksiffra st�mmer p� angivet personnummer. 
		/// Personnummer kan anges enligt f�ljande format:
		/// 
		/// YYYYMMDD-XXXX
		/// YYYYMMDDXXXX
		/// 
		/// YYMMDD-XXXX
		/// YYMMDDXXXX
		/// 
		/// </summary>
		/// <param name="pnr"></param>
		/// <returns></returns>
		public bool calculate(string pnr)
		{
			int sum = 0, nextvalue, checksiffra = 0, multiplyer = 2;
			
			pnr = pnr.Replace("-","");
			if(pnr.Length == 12)
				pnr = pnr.Substring(2);

			// Summera v�rden
			char[] c = pnr.ToCharArray();
			for(int i=0;i<c.Length -1;i++)
			{
				nextvalue = int.Parse(c[i].ToString()) * multiplyer;
				if(nextvalue > 9)
					sum += splitNumber(nextvalue);
				else
					sum += nextvalue;

				// varannan g�ng 2 och varannan 1
				if(multiplyer == 2)
					multiplyer = 1;
				else
					multiplyer = 2;
			}

			// Om talet slutar p� noll �r checksiffran noll
			if((sum % 10) == 0)
			{
				checksiffra = 0;
			}
			else
			{
				checksiffra = 10 - (sum % 10);
			}

			// Kontrollera om checksiffra st�mmer
			if(checksiffra == int.Parse(pnr.Substring(9,1)))
        return true;
			else
				return false;
		}

		private int splitNumber(int number)
		{
			string s, s1, s2;
			
			s = number.ToString();
			s1 = s.Substring(0,1);
			s2 = s.Substring(1,1);
			return int.Parse(s1) + int.Parse(s2);
		}
	}
}
