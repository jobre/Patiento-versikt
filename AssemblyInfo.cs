using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/*
 * Version 1.1.0.1 uppdaterad av Joakim Brenevik 2005-04-20
 * 	
 * Version 1.1.0.2 uppdaterad av Joakim Brenevik 2005-04-20
 * *	I order�versikt visas nu att om ordern �r st�ngd
 *	* Styrning av mellanslag i Pnr/Ort (uppl�gg patient). Vissa kontroller av postnummrets
 *		l�ngd och mellanslag i position fyra kontrolleras
 * * Koppling till Kalender, Kundformul�r, Kundreskontraformul�r i Garp genom knappar. Kundnummer sparas
 *		i ClipBoard innan v�xling till Garp g�rs.
 *
 * Version 1.1.0.3 uppdaterad av Joakim Brenevik 2005-05-17
 *	* Lagt till konvertering av null v�rden till blankt i Porpertys f�r
 *		klassen OrderCOM
 * 
 * Version 1.1.0.4 uppdaterad av Joakim Brenevik 2005-05-17 
 *	*	Problem med Nullreferens i "isOrderHeadChanged". Har lagt till en try
 *		f�r att hantera dessa felmeddelanden
 *	*	Vid val av bl a handl�ggare kom det upp ett meddelande om "Uppdaterin ej till�ten".
 *		Detta kom d� det var en rad med blankt artikelnummer. Nu �r denna kontroll �ndrad s� att
 *		funktionen "saveOrderRow" returnerar tru �ven n�r en rad har blankt artiklenummer. Vad som egentligen
 *		h�nder �r att raden raderas, "You will learn the hard way!"
 *	*	Radtexten h�ngde kavar mellan orderrader. Detta �r �ndrat s� att radtexten blankas n�r tabb 0 (�versikt)
 *		v�ljs, sedan uppdateras texten n�r man v�ljer en under "Detaljer"
 *	*	Om man angav en order direkt s� uppdaterades inte patienten, ist�llet fick ordern den "gammla"
 *		patientens kundnummer s� att ordern bytte Patient/levkund. Detta �r nu �ndrat, i frmMain till�ter
 *		vi uppdatering av KNR endast n�r KNR �r blankt.
 *
 * Version 1.1.4.2 uppdaterad av Joakim Brenevik 2005-05-17
 *	*	Bortag av fr�ga ifall order skall l�ggas upp om den skansas, detta strulade n�r det g�ller
 *		kontroll av signatur
 *	* �ndrat nummerserien f�r versionshanteringen
 *
 * Version 1.1.4.3 uppdaterad av Joakim Brenevik 2005-05-17
 *	* Fixat problem med att artiklar inte visades i �versikten
 *	* Produktionsstatuslistan raderades vid orderuppl�gg, fixat	
 *
 * Version 1.1.5.1 uppdaterad av Joakim Brenevik 2005-06-14
 *	* I order�versikt visas, Ordernr, giltighetsdatum from/tom, status, rekvnr, ordinationstext
 *	* G� till tidbok med h�gerklick fr�n hj�lpmedel
 *	* Skriv ut Kallelse med h�gerklick fr�n hj�lpmedelsrad
 *	* Skriv ut Arbetsorder med h�gerklick fr�n hj�lpmedelsrad
 *	*	Ledtext lagd till f�r "Lev.S�tt"
 *	
 * Version 1.1.5.2 uppdaterad av Joakim Brenevik 2005-06-22
 *	* Detta �r nu �ndrat. Ny hantering av s�kning p� patient. Om s�kning skall ske skall det
 *		initsieras med punkt (precis som i alla andra s�kningar), om ett v�rde matas in utan n�gon 
 *		punkt framf�r s� f�ruts�tts detta att vara ett personnummer.
 *	* Vid ny order t�ms hela formul�ret p� data
 *	* Alla tidbokningar p� en Patient visas, tidigare visades bara de som var kopplade till aktuell order
 *	* Hopp direkt till kontantknapp vid egenavgift (efter att Avgift fyllts i)
 *	*	Vid nytt Hj�lpmedel hamnar man p� handl�ggare ist�llet f�r p i Artikelnummer
 *	*	Efter prislista hamnar man i orderrads�versikten
 *
 * Version 1.1.5.3 uppdaterad av Joakim Brenevik 2005-06-30
 *	* V�ldigt mycket nytt enligt lista, n�gra axplock nedan
 *	* Implementering av XML baserd konfigurationsfil
 *	* Dynamiska dokumentlistor
 *	* Journal och CopDoc g�r att andra fr�n formul�ret (readonly innan)
 *
 ** Version 1.1.5.4 uppdaterad av Joakim Brenevik 2005-07-05
 *	* Buggr�ttningar 
 *
 ** Version 1.1.5.5 uppdaterad av Joakim Brenevik 2005-07-08
 *	* Buggr�ttningar
 *
 ** Version 1.1.5.6-8 uppdaterad av Joakim Brenevik 2005-10-01
 * *	Ombyggnad av dokumenthantering (kallelse, arbetsorder)
 * *	Ombyggnad av egenavgift. 
 * *	Utskrift av faktura och kontantkvitto p� egenavgift
 * *	Ny hantering av koppling av �renden.
 * *	Diverse r�ttningar
 *
 ** Version 1.1.5.9 uppdaterad av Joakim Brenevik 2005-10-01
 * *	�ndring av utskrft av Kallelse. Nu skickas INTE hj�lpmedelsid
 *		med. Detta f�r att informationen �r �verfl�dig vid arbetsorder.
 *		�nskem�l l�mnat av PN.
 * *	R�ttning av koppling av �rende. Ordernummer skickades med endast 5
 *		tecken till getAllAid(), detta �r nu r�ttat.	
 *
 ** Version 1.1.5.10 uppdaterad av Joakim Brenevik 2005-10-03
 *	*	Vid leverans s�tts reservationskod till "1"
 *	*	�ndrad hantering av priser p� artikel. Innan h�mtades proset fr�n
 *		artikeln vilket gjorde att prishantering med prislistor/offerter
 *		inte hanterades p� korrekt vis. Nu h�mtas proiset fr�n orderrad 
 *		(som ber�knats av servern) och uppdateras endast n�r man l�mnar 
 *		txtApris f�ltet.
 *
 ** Version 1.1.5.11 uppdaterad av Joakim Brenevik 2005-10-03
 *	*	Fixat problem d� det vid byte av order kunde kopieras hj�lpmedel mellan ordrarna.
 *
 ** Version 1.1.5.12 uppdaterad av Joakim Brenevik 2005-10-04
 *	*	Ytterligare "t�tning" f�r att f�rhindra att orderrader "kopieras" mellan ordrar 
 *	*	Vissa f�lt nollst�lldes inte vid byte av order, detta �r nu fixat .
 *	*	Numer g�r det att ange en order eller v�lja knappen tidigare �ven om alla
 *		groupboxar �r "gr�ade". 
 *	*	Fixat problem med s�kning d� det inte hittades en patient om man angav ett helt personnummer
 *		Detta berodde p� ett felaktigt j�mf�relsev�rde i getPatientByPnr()
 *	*	Fixat decimalproblem som uppstod vid decimalhantering p� orderradsantal. Nu konverteras
 *		alltid komma till punkt
 *	*	Fixat problem som kan uppst� vid byte av artikel p� en rad, detta �r f�r tillf�llet sp�rrat
 *
 ** Version 1.1.5.13 uppdaterad av Joakim Brenevik 2005-10-05
 *	*	Lagt till funktion getAllRowsOwnFeeIncluded() som returnerar �ven egenavgifter
 *		Denna funktion anv�nds vid leverans av hj�lpmedel s� att �ven egenavgifterna
 *		levereras p� samma FS
 *	*	�ndrat s� att datum visar dagens datum n�r formul�ret �ppnas
 *
 ** Version 1.1.5.14 uppdaterad av Joakim Brenevik 2005-10-10
 *	*	R�ttat bugg i egenavgift som gjorde att patientens egeanavgift alltid blev debet.
 *	*	Lagt till textrader med h�nvisningar till/fr�n egenavgift p� orginal och patientorder
 *
 *** Version 1.1.5.15 uppdaterad av Joakim Brenevik 2005-10-11
 *	*	�ndradr funtionalitet f�r att inte texter skall kopieras mellan ordrar, detta kunde tidigare
 *		ske om en helt ny order lades upp.
 *	*	Ny funktion CTL + N f�r ny order
 *	*	Ny funktion CTRL + I f�r nytt hj�lpmedel
 *	
 *** Version 1.1.5.16 uppdaterad av Joakim Brenevik 2005-10-17
 *	*	Fixat hantering av hj�lpmedelsid som konverterats in p� anorlunda s�tt gentemot hur vi hanterar detta 
 *		i Patient�versikten.
 *	*	D�pt om "CopDok" till "CoPdoc"	
 *	*	Fixat ett antal buggar som g�r att orderrader kan mixas
 *	* Ny b�ttre generering av AidID, den gammla gjorde att ett AidID som redan fanns kunde ges som f�rslag
 *
 *** Version 1.1.5.17 uppdaterad av Joakim Brenevik 2005-10-19
 *	* Prislista h�mtas fr�n fakturakunden n�r denna anges. Tidigare h�mtades denna fr�n order och dett
 *		fungerade inte f�r Aktivs del d� det inte finns n�gon koppling mellan patient (leveranskund)
 *		och fakturakunden.
 *	*	T�ppt igen ytterligare luckor d� orderrader kunde kopieras mellan olika ordrar. Denna g�ng var det i vissa
 *		l�gen n�r man gick fr�n en order med hj�lpmedel till en order utan hj�lpmedel som raderna kunde kopieras
 *		med till den raden utan hj�lpmedel. Detta f�r att Detaljvyn inte rensades mellan orderbyte, det g�rs nu.
 *	*	Vid uppl�gg av ny patient s� kontrolleras om det finns ett "-" i personnummret, finns inte detta l�ggs det till
 *
 *** Version 1.1.5.18 uppdaterad av Joakim Brenevik 2005-10-31
 *	*	Numer sparas ordern innan en frysning g�rs, detta gjordes ej tidigare vilket innebar att
 *		vissa f�lt (som Diagnos, notering m.m) inte sparades
 *	*	�ndrat ordning p� f�rnamn och efternamn, dessa h�mttades tidigare in i fel ordning s� att dessa mixades.
 *	*	Lagt till funktionalitet f�r att hantera inst�llningar per anv�ndare. anv�ndare kodas med prefix i 
 *		anv�ndarnamnet f�ljt av #. I Configure.xml kan man sean styra vilka anv�ndarkoder som skall styras mot 
 *		nummerserie p� order samt vilka patienter som skall visas.
 *
 *** Version 1.1.5.19 uppdaterad av Joakim Brenevik 2005-10-31
 *	*	KOD3 som enligt specefikation skall anv�ndas till patientens Tillh�righet�r bytt till
 *		KOD6, detta beslut �r taget av Peter Nilsson Office Karlstad.
 *	*	Fixat problem med ordrar som inte visades i �versikten och inte gick att l�gga till nya rader p�.
 *		Detta berodde p� gamla egenavgifter som st�llde till kontrollen som g�rs p� antal aidid kontra
 *		antal rader som visas i �versikten. Numer "r�knas" inte egenavgiftsrader n�r denna kontroll g�rs
 *	*	Nu inaktiveras groupboxarna under "Detaljer" om inget AidId �r valt, tidigare var dessa �pnna �ven 
 *		om man inte valt n�got AidID
 *
 *** Version 1.1.5.20 uppdaterad av Joakim Brenevik 2005-11-10
 *	*	Numer sparas Produktionsstatus, Levernastid, Garanti, Handl�ggare p� AidId, tidigare sparades
 *		detta p� den sista raden i ett hj�lpmedelsid	
 *
 *** Version 1.1.5.21 uppdaterad av Joakim Brenevik 2005-11-18
 *	*	Uppdaterat med styrning till kostnadsst�lle per bolag.
 *	*	Uppdaterat med styrning av betalningsvillkor vad g�ller egenavgift per bolag
 *	* Ombyggnad av Configure.xml f�r att f�renkla strukturen.		
 *
 *** Version 1.1.5.22 uppdaterad av Joakim Brenevik 2005-11-21
 *	*	Uppdateringar enlig version 1.1.5.21 skall ist�llet vara per anv�ndargrupp,
 *		detta �r nu fixat
 *	*	Lagt till visning av detaljer fr�n Config i statusraden. Visning kan v�xlas
 *		med CTRL + SHIFT + ALT + V
 *	 
 *** Version 1.1.5.23 uppdaterad av Joakim Brenevik 2005-12-05
 *	*	R�ttning av Leverenss�tt. Om n�got leveranss�tt hade blank ben�mning genererades ett fel
 *		om detta anv�ndes
 *
 * *** Version 1.1.5.24 uppdaterad av Joakim Brenevik 2005-12-23
 *	* Breddat formul�ret
 * * Lagt till visning av enhet p� detaljinfo hj�lpmedel
 * * Lagt till antal p� listview som visar ing�ende artiklar
 * * Lagt till handl�ggare i visningen av �renden, �ven kortat tiderna ner till 5 tkn (hh:mm)
 * * �ndrat tabordning i detaljvy. Handl�ggare -> Produktionsstatus -> Artikel. Samtidigt
 *   l�ses �nskem�l om att kunna backa i denna tabordning
 *
 * * *** Version 1.1.5.25 uppdaterad av Joakim Brenevik 2006-02-10
 *	* Lagt till kreditering av hj�lpmedel
 * * Lagt till texter p� orderhuvudsniv� (fakturainfo)
 * * Lagt till texterader kopplade till hj�lpmedel
 * * Lagt till summering av priser p� hj�lpmedelsniv�
 * * Lagt till f�lt f�r enhetshantering
 *
 * * *** Version 1.1.5.26 uppdaterad av Joakim Brenevik 2006-02-10
 * *	Fixat bugg med aidstexter d�r en loop gick igenom hela orderegistret ist�llet f�r aktuell order.
 *
 * * *** Version 1.1.5.27 uppdaterad av Joakim Brenevik 2006-02-23
 * *	Fixat problem med CopDoc och Journal som inte alltid uppdaterades
 * *	Fixat problem med ID rader som mixas, detta berode p� en sparning som gjordes vid val av handl�ggare
 *		d� triggades funktionen som raderar orderrader utan artikelnummer 
 *	*	
 *
 * * *** Version 1.1.5.28 uppdaterad av Joakim Brenevik 2006-02-28
 * *	Fixat problem med CopDoc och Journal som inte alltid uppdaterades, utterliggare h�l t�tat
 * *	Lagt till en ny grupp i Configure.xml f�r att hantera mall p� kunder.
 *	*	Lagt till visning av "verkligt" leveransdatum, fakturanummer och fakturadatum
 *
 * * *** Version 1.1.5.29 uppdaterad av Joakim Brenevik 2006-03-02
 * *	Diverse sm�buggar
 *
 * * *** Version 1.1.5.30 uppdaterad av Joakim Brenevik 2006-03-02
 * *	�ndrat leveransdatum, nu h�mtas denna fr�n HKR och LDT vilket �r "perioddatum"
 *
 * * *** Version 1.1.5.31 uppdaterad av Joakim Brenevik 2006-03-07
 * *	Fixat problem med handl�ggare och produktionsstatus som f�rsvann n�r nytt hj�lpmedel lades upp
 * *	Optimerat visningen av orderrader n�r man v�xlar till detaljvyn.
�*  
 * * *** Version 1.1.5.32 uppdaterad av Joakim Brenevik 2006-03-07
 * *	Levtid p� OR blir numer alltid det samma som leveranstid vid leverans
 * *	Fixat visning av dokument med menyer i mainform, visades tidigare vid fel nod.
 *
 * * *** Version 1.1.5.33 uppdaterad av Joakim Brenevik 2006-03-10
  * *	�ndrat hantering av texter s� att endast en text syns (hj�lpmedel/artikel)
 * *	�ndrat s� att enhet visas direkt efter man angivit artikelnummer
 *
 * * *** Version 1.1.5.34 uppdaterad av Joakim Brenevik 2006-03-13
 * *	Lagt till parameter i saveOrderRow f�r att styra om commondata skall sparas. Detta f�r att det
 * *	blir fel n�r en egenavgift skall sparas, d� commondata p� en idrad f�rsvinna.
 *
 * * * *** Version 1.1.5.35 uppdaterad av Joakim Brenevik 2006-03-13
 * *	Samma som ovan fast felet uppstod vid leverans ist�llet. �ven lagt 
 *		in kontroll p� commdata i saveOrderRow, k�rs aldrig om det �r en EA
 *
 * * * *** Version 1.1.5.36 uppdaterad av Joakim Brenevik 2006-03-13
 * *	Fixat problem med texter som inte h�ngde med vid nytt hj�lpmedel (vidade f�reg. hj�lpmedels text)
 *
 * * * *** Version 1.1.5.37 uppdaterad av Joakim Brenevik 2006-03-13
 * *	�ndrat hanteringen av datum. Nu kan datum anges under detaljer och detta datum plockas sedan med 
 * * och anv�nds vid leverans
 *
 * * * *** Version 1.1.5.38 uppdaterad av Joakim Brenevik 2006-04-13
 * *	R�ttat status p� �ppen/st�ngd order som inte visades r�tt
 * * Lagt till egenavgifter vid kreditering av hj�lpmedel
 * * Lagt till kontroll av antal p� artikelrad s� att nollv�rden inte accepteras
 * * �ndrat hanteringen av namn s� att vi kan hantera dubbelnamn i efternamn.
 * * Ut�kad logik f�r Thord och behovstrappa (se manual f�r configure.xml)
 * 
 * * * *** Version 1.1.5.39 uppdaterad av Joakim Brenevik 2006-05-12
 * *	�ndrat hanteringen av behovstrappan f�r Thord. Nu visas de r�tta texterna.
 *
 * * * *** Version 1.1.5.40 uppdaterad av Joakim Brenevik 2006-05-19
 * *	R�ttat fel som vid val av Begovstrappa 20,21. Detta berodde p� att det f�rs�ktes
 *   v�ljas ett index p� 20,21 ist�llet f�r alternativet som b�rjade p� 20,21.
 *   Nu g�rs ist�llet en FindString()
 * 
 * * * *** Version 1.1.5.41 uppdaterad av Joakim Brenevik 2006-06-19
 * *	Lagt in "Enter As Tab" i formul�r f�r kunduppl�gg
 * * P�b�rjat inl�ggning av konfigurationsfil och inst�llningar f�r gr�nssnitt
 * * Lagt in b�rjan p� en "MainMenu"
 * * R�ttat hantering av textrader (se Trac #10)
 *
 * * * *** Version 1.1.5.42 uppdaterad av Joakim Brenevik 2006-07-17
 * *	Beta till ver 2.0.6.1, se trac f�r detaljer
 * 
 * * * *** Version 1.1.5.43 uppdaterad av Joakim Brenevik 2006-09-XX
 * *	Beta till ver 2.0.6.1, se trac f�r detaljer
 *
 * * * * *** Version 1.1.5.43 uppdaterad av Joakim Brenevik 2006-10-19
 * *	Beta till ver 2.0.6.1, se trac f�r detaljer
 *
 * * * * * *** Version 2.0.7.0 uppdaterad av Joakim Brenevik 2008-01-07
 * *  Lagt till hantering av statistik p� egenavgifter
 * *  Thord slutf�rt enligt spec.
 *
 * * * * * * *** Version 2.0.7.1 uppdaterad av Joakim Brenevik 2008-01-14
 * *  Lagt till sparafunktion vid flera knappar. Detta d� det i vissa l�gen kunde ske att postning ej 
 *    genomf�rdes innan en uppdatering av posten skedde. T ex kunde detta ske vid v�xling med "F�reg�ende patient"
 *    men �ven i flera andra funktioner
 * 
 * * * * * * * *** Version 2.0.7.1 uppdaterad av Joakim Brenevik 2008-01-14
 * *  R�ttat radering av rader som alltid gjorde an h�mtning av referral, �ven om det inte var en Thord remiss
 * 
 * 
 * * * * * * * *** Version 2.0.10.0 uppdaterad av Joakim Brenevik 2008-09-29
 * Finns detaljerad beskrivning i P�_ver_10.docx
 * 
 * * * * * * * *** Version 2.0.10.2 uppdaterad av Joakim Brenevik 2008-11-10
 * Finns detaljerad beskrivning i Patient�versikt_ver_2.0.10.2.docx
 * 
 * * * * * * * *** Version 2.0.10.3 uppdaterad av Joakim Brenevik 2008-11-12
 * Finns detaljerad beskrivning i Patient�versikt_ver_2.0.10.3.docx
 * 
 
*/

[assembly: AssemblyVersion("3.0.0.22")]
[assembly: AssemblyTitle("Patient�versikt")]
[assembly: AssemblyDescription("Hantering av remisser och hj�lpmedel f�r ortopedtekniska verkst�der")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("GC Solutions AB")]
[assembly: AssemblyProduct("Patient�versikt")]
[assembly: AssemblyCopyright("� 2007 GC Solutions AB")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
[assembly: ComVisibleAttribute(false)]
