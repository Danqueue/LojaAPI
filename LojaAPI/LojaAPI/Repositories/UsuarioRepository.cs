using LojaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace LojaAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> CadastrarUsuarioDB(Usuario usuario)
        {
            using (var conn = Connection)
            {
                var sql = @"INSERT INTO Usuarios (Nome, Email, Endereco) 
                            VALUES (@Nome, @Email, @Endereco);
                            SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, usuario);
            }
        }

        public async Task<IEnumerable<Usuario>> ListarUsuariosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Usuarios";
                return await conn.QueryAsync<Usuario>(sql);
            }
        }

        public async Task<Usuario> ObterUsuarioPorIdDB(int id)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Usuarios WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
            }
        }

        public async Task<int> AtualizarUsuarioDB(Usuario usuario)
        {
            using (var conn = Connection)
            {
                var sql = @"UPDATE Usuarios SET Nome = @Nome, Email = @Email, 
                            Endereco = @Endereco WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, usuario);
            }
        }

        public async Task<int> ExcluirUsuarioDB(int id)
        {
            using (var conn = Connection)
            {
                var sql = "DELETE FROM Usuarios WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
