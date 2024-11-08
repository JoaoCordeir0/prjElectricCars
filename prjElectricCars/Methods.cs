﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjElectricCars
{
    internal class Methods
    {
        // Método 01: Calcular a quantidade de carros em cada estado (campo State) e a sua média.
        public void NumberOfCarsPerState(IEnumerable<RowModel> rows)
        {            
            Metrics metrics = new Metrics(01);

            metrics.StartClock();

            // Aplica o PLINQ para contar a quantidade de carros por estado
            var stateCounts = rows
                .AsParallel()
                .GroupBy(row => row.State)
                .Select(group => new
                {
                    State = group.Key,
                    Count = group.Count()
                })
                .Where(s => !string.IsNullOrEmpty(s.State)) // Filtro para não pegar estados vazios
                .ToList();

            decimal countCarSum = 0;
            int countState = 0;

            // Resultados
            foreach (var stateCount in stateCounts)
            {
                countCarSum += stateCount.Count;
                countState++;
                Console.WriteLine($"Estado: {stateCount.State}, Quantidade: {stateCount.Count}");
            }
            double media = (double)(countCarSum / countState);
            Console.WriteLine($"Média: {Math.Round(media, 2)}");

            metrics.EndClock();
        }

        // Método 02: Calcular o preço médio(campo Base MSRP) por modelo(campo Model).
        public void AveragePricePerModel(IEnumerable<RowModel> rows)
        {
            Metrics metrics = new Metrics(02);

            metrics.StartClock();

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
                .Where(s => !string.IsNullOrEmpty(s.Model) && s.Total > 0) // Filtro para não pegar modelos vazios e com total igual a 0
                .ToList();

            // Resultado
            foreach (var modelCount in modelCounts)
            {           
                double media = (double)(modelCount.Total / modelCount.Count);

                Console.WriteLine($"Modelo: {modelCount.Model}, Quantidade: {modelCount.Count}, Valor: {modelCount.Total}, Média: {Math.Round(media, 2)}");
            }

            metrics.EndClock();
        }

        // Método 03: Calcular a quantidade de pontos de carregamento(campo Electric Utility) por estado(campo State).
        public void NumberOfChargingPointsPerState(IEnumerable<RowModel> rows)
        {
            Metrics metrics = new Metrics(03);

            metrics.StartClock();

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
                .Where(s => !string.IsNullOrEmpty(s.State)) // Filtro para não pegar estados vazios
                .ToList();

            // Resultados
            foreach (var stateCount in chargingPointsByState)
            {
                Console.WriteLine($"Estado: {stateCount.State}, Pontos de Carregamento: {stateCount.ChargingPointCount}");
            }

            metrics.EndClock();
        }

        // Método 04: Calcular a autonomia média do veículo (campo Electric Range) por fabricante (Make).
        public void AverageVehicleRangeByManufacturer(IEnumerable<RowModel> rows)
        {
            Metrics metrics = new Metrics(04);

            metrics.StartClock();

            // Aplica o PLINQ para contar a quantidade de fabricantes e obter a média
            var makeCounts = rows
                .AsParallel()
                .GroupBy(row => row.Make)
                .Select(group => new
                {
                    Make = group.Key,
                    Count = group.Count(),
                    Total = group.Sum(row => row.ElectricRange)
                })
                .Where(s => !string.IsNullOrEmpty(s.Make) && s.Total > 0) // Filtro para não pegar fabricantes vazios e com total igual a 0
                .ToList();

            // Resultado
            foreach (var makeCount in makeCounts)
            {
                double media = (double)(makeCount.Total / makeCount.Count);

                Console.WriteLine($"Fabricante: {makeCount.Make}, Quantidade: {makeCount.Count}, Valor: {makeCount.Total}, Média: {Math.Round(media, 2)}");
            }

            metrics.EndClock();
        }

        // Monta e coleta a opção do menu que o usuário deseja
        public string ShowMenu()
        {
            Console.WriteLine("Projeto prático B2 - SISTEMAS DISTRIBUÍDOS E PROGRAMAÇÃO CONCORRENTE");
            Console.WriteLine("Escolha uma opção para executar:");
            Console.WriteLine("[1] - Síncrona");
            Console.WriteLine("[2] - Asíncrona com Tasks");
            Console.WriteLine("[3] - Asíncrona com Parallel");
            Console.WriteLine("[4] - Sair");
            Console.Write("Opção: ");
            string? option = Console.ReadLine();

            return option ?? "4";
        }
    }
}
