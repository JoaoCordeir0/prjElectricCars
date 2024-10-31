using Microsoft.VisualBasic.FileIO;
using System;
using System.Xml.Linq;

namespace prjElectricCars
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            // Carrega o XML
            XDocument xdoc = XDocument.Load("C:/Users/joaoc/Documents/Material/CarrosEletricos.xml");

            // Extraí as informações
            var rows = from row in xdoc.Descendants("row")
                select new RowModel
                {
                    Vin = (string?) row.Element("vin_1_10") ?? String.Empty,
                    County = (string?) row.Element("county") ?? String.Empty,
                    City = (string?) row.Element("city") ?? String.Empty,
                    State = (string?) row.Element("state") ?? String.Empty,
                    ZipCode = (string?) row.Element("zip_code") ?? String.Empty,
                    ModelYear = (int?) row.Element("model_year") ?? 0,
                    Make = (string?) row.Element("make") ?? String.Empty,
                    Model = (string?) row.Element("model") ?? String.Empty,
                    EvType = (string?) row.Element("ev_type") ?? String.Empty,
                    CafvType = (string?) row.Element("cafv_type") ?? String.Empty,
                    ElectricRange = (int?) row.Element("electric_range") ?? 0,
                    BaseMsrp = (decimal?) row.Element("base_msrp") ?? 0m,
                    LegislativeDistrict = (string?) row.Element("legislative_district") ?? String.Empty,
                    DolVehicleId = (string?) row.Element("dol_vehicle_id") ?? String.Empty,
                    GeocodedColumn = (string?) row.Element("geocoded_column") ?? String.Empty,
                    ElectricUtility = (string?) row.Element("electric_utility") ?? String.Empty,
                };

            // Instância da classe que contém os métodos
            Methods methods = new Methods();

            // Coleta como o usuário vai querer processar os métodos
            string option = methods.ShowMenu();

            while (option != "4")
            {                
                switch (option)
                {
                    case "1":
                        // Executa de forma síncrona
                        methods.NumberOfCarsPerState(rows);
                        methods.AveragePricePerModel(rows);
                        methods.NumberOfChargingPointsPerState(rows);
                        methods.AverageVehicleRangeByManufacturer(rows);
                        break;
                    case "2":
                        // Executa com tasks
                        Task method01 = Task.Run(() => methods.NumberOfCarsPerState(rows));
                        Task method02 = Task.Run(() => methods.AveragePricePerModel(rows));
                        Task method03 = Task.Run(() => methods.NumberOfChargingPointsPerState(rows));
                        Task method04 = Task.Run(() => methods.AverageVehicleRangeByManufacturer(rows));

                        method01.Wait();
                        method02.Wait();
                        method03.Wait();
                        method04.Wait();
                        break;
                    case "3":
                        // Executa com parallel
                        Parallel.Invoke(
                            () => methods.NumberOfCarsPerState(rows),
                            () => methods.AveragePricePerModel(rows),
                            () => methods.NumberOfChargingPointsPerState(rows),
                            () => methods.AverageVehicleRangeByManufacturer(rows)
                        );
                        break;
                    default:
                        Console.WriteLine("Opção inválida\n\n");
                        break;
                }

                option = methods.ShowMenu();
            }                            
        }
    }
}
