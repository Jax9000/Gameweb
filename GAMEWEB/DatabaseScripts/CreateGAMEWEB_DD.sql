CREATE TABLE Studia
(
	StudioID int NOT NULL PRIMARY KEY IDENTITY,
	Nazwa nvarchar(200) NOT NULL,
	DataZalozenia date NULL,
	IloscPracownikow int NULL,
	Zalozyciel nvarchar(500) NULL,
	Opis nvarchar(2000) NULL
)

CREATE TABLE Producenci
(
	ProducentID int NOT NULL PRIMARY KEY IDENTITY,
	Nazwa nvarchar(200) NOT NULL,
	Prezes nvarchar(200) NULL,
	DataZalozenia date NULL,
	Opis nvarchar(2000) NULL
)

CREATE TABLE Gatunki
(
	GatunekID int NOT NULL PRIMARY KEY IDENTITY,
	Nazwa nvarchar(200) NOT NULL,
)

CREATE TABLE Uprawnienia
(
	UprawnienieID int NOT NULL PRIMARY KEY IDENTITY,
	Nazwa nvarchar(200) NOT NULL,
	DodawanieWpisow bit NULL,
	DodawanieOcen bit NULL,
	UsuwanieWpisowOcen bit NULL,
	UsuwanieWlasnychWpisowOcen bit NULL
)

CREATE TABLE Uzytkownicy
(
	UzytkownikID int NOT NULL PRIMARY KEY IDENTITY,
	UprawnienieID int NULL FOREIGN KEY REFERENCES Uprawnienia(UprawnienieID),
	Nazwa nvarchar(200) NOT NULL,
	Haslo nvarchar(200) NOT NULL,
	Email nvarchar(200) NOT NULL,
	Plec nvarchar(1) NOT NULL,
	BlokadaKonta bit NOT NULL,
	DataUrodzenia date NULL,
	Zdjecie varbinary NULL,
	Opis nvarchar(2000) NULL
)

CREATE TABLE Ostrzezenia
(
	OstrzezenieID int NOT NULL PRIMARY KEY IDENTITY,
	OstrzegajacyID int NULL FOREIGN KEY REFERENCES Uzytkownicy(UzytkownikID),
	OstrzeganyID int NULL FOREIGN KEY REFERENCES Uzytkownicy(UzytkownikID),
	Opis nvarchar(1000) NOT NULL
)

CREATE TABLE Gry
(
	GraID int NOT NULL PRIMARY KEY IDENTITY,
	StudioID int NULL FOREIGN KEY REFERENCES Studia(StudioID),
	Tytul nvarchar(200) NOT NULL,
	DataWydania date NOT NULL,
	Opis nvarchar(2000) NULL
)

CREATE TABLE Platformy
(
	PlatformaID int NOT NULL PRIMARY KEY IDENTITY,
	ProducentID int NULL FOREIGN KEY REFERENCES Producenci(ProducentID),
	Nazwa nvarchar(200) NOT NULL,
	DataWydania date NOT NULL
)

CREATE TABLE Wpisy
(
	WpisID int NOT NULL PRIMARY KEY IDENTITY,
	GraID int NULL FOREIGN KEY REFERENCES Gry(GraID),
	UzytkownikID int NOT NULL FOREIGN KEY REFERENCES Uzytkownicy(UzytkownikID),
	Tytul nvarchar(200) NOT NULL,
	DataDodania date NOT NULL
)

CREATE TABLE Komentarze
(
	KomentarzID int NOT NULL FOREIGN KEY REFERENCES Wpisy(WpisID),
	WpisID int NULL FOREIGN KEY REFERENCES Wpisy(WpisID),
	Tresc nvarchar(500),

	PRIMARY KEY (KomentarzID)
)

CREATE TABLE Zdjecia
(
	ZdjecieID int NOT NULL FOREIGN KEY REFERENCES Wpisy(WpisID),
	PlikGraficzny varbinary NOT NULL

	PRIMARY KEY (ZdjecieID)
)

CREATE TABLE Recenzje
(
	RecenzjaID int NOT NULL FOREIGN KEY REFERENCES Wpisy(WpisID),
	DlugaTresc nvarchar(2000) NOT NULL

	PRIMARY KEY (RecenzjaID)
)

CREATE TABLE OcenyGier
(
	GraID int NOT NULL FOREIGN KEY REFERENCES Gry(GraID),
	UzytkownikID int NOT NULL FOREIGN KEY REFERENCES Uzytkownicy(UzytkownikID),
	OcenaOgolna int NOT NULL,
	Fabula int NOT NULL,
	Grafika int NOT NULL,

	PRIMARY KEY (GraID, UzytkownikID)
)

CREATE TABLE GatunkiGier
(
	GraID int NOT NULL FOREIGN KEY REFERENCES Gry(GraID),
	GatunekID int NOT NULL FOREIGN KEY REFERENCES Gatunki(GatunekID),

	PRIMARY KEY (GraID, GatunekID)
)

CREATE TABLE PlatformyGier
(
	GraID int NOT NULL FOREIGN KEY REFERENCES Gry(GraID),
	PlatformaID int NOT NULL FOREIGN KEY REFERENCES Platformy(PlatformaID),

	PRIMARY KEY (GraID, PlatformaID)
)

CREATE TABLE OcenyWpisow
(
	UzytkownikID int NOT NULL FOREIGN KEY REFERENCES Uzytkownicy(UzytkownikID),
	WpisID int NOT NULL FOREIGN KEY REFERENCES Wpisy(WpisID),
	BinarnaOcena bit NOT NULL,

	PRIMARY KEY (UzytkownikID, WpisID)
)