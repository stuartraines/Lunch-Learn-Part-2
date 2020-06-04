using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Demo.Api.Configuration;
using Demo.Api.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using static Dapper.SimpleCRUD;

namespace Demo.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public ProductRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<int?> CreateAsync(string name, decimal price, bool active)
        {
            using (var connection = new MySqlConnection(_appSettings.Value.DBConnectionString))
            {
                SimpleCRUD.SetDialect(Dialect.MySQL);

                return await connection.InsertAsync(new Product { Name = name, Price = price, Active = active });
            }
        }

        public async Task<Product> GetAsync(int productId)
        {
            using (var connection = new MySqlConnection(_appSettings.Value.DBConnectionString))
            {
                SimpleCRUD.SetDialect(Dialect.MySQL);

                return await connection.GetAsync<Product>(productId);
            }
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            using (var connection = new MySqlConnection(_appSettings.Value.DBConnectionString))
            {
                SimpleCRUD.SetDialect(Dialect.MySQL);

                var products =  await connection.GetListAsync<Product>();

                return products.Where(p => p.Active);
            }
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            using (var connection = new MySqlConnection(_appSettings.Value.DBConnectionString))
            {
                SimpleCRUD.SetDialect(Dialect.MySQL);

                var rowsAffected = await connection.UpdateAsync(product);

                return rowsAffected == 1;
            }
        }
    }
}