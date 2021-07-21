using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using PPPAttendance.Dtos;
using PPPAttendance.Models;

namespace PPPAttendance.Services.TableServiceClient
{
    public class AzureTableClient
    {
        private readonly Azure.Data.Tables.TableServiceClient _tableServiceClient;
        private readonly TableClient _tableClient;

        public AzureTableClient()
        {
            // _tableServiceClient = GetTableServiceClient();
            // CreateTableIfNotExist();

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
            => _tableClient.CreateIfNotExists();

        public async Task<Response> AddEntityAsync(AttendanceEntity entity)
            => await _tableClient.AddEntityAsync<AttendanceEntity>(entity);

        public async Task<Response> UpdateEntityAsync(AttendanceEntity entity)
            => await _tableClient.UpdateEntityAsync<AttendanceEntity>(entity,
                                                                      ETag.All,
                                                                      TableUpdateMode.Replace);

        public async Task<AttendanceEntity?> GetEntity(AttendanceEntity entity)
        {
            var response = _tableClient
                .Query<AttendanceEntity>(a => a.Date.Date == entity.Date.Date
                                              && a.Service == entity.Service && a.RowKey == entity.RowKey);

            if (!response.Any())
                return default;

            var found = response.FirstOrDefault();

            if (found is null)
                return default;

            return found;
        }

        public async Task<IEnumerable<AttendanceReportDto>> GetReport(DateTime startDate,
                                                                      DateTime endDate)
        {
            var response = _tableClient
                           .Query<AttendanceEntity>(a => a.Date >= startDate)
                           .ToList();

            var responseFiltered =
                response.Where(x => x.Date.Date <= endDate.Date)
                        .Select(entity => new AttendanceReportDto
                        {
                            Men = entity.Men,
                            Women = entity.Women,
                            Children = entity.Children,
                            Service = entity.Service,
                            Date = entity.Date,
                            Total = entity.Women + entity.Men + entity.Children
                        })
                        .ToList();

            return responseFiltered;
        }
    }
}