using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class FileService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> ConvertJsonToExcelAndUpload(List<TruckLoadDetails> truckLoadDetails, string fileName, string containerName)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("TruckLoadDetails");
                worksheet.Cell(1, 1).Value = "FactoryId";
                worksheet.Cell(1, 2).Value = "ClusterId";
                worksheet.Cell(1, 3).Value = "CascadeId";
                worksheet.Cell(1, 4).Value = "SlNo";
                worksheet.Cell(1, 5).Value = "SecondWeightRefNo";
                worksheet.Cell(1, 6).Value = "In";
                worksheet.Cell(1, 7).Value = "Out";
                worksheet.Cell(1, 8).Value = "ChallanNo";
                worksheet.Cell(1, 9).Value = "VehicleNo";
                worksheet.Cell(1, 10).Value = "MaterialID";
                worksheet.Cell(1, 11).Value = "Material";
                worksheet.Cell(1, 12).Value = "MaterialActualName";
                worksheet.Cell(1, 13).Value = "SupplierOrCustomer";
                worksheet.Cell(1, 14).Value = "Destination";
                worksheet.Cell(1, 15).Value = "Remark";
                worksheet.Cell(1, 16).Value = "Carrier";
                worksheet.Cell(1, 17).Value = "FirstReadingWeight";
                worksheet.Cell(1, 18).Value = "SecondReadingWeight";
                worksheet.Cell(1, 19).Value = "NetWeight";
                worksheet.Cell(1, 20).Value = "InMachine";
                worksheet.Cell(1, 21).Value = "OutMachine";

                for (int i = 0; i < truckLoadDetails.Count; i++)
                {
                    var item = truckLoadDetails[i];
                    worksheet.Cell(i + 2, 1).Value = item.FactoryId;
                    worksheet.Cell(i + 2, 2).Value = item.ClusterId;
                    worksheet.Cell(i + 2, 3).Value = item.CascadeId;
                    worksheet.Cell(i + 2, 4).Value = item.SlNo;
                    worksheet.Cell(i + 2, 5).Value = item.SecondWeightRefNo;
                    worksheet.Cell(i + 2, 6).Value = item.In;
                    worksheet.Cell(i + 2, 7).Value = item.Out;
                    worksheet.Cell(i + 2, 8).Value = item.ChallanNo;
                    worksheet.Cell(i + 2, 9).Value = item.VehicleNo;
                    worksheet.Cell(i + 2, 10).Value = item.MaterialID;
                    worksheet.Cell(i + 2, 11).Value = item.Material;
                    worksheet.Cell(i + 2, 12).Value = item.MaterialActualName;
                    worksheet.Cell(i + 2, 13).Value = item.SupplierOrCustomer;
                    worksheet.Cell(i + 2, 14).Value = item.Destination;
                    worksheet.Cell(i + 2, 15).Value = item.Remark;
                    worksheet.Cell(i + 2, 16).Value = item.Carrier;
                    worksheet.Cell(i + 2, 17).Value = item.FirstReadingWeight;
                    worksheet.Cell(i + 2, 18).Value = item.SecondReadingWeight;
                    worksheet.Cell(i + 2, 19).Value = item.NetWeight;
                    worksheet.Cell(i + 2, 20).Value = item.InMachine;
                    worksheet.Cell(i + 2, 21).Value = item.OutMachine;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                    await containerClient.CreateIfNotExistsAsync();
                    var blobClient = containerClient.GetBlobClient(fileName);

                    var response = await blobClient.UploadAsync(stream, new BlobHttpHeaders
                    {
                        ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    });

                    if(response.GetRawResponse().Status != 201)
                    {
                           throw new Exception($"Failed to upload file to Azure Blob Storage  { response.GetRawResponse().ReasonPhrase}");
                    }

                    return blobClient.Uri.ToString();
                }

            }
        }
    }
}
