# Moj-Vrt
Prvi projekt pri predmetu: Napredna uporaba podatkovnih baz
 <br/>
 <br/>
PRVI ZAGON KODE (z tem poskrbimo, da se bodo vse poti do datotek na novo generirale in <br/>
tako ne bo težav napakami kot so mankajoče datoteke): <br/>
-V Visual studijo izberemo zavihek Build <br/>
-kliknemo na podzavihek Rebuild soluton <br/>
 <br/>
Tako smo na novo določili kje se naše datoteke nahajajo. <br/>
Zaradi uporabljenega dodatka SQLite, ki se ne prenese avtomatsko z kodo, ga moramo inštalirati. <br/>
 <br/>
Link: https://visualstudiogallery.msdn.microsoft.com/1d04f82f-2fe9-4727-a2f9-a2db127ddc9a <br/>
 <br/>
Po inštalaciji Visual Stuio na novo zaženemo. <br/>
 <br/>
V Solution Explorerju izberemo: <br/>
-References in nanj desno kliknemo <br/>
-izberemo Manage NuGet packages... <br/>
-Inštaliramo: <br/>
	-sqlite-net <br/>
	-SQLitePCL <br/>
 <br/>
Da odpravimo rumene trikotnike zraven dodanih referenc moramo pri Debug izbrati x86 (namesto Any CPU) <br/>
 <br/>
Po tem lahko projekt zaženemo. <br/>
 <br/>
 <br/>
DELOVANJE: <br/>
 <br/>
Projekt je sestavljen iz dveh delov in dodatne aplikacije: <br/>
-Izdelava vrta <br/>
-Pregled vrta <br/>
-Dodatna aplikacija za nalaganje vrta na in iz Azure podatkovne baze <br/>
(link: ) <br/>
(podatkovna baza Azure: https://azurehowto101.wordpress.com/uvod-v-microsoft-azure/) <br/>
 <br/>
Prvi del aplikacije - Izdelava vrta nam omogoča: <br/>
-Izbrati velikost vrta <br/>
-Razdeliti vrt (max. 4x4) <br/>
-Dodati rastline v vrt <br/>
 <br/>
Drugi del aplikacije - Pregled vrta nam omogoča <br/>
-Izbrati vrt iz lokalne podatkovne baze in pregledati njegovo velikost, razdelitev in dodane rastline <br/>
 <br/>
Dodatna aplikacija (link: ) <br/>
 <br/>
 <br/>
OPIS DELOV KODE: <br/>
 <br/>
-Razdelitev vrta <br/>
https://github.com/JakaJenko/Moj-Vrt/blob/master/Koda%20za%20razdelitev <br/>
 <br/>
-Shranjevanje pozicije rastlin v bazo <br/>
 https://github.com/JakaJenko/Moj-Vrt/blob/master/Koda%20za%20shranjevanje%20rastline<br/>
 <br/>
-Izris shranjenega vrta <br/>
https://github.com/JakaJenko/Moj-Vrt/blob/master/Koda%20za%20izris%20shranjenega%20vrta <br/>
 <br/>
-Primer podatkov v bazi <br/>
https://github.com/JakaJenko/Moj-Vrt/blob/master/Primer%20podatkov%20iz%20lokalne%20baze
