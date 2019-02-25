using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    class TestCreatetbl: TableEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public TestCreatetbl(string name, DateTime date)
        {
            Name = name;
            Date = date;
            PartitionKey = "TestCreatetblApp";
            RowKey = name;
        }

        public TestCreatetbl()
        {

        }

    }
}
