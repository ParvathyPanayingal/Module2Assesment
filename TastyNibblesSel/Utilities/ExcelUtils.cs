using ExcelDataReader;
using System.Data;
using System.Text;

namespace TastyNibblesSel.Utilities
{
    internal class ExcelUtils
    {

        public static List<BuyProductExcelData> ReadSearchExcelData(string excelFilePath, string sheetName)
        {
            List<BuyProductExcelData> excelDataList = new List<BuyProductExcelData>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            BuyProductExcelData excelData = new BuyProductExcelData
                            {
                                SearchText = GetValueOrDefault(row, "searchtext"),
                                ContactNo = GetValueOrDefault(row, "contactNo"),
                                FirstName = GetValueOrDefault(row, "firstName"),
                                LastName = GetValueOrDefault(row, "lastName"),
                                Address = GetValueOrDefault(row, "address"),
                                City = GetValueOrDefault(row, "city"),
                                Pincode = GetValueOrDefault(row, "pincode")
                            };

                            excelDataList.Add(excelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return excelDataList;
        }
        public static List<CreateAccountExcelData> ReadCreateAccExcelData(string excelFilePath, string sheetName)
        {
            List<CreateAccountExcelData> excelDataList = new List<CreateAccountExcelData>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CreateAccountExcelData excelData = new CreateAccountExcelData
                            {
                                FirstName = GetValueOrDefault(row, "firstName"),
                                LastName = GetValueOrDefault(row, "lastName"),
                                Email = GetValueOrDefault(row, "email"),
                                Password = GetValueOrDefault(row, "password"),
                               
                            };

                            excelDataList.Add(excelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return excelDataList;
        }


        static string GetValueOrDefault(DataRow row, string columnName)
        {
            
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }



    }
}