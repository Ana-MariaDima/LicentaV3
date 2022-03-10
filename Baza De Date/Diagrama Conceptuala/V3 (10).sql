CREATE TABLE `Utilizatori` (
  `id_utilizator` guid PRIMARY KEY AUTO_INCREMENT,
  `nume_utilizator` varchar(255),
  `prenume_utilizator` varchar(255),
  `username` varchar(255),
  `email` varchar(255),
  `parola` varchar(255),
  `moment_creare` timestamp,
  `activ` bool,
  `rol` varchar(255),
  `varsta` int,
  `sex` char,
  `poza_utilizator` varchar(255)
);

CREATE TABLE `Unitati` (
  `id_unitate` guid PRIMARY KEY AUTO_INCREMENT,
  `nume_unitate` varchar(255)
);

CREATE TABLE `Aprecieri` (
  `id_apreciere` guid PRIMARY KEY AUTO_INCREMENT,
  `id_reteta` guid,
  `id_utilizator` guid,
  `moment_apreciere` timestamp,
  `scor_apreciere` float
);

CREATE TABLE `Categorii_Ingrediente` (
  `id_categorie_ingredient` guid PRIMARY KEY AUTO_INCREMENT,
  `nume_categorie_ingredient` varchar(255),
  `poza_categorie_ingredient` varchar(255),
  `descriere_categorie_ingredient` varchar(255)
);

CREATE TABLE `SubCategorii_Ingrediente` (
  `id_subcategorie_ingredient` guid PRIMARY KEY AUTO_INCREMENT,
  `id_categorie_ingredient` guid,
  `nume_subcategorie_ingredient` varchar(255),
  `poza_subcategorie_ingredient` varchar(255),
  `descriere_subcategorie_ingredient` varchar(255)
);

CREATE TABLE `Categorii_Retete` (
  `id_categorie_reteta` guid PRIMARY KEY AUTO_INCREMENT,
  `nume_categorie_reteta` varchar(255)
);

CREATE TABLE `Tipuri_Retete` (
  `id_tip_reteta` guid PRIMARY KEY AUTO_INCREMENT,
  `nume_tip_reteta` varchar(255)
);

CREATE TABLE `Pahare` (
  `id_pahar` guid PRIMARY KEY AUTO_INCREMENT,
  `nume_pahar` varchar(255)
);

CREATE TABLE `Reteta_Ingrediente` (
  `id_reteta_ingredient` guid PRIMARY KEY AUTO_INCREMENT,
  `id_reteta` guid,
  `id_ingredient` guid,
  `cantitate_ingredient` float,
  `id_unitate` guid
);

CREATE TABLE `Ingrediente` (
  `id_ingredient` guid PRIMARY KEY AUTO_INCREMENT,
  `nume_ingredient` varchar(255),
  `id_subcategorie_ingredient` varchar(255)
);

CREATE TABLE `Retete` (
  `id_reteta` guid PRIMARY KEY AUTO_INCREMENT,
  `id_categorie_reteta` guid,
  `id_tip_reteta` guid,
  `id_pahar` guid,
  `nume_reteta` varchar(255),
  `descriere_reteta` varchar(255),
  `poza_reteta` varchar(255),
  `instructiuni_reteta` varchar(255),
  `rating_reteta` float
);

ALTER TABLE `Reteta_Ingrediente` ADD FOREIGN KEY (`id_ingredient`) REFERENCES `Ingrediente` (`id_ingredient`);

ALTER TABLE `Ingrediente` ADD FOREIGN KEY (`id_subcategorie_ingredient`) REFERENCES `SubCategorii_Ingrediente` (`id_subcategorie_ingredient`);

ALTER TABLE `SubCategorii_Ingrediente` ADD FOREIGN KEY (`id_categorie_ingredient`) REFERENCES `Categorii_Ingrediente` (`id_categorie_ingredient`);

ALTER TABLE `Reteta_Ingrediente` ADD FOREIGN KEY (`id_reteta`) REFERENCES `Retete` (`id_reteta`);

ALTER TABLE `Retete` ADD FOREIGN KEY (`id_categorie_reteta`) REFERENCES `Categorii_Retete` (`id_categorie_reteta`);

ALTER TABLE `Retete` ADD FOREIGN KEY (`id_tip_reteta`) REFERENCES `Tipuri_Retete` (`id_tip_reteta`);

ALTER TABLE `Retete` ADD FOREIGN KEY (`id_pahar`) REFERENCES `Pahare` (`id_pahar`);

ALTER TABLE `Reteta_Ingrediente` ADD FOREIGN KEY (`id_unitate`) REFERENCES `Unitati` (`id_unitate`);

ALTER TABLE `Aprecieri` ADD FOREIGN KEY (`id_utilizator`) REFERENCES `Utilizatori` (`id_utilizator`);

ALTER TABLE `Aprecieri` ADD FOREIGN KEY (`id_reteta`) REFERENCES `Retete` (`id_reteta`);
