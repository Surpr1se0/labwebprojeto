CREATE TABLE Utilizador(
    Id_Utilizador INTEGER IDENTITY(1, 1) NOT NULL,
    nome NVARCHAR(50) NOT NULL,
    telefone NVARCHAR(20),

    PRIMARY KEY (Id_Utilizador),
)

CREATE TABLE Categoria(
Id_Categoria INTEGER IDENTITY(1,1) NOT NULL,
nome NVARCHAR(50) NOT NULL,

PRIMARY KEY (Id_Categoria),
)

CREATE TABLE Produtora(
Id_Produtora INTEGER IDENTITY(1,1) NOT NULL,
nome NVARCHAR(50),

PRIMARY KEY(Id_produtora),
)

CREATE TABLE Consola(
Id_Consola INTEGER IDENTITY(1,1) NOT NULL,
nome NVARCHAR(50) NOT NULL,

PRIMARY KEY(Id_Consola),
)

CREATE TABLE Jogo(
Id_Jogos INTEGER IDENTITY(1,1) NOT NULL,
nome NVARCHAR(70) NOT NULL,
foto NVARCHAR(100) NOT NULL,
foto1 NVARCHAR(100) NOT NULL,
foto_2 NVARCHAR (100) NOT NULL,
Id_Categoria INTEGER NOT NULL,
Id_Consola INTEGER NOT NULL,
Id_Produtora INTEGER NOT NULL,
preco MONEY NOT NULL,
descricao NVARCHAR(100) NOT NULL,
descricao1 NVARCHAR(200) NOT NULL,


PRIMARY KEY (Id_Jogos),
FOREIGN KEY(Id_Categoria) REFERENCES Categoria(Id_Categoria),
FOREIGN KEY(Id_Consola) REFERENCES Consola(Id_Consola),
FOREIGN KEY(Id_Produtora) REFERENCES Produtora(Id_Produtora),
)

CREATE TABLE Compra(
Id_Compra INTEGER IDENTITY(1,1) NOT NULL,
Id_Jogo INTEGER NOT NULL,
Id_Utilizador INTEGER NOT NULL,
data_compra DATETIME NOT NULL,

PRIMARY KEY(Id_Compra),
FOREIGN KEY(Id_Jogo) REFERENCES Jogo(Id_Jogos), 
FOREIGN KEY (Id_Utilizador) REFERENCES Utilizador(Id_Utilizador),
)

CREATE TABLE Favorito(
    Id_Favorito INTEGER IDENTITY(1, 1) NOT NULL,
    Id_Categoria INTEGER NOT NULL,
    Id_Utilizador INTEGER NOT NULL,

    PRIMARY KEY (Id_Favorito),
    FOREIGN KEY (Id_Categoria) REFERENCES Categoria(Id_Categoria),
    FOREIGN KEY (Id_Utilizador) REFERENCES Utilizador(Id_Utilizador)
)

