using LojaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace LojaAPI.Repositories
{
    public class PedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> RegistrarPedidoDB(Pedidos pedido)
        {
            using (var conn = Connection)
            {
                var sql = @"INSERT INTO Pedidos (UsuarioId, DataPedido, StatusPedido, ValorTotal) 
                            VALUES (@UsuarioId, @DataPedido, @StatusPedido, @ValorTotal);
                            SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, pedido);
            }
        }

        public async Task<IEnumerable<Pedidos>> ListarPedidosDB(int usuarioId)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Pedidos WHERE UsuarioId = @UsuarioId";
                return await conn.QueryAsync<Pedidos>(sql, new { UsuarioId = usuarioId });
            }
        }

        public async Task<int> AtualizarStatusPedidoDB(int pedidoId, string novoStatus)
        {
            using (var conn = Connection)
            {
                var sql = @"UPDATE Pedidos SET StatusPedido = @NovoStatus WHERE Id = @PedidoId";
                return await conn.ExecuteAsync(sql, new { NovoStatus = novoStatus, PedidoId = pedidoId });
            }
        }
    }
}
