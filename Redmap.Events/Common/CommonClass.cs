using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Redmap.Events.DTO;
using Redmap.Events.Model;
using System.Collections.Generic;
using System.IO;

namespace Redmap.Events.Common
{
    /// <summary>
    /// Class with common functions.
    /// </summary>
    public static class CommonClass
    {
        /// <summary>
        /// Getting category id from enum by category name.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        public static int GetCategoryId(string categoryname)
        {
            int logCategoryId = 0;
            if (categoryname != null)
            {
                switch (categoryname.ToLower())
                {
                    case "error":
                        logCategoryId = (int)Categories.Error;
                        break;
                    case "failure":
                        logCategoryId = (int)Categories.Failure;
                        break;
                    case "successAudit":
                        logCategoryId = (int)Categories.SuccessAudit;
                        break;
                    case "information":
                        logCategoryId = (int)Categories.Information;
                        break;
                    case "summary":
                        logCategoryId = (int)Categories.Summary;
                        break;
                    case "warning":
                        logCategoryId = (int)Categories.Warning;
                        break;

                }
            }
            return logCategoryId;
        }

        /// <summary>
        /// Upload File To S3 Bucket.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="accesskey"></param>
        /// <param name="secertkey"></param>
        /// <param name="bucketname"></param>
        public static void UploadFileToS3(IFormFile file, string fileName, string accesskey, string secertkey, string bucketname)
        {
            using (var client = new AmazonS3Client(accesskey, secertkey, RegionEndpoint.APSoutheast1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = fileName,
                        BucketName = bucketname,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    fileTransferUtility.Upload(uploadRequest);
                }
            }
        }

       
        /// <summary>
        /// Replace Single Quotes with double
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceSingleQuotes(string text)
        {
            text = string.IsNullOrEmpty(text) ? text : text.Replace("'", "''"); ;
            return text;
        }
        /// <summary>
        /// Change Date Format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ChangeDateFormat(string text)
        {
            string returnDate = "";
            if(!string.IsNullOrEmpty(text))
            {
                var splitdate = text.Split('/');
                returnDate = splitdate[1]+"/"+ splitdate[0]+"/"+splitdate[2];
            }
            
            return returnDate;
        }

        /// <summary>
        /// Export To Excel
        /// </summary>
        public static byte[] ExportToExcel(IEnumerable<EventsDto> events)
        {
            byte[] content =null;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Events");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "EventId";
                worksheet.Cell(currentRow, 2).Value = "Catetory";
                worksheet.Cell(currentRow, 3).Value = "Message";
                worksheet.Cell(currentRow, 4).Value = "Summary";
                worksheet.Cell(currentRow, 5).Value = "Source";
                worksheet.Cell(currentRow, 6).Value = "EventCode";
                worksheet.Cell(currentRow, 7).Value = "Attachmemnt";
                worksheet.Cell(currentRow, 8).Value = "Time stamp";
                foreach (var evnt in events)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = evnt.EventId;
                    worksheet.Cell(currentRow, 2).Value = evnt.Category;
                    worksheet.Cell(currentRow, 3).Value = evnt.Message;
                    worksheet.Cell(currentRow, 4).Value = evnt.Summary;
                    worksheet.Cell(currentRow, 5).Value = evnt.Source;
                    worksheet.Cell(currentRow, 6).Value = evnt.Errorcode;
                    worksheet.Cell(currentRow, 7).Value = evnt.Attachment;
                    worksheet.Cell(currentRow, 8).Value = evnt.CreatedDate;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                     content = stream.ToArray();

                    return content;
                }

            }
        }
    }
}
