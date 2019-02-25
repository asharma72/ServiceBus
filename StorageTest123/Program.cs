using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System;

namespace StorageTest123
{
    class Program
    {
        public static object CloudConfigurationManager { get; private set; }

        static void Main(string[] args)
        {
            //https://vkinfotek.com/azureqa/how-to-update-records-in-a-table-storage.html
            //https://www.michaelcrump.net/azure-tips-and-tricks85/
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            // CloudStorageAccount storageAccount1 = CloudStorageAccount.Parse(Microsoft.WindowsAzure.CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationSettings.AppSettings["StorageConnection"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("thankfulfor");
            table.CreateIfNotExists();
            //added this line
            //CreateMessage(table, new TestCreatetbl("I'm thankful for the time with my family", DateTime.Now));
            //added this line

            //added this line
            //GetMessage(table, "TestCreatetblApp", "I'm thankful for the time with my family");
            //GetAllMessages(table);
            //added this line

            //added these lines
            UpdateMessage(table, "TestCreatetblApp", "I'm thankful for the time with my family", "I'm thankful for the time with my family and friends Amit");
            //added these lines

            //table.Execute(update);


            Console.ReadKey();

        }
        static void CreateMessage(CloudTable table, TestCreatetbl message)
        {
            TableOperation insert = TableOperation.Insert(message);

            table.Execute(insert);
        }
        static void GetAllMessages(CloudTable table)
        {
            TableQuery<TestCreatetbl> query = new TableQuery<TestCreatetbl>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "ThanksApp"));

            Console.WriteLine("GetAllMessages begin");
            foreach (TestCreatetbl message in table.ExecuteQuery(query))
            {
                Console.WriteLine(message.Name);
                Console.WriteLine(message.Date);
            }
            Console.WriteLine("GetAllMessages ends");
        }
        static void GetMessage(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrieve = TableOperation.Retrieve<TestCreatetbl>(partitionKey, rowKey);

            TableResult result = table.Execute(retrieve);

            Console.WriteLine(((TestCreatetbl)result.Result).Date);
        }

        static void UpdateMessage(CloudTable table, string partitionKey, string rowKey, string newMessage)
        {
            TableOperation retrieve = TableOperation.Retrieve<TestCreatetbl>(partitionKey, rowKey);

            TableResult result = table.Execute(retrieve);

            TestCreatetbl thanks = (TestCreatetbl)result.Result;

            thanks.ETag = "*";
            thanks.Name = newMessage;

            if (result != null)
            {
                TableOperation update = TableOperation.Replace(thanks);

                table.Execute(update);
            }

        }
    }
}
