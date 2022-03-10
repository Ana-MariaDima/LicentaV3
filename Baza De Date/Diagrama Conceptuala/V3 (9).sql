CREATE TABLE "Utilizatori" (
  "id_utilizator" SERIAL PRIMARY KEY,
  "nume_utilizator" varchar,
  "prenume_utilizator" varchar,
  "username" varchar,
  "email" varchar,
  "parola" varchar,
  "moment_creare" timestamp,
  "activ" bool,
  "rol" varchar,
  "varsta" int,
  "sex" char,
  "poza_utilizator" varchar
);

CREATE TABLE "Unitati" (
  "id_unitate" SERIAL PRIMARY KEY,
  "nume_unitate" varchar
);

CREATE TABLE "Aprecieri" (
  "id_apreciere" SERIAL PRIMARY KEY,
  "id_reteta" guid,
  "id_utilizator" guid,
  "moment_apreciere" timestamp,
  "scor_apreciere" float
);

CREATE TABLE "Categorii_Ingrediente" (
  "id_categorie_ingredient" SERIAL PRIMARY KEY,
  "nume_categorie_ingredient" varchar,
  "poza_categorie_ingredient" varchar,
  "descriere_categorie_ingredient" varchar
);

CREATE TABLE "SubCategorii_Ingrediente" (
  "id_subcategorie_ingredient" SERIAL PRIMARY KEY,
  "id_categorie_ingredient" guid,
  "nume_subcategorie_ingredient" varchar,
  "poza_subcategorie_ingredient" varchar,
  "descriere_subcategorie_ingredient" varchar
);

CREATE TABLE "Categorii_Retete" (
  "id_categorie_reteta" SERIAL PRIMARY KEY,
  "nume_categorie_reteta" varchar
);

CREATE TABLE "Tipuri_Retete" (
  "id_tip_reteta" SERIAL PRIMARY KEY,
  "nume_tip_reteta" varchar
);

CREATE TABLE "Pahare" (
  "id_pahar" SERIAL PRIMARY KEY,
  "nume_pahar" varchar
);

CREATE TABLE "Reteta_Ingrediente" (
  "id_reteta_ingredient" SERIAL PRIMARY KEY,
  "id_reteta" guid,
  "id_ingredient" guid,
  "cantitate_ingredient" float,
  "id_unitate" guid
);

CREATE TABLE "Ingrediente" (
  "id_ingredient" SERIAL PRIMARY KEY,
  "nume_ingredient" varchar,
  "id_subcategorie_ingredient" varchar
);

CREATE TABLE "Retete" (
  "id_reteta" SERIAL PRIMARY KEY,
  "id_categorie_reteta" guid,
  "id_tip_reteta" guid,
  "id_pahar" guid,
  "nume_reteta" varchar,
  "descriere_reteta" varchar,
  "poza_reteta" varchar,
  "instructiuni_reteta" varchar,
  "rating_reteta" float
);

ALTER TABLE "Reteta_Ingrediente" ADD FOREIGN KEY ("id_ingredient") REFERENCES "Ingrediente" ("id_ingredient");

ALTER TABLE "Ingrediente" ADD FOREIGN KEY ("id_subcategorie_ingredient") REFERENCES "SubCategorii_Ingrediente" ("id_subcategorie_ingredient");

ALTER TABLE "SubCategorii_Ingrediente" ADD FOREIGN KEY ("id_categorie_ingredient") REFERENCES "Categorii_Ingrediente" ("id_categorie_ingredient");

ALTER TABLE "Reteta_Ingrediente" ADD FOREIGN KEY ("id_reteta") REFERENCES "Retete" ("id_reteta");

ALTER TABLE "Retete" ADD FOREIGN KEY ("id_categorie_reteta") REFERENCES "Categorii_Retete" ("id_categorie_reteta");

ALTER TABLE "Retete" ADD FOREIGN KEY ("id_tip_reteta") REFERENCES "Tipuri_Retete" ("id_tip_reteta");

ALTER TABLE "Retete" ADD FOREIGN KEY ("id_pahar") REFERENCES "Pahare" ("id_pahar");

ALTER TABLE "Reteta_Ingrediente" ADD FOREIGN KEY ("id_unitate") REFERENCES "Unitati" ("id_unitate");

ALTER TABLE "Aprecieri" ADD FOREIGN KEY ("id_utilizator") REFERENCES "Utilizatori" ("id_utilizator");

ALTER TABLE "Aprecieri" ADD FOREIGN KEY ("id_reteta") REFERENCES "Retete" ("id_reteta");
