using System;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using PPPAttendance.Models;

namespace PPPAttendance.Services.TableServiceClient
{
    public class AzureTableClient
    {
        private readonly Azure.Data.Tables.TableServiceClient _tableServiceClient;
        private readonly TableClient _tableClient;

        public AzureTableClient()
        {
            _tableServiceClient = GetTableServiceClient();
            CreateTableIfNotExist();

            _tableClient = GetTableClient();
            CreateTableInService();
        }

        /// <summary>
        /// TableServiceClient: provides methods to interact with creating, listing and deleting tables
        /// </summary>
        /// <returns></returns>
        private Azure.Data.Tables.TableServiceClient GetTableServiceClient()
            => new Azure.Data.Tables.TableServiceClient(new Uri(TableSettings.Url),
                                                        new TableSharedKeyCredential(
                                                         TableSettings.AccountName,
                                                         TableSettings.AccountKey));

        private void CreateTableIfNotExist()
            => _tableServiceClient.CreateTableIfNotExists(TableSettings.TableName);

        /// <summary>
        /// TableClient: provides methods interact with table entity such as creating, querying, deleting entities within table
        /// </summary>
        /// <returns></returns>
        private TableClient GetTableClient()
            => new TableClient(new Uri(TableSettings.Url),
                               TableSettings.TableName,
                               new TableSharedKeyCredential(TableSettings.AccountName,
                                                            TableSettings.AccountKey));

        private void CreateTableInService()
            => _tableClient.Create();

        public async Task<Response> AddEntityAsync(Attendance entity)
            => await _tableClient.AddEntityAsync<Attendance>(entity);

        public  AsyncPageable<Attendance> GetEntity(Attendance entity)
        {
            var response =  _tableClient.QueryAsync<Attendance>(a 
                                                                    => a.Date == entity.Date);
            return response;
        }
    }
}