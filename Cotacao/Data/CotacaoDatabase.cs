namespace Cotacao.Data;

using Cotacao.Models;
using Dapper;
using Microsoft.Data.Sqlite;

public class CotacaoDatabase
{
    private readonly string _connectionString;

    public CotacaoDatabase(string databasePath = "cotacoes.db")
    {
        var builder = new SqliteConnectionStringBuilder
        {
            DataSource = databasePath,
            Mode = SqliteOpenMode.ReadWriteCreate
        };
        _connectionString = builder.ToString();
    }

    public SqliteConnection CreateConnection() => new SqliteConnection(_connectionString);

    public void Initialize()
    {
        using var connection = CreateConnection();
        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS ConfiguracaoEmpresa (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                NomeEmpresa TEXT NOT NULL,
                Nuit TEXT NOT NULL,
                Endereco TEXT NOT NULL,
                Contactos TEXT NOT NULL,
                Email TEXT NOT NULL,
                InformacaoBancaria TEXT NOT NULL,
                LogotipoPath TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Cotacao (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                NumeroCotacao TEXT NOT NULL,
                DataEmissao TEXT NOT NULL,
                ClienteNome TEXT NOT NULL,
                ClienteLocalizacao TEXT NOT NULL,
                ClienteNuit TEXT,
                Subtotal REAL NOT NULL,
                TaxaAplicavel REAL NOT NULL,
                ValorTaxa REAL NOT NULL,
                TotalGeral REAL NOT NULL,
                ResponsavelNome TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS CotacaoArtigo (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CotacaoId INTEGER NOT NULL,
                NumeroItem INTEGER NOT NULL,
                Designacao TEXT NOT NULL,
                Quantidade REAL NOT NULL,
                PrecoUnitario REAL NOT NULL,
                PrecoTotal REAL NOT NULL,
                FOREIGN KEY (CotacaoId) REFERENCES Cotacao(Id)
            );
        ");
    }
}
