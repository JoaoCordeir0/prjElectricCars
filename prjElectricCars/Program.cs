using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace prjElectricCars
{
    internal class Program
    {
        static void NumberOfCarsPerState(IEnumerable<RowModel> rows)
        {
            // Usa PLINQ para contar a quantidade de carros por estado
            var stateCounts = rows
                .AsParallel() 
                .GroupBy(row => row.State)
                .Select(group => new
                {
                    State = group.Key,
                    Count = group.Count()
                })
                .ToList();
            
            foreach (var stateCount in stateCounts)
            {
                Console.WriteLine($"Estado: {stateCount.State}, Quantidade: {stateCount.Count}");
            }
        }

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
            
            // Método 01: Calcular a quantidade de carros em cada estado (campo State) e a sua média.
            Console.WriteLine("Executando método 1");            
            NumberOfCarsPerState(rows);
            Console.WriteLine("Fim do método 1\n\n");
        }
    }
}
