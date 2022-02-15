CREATE TABLE [Reteta_Ingrediente] (
  [id_reteta_ingredient] guid PRIMARY KEY IDENTITY(1, 1),
  [id_reteta] guid,
  [id_ingredient] guid,
  [cantitate_ingredient] float,
  [id_unitate] guid
)
GO

CREATE TABLE [Unitati] (
  [id_unitate] guid PRIMARY KEY IDENTITY(1, 1),
  [nume_unitate] nvarchar(255)
)
GO

CREATE TABLE [Ingrediente] (
  [id_ingredient] guid PRIMARY KEY IDENTITY(1, 1),
  [nume_ingredient] nvarchar(255),
  [id_categorie_ingredient] nvarchar(255)
)
GO

CREATE TABLE [SubCategorii_Ingrediente] (
  [id_subcategorie_ingredient] guid PRIMARY KEY IDENTITY(1, 1),
  [descriere_categorie_ingredient] nvarchar(255)
)
GO

CREATE TABLE [Categorii_Ingrediente] (
  [id_categorie_ingredient] guid PRIMARY KEY IDENTITY(1, 1),
  [descriere_categorie_ingredient] nvarchar(255)
)
GO

CREATE TABLE [Retete] (
  [id_reteta] guid PRIMARY KEY IDENTITY(1, 1),
  [id_categorie_reteta] guid,
  [id_tip_reteta] guid,
  [id_pahar] guid,
  [nume_reteta] nvarchar(255),
  [descriere_reteta] nvarchar(255),
  [poza_reteta] nvarchar(255),
  [instructiuni_reteta] nvarchar(255),
  [rating_reteta] float
)
GO

CREATE TABLE [Tipuri_Retete] (
  [id_tip_reteta] guid PRIMARY KEY IDENTITY(1, 1),
  [nume_tip_reteta] nvarchar(255)
)
GO

CREATE TABLE [Pahare] (
  [id_pahar] guid PRIMARY KEY IDENTITY(1, 1),
  [nume_pahar] nvarchar(255)
)
GO

CREATE TABLE [Categorii_Retete] (
  [id_categorie_reteta] guid PRIMARY KEY IDENTITY(1, 1),
  [nume_categorie_reteta] nvarchar(255)
)
GO

CREATE TABLE [Utilizatori] (
  [id_utilizator] guid PRIMARY KEY IDENTITY(1, 1),
  [nume_utilizator] nvarchar(255),
  [prenume_utilizator] nvarchar(255),
  [username] nvarchar(255),
  [parola] nvarchar(255),
  [moment_creere] timestamp
)
GO

CREATE TABLE [Aprecieri] (
  [id_apreciere] guid PRIMARY KEY IDENTITY(1, 1),
  [id_reteta] guid,
  [id_utilizator] guid,
  [moment_apreciere] timestamp
)
GO

ALTER TABLE [Reteta_Ingrediente] ADD FOREIGN KEY ([id_ingredient]) REFERENCES [Ingrediente] ([id_ingredient])
GO

ALTER TABLE [Ingrediente] ADD FOREIGN KEY ([id_categorie_ingredient]) REFERENCES [SubCategorii_Ingrediente] ([id_subcategorie_ingredient])
GO

ALTER TABLE [SubCategorii_Ingrediente] ADD FOREIGN KEY ([id_subcategorie_ingredient]) REFERENCES [Categorii_Ingrediente] ([id_categorie_ingredient])
GO

ALTER TABLE [Reteta_Ingrediente] ADD FOREIGN KEY ([id_reteta]) REFERENCES [Retete] ([id_reteta])
GO

ALTER TABLE [Retete] ADD FOREIGN KEY ([id_categorie_reteta]) REFERENCES [Categorii_Retete] ([id_categorie_reteta])
GO

ALTER TABLE [Retete] ADD FOREIGN KEY ([id_tip_reteta]) REFERENCES [Tipuri_Retete] ([id_tip_reteta])
GO

ALTER TABLE [Retete] ADD FOREIGN KEY ([id_pahar]) REFERENCES [Pahare] ([id_pahar])
GO

ALTER TABLE [Reteta_Ingrediente] ADD FOREIGN KEY ([id_unitate]) REFERENCES [Unitati] ([id_unitate])
GO

ALTER TABLE [Aprecieri] ADD FOREIGN KEY ([id_utilizator]) REFERENCES [Utilizatori] ([id_utilizator])
GO

ALTER TABLE [Aprecieri] ADD FOREIGN KEY ([id_reteta]) REFERENCES [Retete] ([id_reteta])
GO
