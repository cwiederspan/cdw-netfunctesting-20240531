using Azure.Data.Tables;
using System.Threading.Tasks;

namespace MyFunctions {
    
    public class TableRepository {

        private readonly TableServiceClient Service;
        private readonly TableClient Table;

        public TableRepository() {

            var connectionString = Environment.GetEnvironmentVariable("TableCS");
            this.Service = new TableServiceClient(connectionString);
            this.Table = this.Service.GetTableClient("data");
        }

        public async Task Create(string partition, string row) {
            var entity = new TableEntity(partition, row);
            await this.Table.AddEntityAsync(entity);
        }

        public async Task<TableEntity> Read(string partition, string row) {
            return await this.Table.GetEntityAsync<TableEntity>(partition, row);
        }

        public async Task Delete(string partition, string row) {
            await this.Table.DeleteEntityAsync(partition, row);
        }
    }
}
