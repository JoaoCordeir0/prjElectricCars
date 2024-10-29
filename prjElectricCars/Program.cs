using System;
using System.Xml.Linq;

namespace prjElectricCars
{
    internal class Program
    {
        static void NumberOfCarsPerState(IEnumerable<RowModel> rows)
        {
            // Aplica o PLINQ para contar a quantidade de carros por estado
            var stateCounts = rows
                .AsParallel() 
                .GroupBy(row => row.State)
                .Select(group => new
                {
                    State = group.Key,
                    Count = group.Count()
                })
                .ToList();

            // Resultado
            foreach (var stateCount in stateCounts)
            {
                Console.WriteLine($"Estado: {stateCount.State}, Quantidade: {stateCount.Count}");
            }
        }

        static void AveragePricePerModel(IEnumerable<RowModel> rows) 
        {
            // Aplica o PLINQ para contar a quantidade de modelos e obter a soma de cada um
            var modelCounts = rows
                .AsParallel()
                .GroupBy(row => row.Model)
                .Select(group => new
                {
                    Model = group.Key,
                    Count = group.Count(),
                    Total = group.Sum(row => row.BaseMsrp)
                })
                .ToList();

            // Resultado
            foreach (var modelCount in modelCounts)
            {
                Console.WriteLine($"Modelo: {modelCount.Model}, Quantidade: {modelCount.Count}, Valor: {modelCount.Total}, Média: {modelCount.Total / modelCount.Count}");
            }
        }
       
        static void NumberOfChargingPointsPerState(IEnumerable<RowModel> rows)
        {
            // Aplica o PLINQ para contar a quantidade de pontos de carregamento por estado
            var chargingPointsByState = rows
                .AsParallel()
                .GroupBy(row => row.State)
                .Select(group => new
                {
                    State = group.Key,
                    ChargingPointCount = rows.Where(v => v.State == group.Key)
                        .GroupBy(row => row.ElectricUtility)
                        .Count()
                })
                .ToList();

            // Resultado
            foreach (var stateCount in chargingPointsByState)
            {
                Console.WriteLine($"Estado: {stateCount.State}, Pontos de Carregamento: {stateCount.ChargingPointCount}");
            }
        }

        static void AverageVehicleRangeByManufacturer(IEnumerable<RowModel> rows)
        {
            // Aplica o PLINQ para contar a quantidade de fabricantes e obter a média
            var modelCounts = rows
                .AsParallel()
                .GroupBy(row => row.Make)
                .Select(group => new
                {
                    Make = group.Key,
                    Count = group.Count(),
                    Total = group.Sum(row => row.ElectricRange)
                })
                .ToList();

            // Resultado
            foreach (var modelCount in modelCounts)
            {
                Console.WriteLine($"Fabricante: {modelCount.Make}, Quantidade: {modelCount.Count}, Valor: {modelCount.Total}, Média: {modelCount.Total / modelCount.Count}");
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

            // Método 02: Calcular o preço médio(campo Base MSRP) por modelo(campo Model).
            Console.WriteLine("Executando método 2");
            AveragePricePerModel(rows);
            Console.WriteLine("Fim do método 2\n\n");

            // Método 03: Calcular a quantidade de pontos de carregamento(campo Electric Utility) por estado(campo State).
            Console.WriteLine("Executando método 3");
            NumberOfChargingPointsPerState(rows);
            Console.WriteLine("Fim do método 3\n\n");

            // Método 04: Calcular a autonomia média do veículo (campo Electric Range) por fabricante (Make).
            Console.WriteLine("Executando método 4");
            AverageVehicleRangeByManufacturer(rows);
            Console.WriteLine("Fim do método 4\n\n");
        }
    }
}
