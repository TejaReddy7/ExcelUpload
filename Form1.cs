using Azure.Identity;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // for local testing 
            // var blobClient = new Azure.Storage.Blobs.BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=techeffautomation;AccountKey=GwHx5feXxqFHNC8CGaXWyq3/+diuZaEn1cqz+Gi7WQqlZAo2sEmx5vE6N8H7VcM8sPicKtoUH0MW+AStN3zX4Q==;EndpointSuffix=core.windows.net");
            var storageEndpoint = "DefaultEndpointsProtocol=https;AccountName=bincetecheffd931146stg01;AccountKey=twJ89WMhTda7XCB4zIDnE7+jRnHS5xSFl1u4ir2+ncxoX8mQBxJxxjjFoCyOkZOcb9jJR3HmpfcK+AStcIFuLQ==;EndpointSuffix=core.windows.net";
            var blobClient = new Azure.Storage.Blobs.BlobServiceClient(storageEndpoint);

            var conStr = new Uri($"https://techeffautomation.blob.core.windows.net");

            var credential = new DefaultAzureCredential();
            //var blobClient = new Azure.Storage.Blobs.BlobServiceClient(conStr, credential);
            var file = new FileService(blobClient);

            List<TruckLoadDetails> truckLoadDetailsList = new List<TruckLoadDetails>
        {
            new TruckLoadDetails
            {
                FactoryId = "F001",
                ClusterId = "C001",
                CascadeId = "CA001",
                SlNo = 1,
                SecondWeightRefNo = 12345,
                In = new DateTime(2024, 12, 19, 8, 0, 0),
                Out = new DateTime(2024, 12, 19, 12, 0, 0),
                ChallanNo = "CH123",
                VehicleNo = "BH01AB1234", 
                MaterialID = "M001",
                Material = "Steel",
                MaterialActualName = "High-Grade Steel",
                SupplierOrCustomer = "Supplier A",
                Destination = "Warehouse 1",
                Remark = "Urgent",
                Carrier = "Carrier X",
                FirstReadingWeight = 5000,
                SecondReadingWeight = 4800,
                NetWeight = 200,
                InMachine = "Machine A",
                OutMachine = "Machine B"
            },
            new TruckLoadDetails
            {
                FactoryId = "F002",
                ClusterId = "C002",
                CascadeId = "CA002",
                SlNo = 2,
                SecondWeightRefNo = 12346,
                In = new DateTime(2024, 12, 19, 9, 0, 0),
                Out = new DateTime(2024, 12, 19, 13, 0, 0),
                ChallanNo = "CH124",
                VehicleNo = "BH01AB1235",
                MaterialID = "M002",
                Material = "Aluminum",
                MaterialActualName = "Pure Aluminum",
                SupplierOrCustomer = "Customer B",
                Destination = "Factory 2",
                Remark = "Standard",
                Carrier = "Carrier Y",
                FirstReadingWeight = 6000,
                SecondReadingWeight = 5800,
                NetWeight = 200,
                InMachine = "Machine C",
                OutMachine = "Machine D"
            }
        };

           await file.ConvertJsonToExcelAndUpload(truckLoadDetailsList, "weighbridgedata.xlsx", "techeffweighbridgeautomation");
        }
    }
}
