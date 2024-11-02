using LojaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace LojaAPI.Repositories
{
    public class ProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> CadastrarProdutoDB(Produto produto)
        {
            using (var conn = Connection)
            {
                var sql = @"INSERT INTO Produtos (Nome, Descricao, Preco, QuantidadeEstoque) 
                            VALUES (@Nome, @Descricao, @Preco, @QuantidadeEstoque);
                            SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, produto);
            }
        }

        public async Task<IEnumerable<Produto>> ListarProdutosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos";
                return await conn.QueryAsync<Produto>(sql);
            }
        }

        public async Task<Produto> ObterProdutoPorIdDB(int id)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
            }
        }

        public async Task<int> AtualizarProdutoDB(Produto produto)
        {
            using (var conn = Connection)
            {
                var sql = @"UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, 
                            Preco = @Preco, QuantidadeEstoque = @QuantidadeEstoque 
                            WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, produto);
            }
        }

        public async Task<int> ExcluirProdutoDB(int id)
        {
            using (var conn = Connection)
            {
                var sql = "DELETE FROM Produtos WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
