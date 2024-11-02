using LojaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace LojaAPI.Repositories
{
    public class CarrinhoRepository
    {
        private readonly string _connectionString;

        public CarrinhoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> AdicionarItemCarrinhoDB(Carrinho item)
        {
            using (var conn = Connection)
            {
                var sql = @"INSERT INTO Carrinho (UsuarioId, ProdutoId, Quantidade) 
                            VALUES (@UsuarioId, @ProdutoId, @Quantidade);
                            SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, item);
            }
        }

        public async Task<IEnumerable<Carrinho>> ListarItensCarrinhoDB(int usuarioId)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Carrinho WHERE UsuarioId = @UsuarioId";
                return await conn.QueryAsync<Carrinho>(sql, new { UsuarioId = usuarioId });
            }
        }

        public async Task<int> RemoverItemCarrinhoDB(int itemId)
        {
            using (var conn = Connection)
            {
                var sql = "DELETE FROM Carrinho WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, new { Id = itemId });
            }
        }
    }
}
