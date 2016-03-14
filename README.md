# Moj-Vrt
Prvi projekt pri predmetu: Napredna uporaba podatkovnih baz


PRVI ZAGON KODE (z tem poskrbimo, da se bodo vse poti do datotek na novo generirale in
tako ne bo težav napakami kot so mankajoče datoteke):
-V Visual studijo izberemo zavihek Build
-kliknemo na podzavihek Rebuild soluton

Tako smo na novo določili kje se naše datoteke nahajajo.
Zaradi uporabljenega dodatka SQLite, ki se ne prenese avtomatsko z kodo, ga moramo inštalirati.

Link: https://visualstudiogallery.msdn.microsoft.com/1d04f82f-2fe9-4727-a2f9-a2db127ddc9a

Po inštalaciji Visual Stuio na novo zaženemo.

V Solution Explorerju izberemo:
-References in nanj desno kliknemo
-izberemo Manage NuGet packages...
-Inštaliramo:
	-sqlite-net
	-SQLitePCL

Da odpravimo rumene trikotnike zraven dodanih referenc moramo pri Debug izbrati x86 (namesto Any CPU)

Po tem lahko projekt zaženemo.


DELOVANJE:

Projekt je sestavljen iz dveh delov in dodatne aplikacije:
-Izdelava vrta
-Pregled vrta
-Dodatna aplikacija za nalaganje vrta na in iz Azure podatkovne baze
(link: )
(podatkovna baza Azure: https://azurehowto101.wordpress.com/uvod-v-microsoft-azure/)

Prvi del aplikacije - Izdelava vrta nam omogoča:
-Izbrati velikost vrta
-Razdeliti vrt (max. 4x4)
-Dodati rastline v vrt

Drugi del aplikacije - Pregled vrta nam omogoča
-Izbrati vrt iz lokalne podatkovne baze in pregledati njegovo velikost, razdelitev in dodane rastline

Dodatna aplikacija (link: )


OPIS DELOV KODE:

-Razdelitev vrta


-Shranjevanje pozicije rastlin v bazo


-Izris shranjenega vrta
