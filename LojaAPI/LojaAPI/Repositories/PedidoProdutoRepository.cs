using LojaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace LojaAPI.Repositories
{
    public class PedidoProdutoRepository
    {
        private readonly string _connectionString;

        public PedidoProdutoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<PedidoProduto>> ListarPedidoProdutosPorPedidoId(int pedidoId)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM PedidoProdutos WHERE PedidoId = @PedidoId";
                return await conn.QueryAsync<PedidoProduto>(sql, new { PedidoId = pedidoId });
            }
        }

        public async Task<int> AdicionarPedidoProduto(PedidoProduto pedidoProduto)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO PedidoProdutos (PedidoId, ProdutoId, Quantidade, Preco) " +
                          "VALUES (@PedidoId, @ProdutoId, @Quantidade, @Preco); " +
                          "SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, pedidoProduto);
            }
        }

        public async Task<int> AtualizarPedidoProduto(PedidoProduto pedidoProduto)
        {
            using (var conn = Connection)
            {
                var sql = "UPDATE PedidoProdutos SET Quantidade = @Quantidade, Preco = @Preco " +
                          "WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, pedidoProduto);
            }
        }

        public async Task<int> RemoverPedidoProduto(int id)
        {
            using (var conn = Connection)
            {
                var sql = "DELETE FROM PedidoProdutos WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
