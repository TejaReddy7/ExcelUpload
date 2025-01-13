using Azure.Identity;
using Azure.Storage.Blobs;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // CBT endpoint
        private const string BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=techeffautomation;AccountKey=GwHx5feXxqFHNC8CGaXWyq3/+diuZaEn1cqz+Gi7WQqlZAo2sEmx5vE6N8H7VcM8sPicKtoUH0MW+AStN3zX4Q==;EndpointSuffix=core.windows.net";
        // uniliver endpoint
        //private const string BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=bincetecheffd931146stg01;AccountKey=twJ89WMhTda7XCB4zIDnE7+jRnHS5xSFl1u4ir2+ncxoX8mQBxJxxjjFoCyOkZOcb9jJR3HmpfcK+AStcIFuLQ==;EndpointSuffix=core.windows.net";
        private const string BlobContainerName = "techeffweighbridgeautomation";

        private async void button1_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\WeighBridgeData";
            string fileName = "weighbridgedata.xlsx";
            string filePath = Path.Combine(folderPath, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File '{fileName}' not found in the folder '{folderPath}'!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // for local testing 
                var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(BlobStorageConnectionString);

                // managed identity
                //var conStr = new Uri($"https://techeffautomation.blob.core.windows.net"); // CBT URL need to changed as per env
                //var credential = new DefaultAzureCredential();
                //var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(conStr, credential);


                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(BlobContainerName);

                await containerClient.CreateIfNotExistsAsync();

                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                using FileStream fs = File.OpenRead(filePath);
                await blobClient.UploadAsync(fs, overwrite: true);

                MessageBox.Show($"File '{fileName}' uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
