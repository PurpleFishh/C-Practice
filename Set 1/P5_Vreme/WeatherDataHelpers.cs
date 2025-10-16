using System.Collections.Immutable;

namespace P5_Vreme
{
    public static class WeatherDataHelpers
    {
        public static readonly ImmutableDictionary<string, Dictionary<Season, double>> ImmutableDictionary = new Dictionary<string, Dictionary<Season, double>>
        {
            ["Bucuresti"] = new Dictionary<Season, double> { { Season.Iarna, 1.0 }, { Season.Primavara, 12.0 }, { Season.Vara, 23.5 }, { Season.Toamna, 12.5 } },
            ["Cluj-Napoca"] = new Dictionary<Season, double> { { Season.Iarna, -1.0 }, { Season.Primavara, 10.0 }, { Season.Vara, 20.5 }, { Season.Toamna, 10.5 } },
            ["Timisoara"] = new Dictionary<Season, double> { { Season.Iarna, 1.5 }, { Season.Primavara, 12.5 }, { Season.Vara, 22.5 }, { Season.Toamna, 12.0 } },
            ["Iasi"] = new Dictionary<Season, double> { { Season.Iarna, 0.0 }, { Season.Primavara, 11.0 }, { Season.Vara, 22.0 }, { Season.Toamna, 11.0 } },
            ["Constanta"] = new Dictionary<Season, double> { { Season.Iarna, 3.0 }, { Season.Primavara, 12.5 }, { Season.Vara, 23.0 }, { Season.Toamna, 14.0 } },
            ["Brasov"] = new Dictionary<Season, double> { { Season.Iarna, -3.0 }, { Season.Primavara, 8.0 }, { Season.Vara, 18.0 }, { Season.Toamna, 8.5 } },
            ["Sibiu"] = new Dictionary<Season, double> { { Season.Iarna, -1.5 }, { Season.Primavara, 9.5 }, { Season.Vara, 19.5 }, { Season.Toamna, 9.5 } },
            ["Craiova"] = new Dictionary<Season, double> { { Season.Iarna, 1.5 }, { Season.Primavara, 12.0 }, { Season.Vara, 23.0 }, { Season.Toamna, 12.0 } },
            ["Oradea"] = new Dictionary<Season, double> { { Season.Iarna, 0.5 }, { Season.Primavara, 11.5 }, { Season.Vara, 22.0 }, { Season.Toamna, 11.5 } },
            ["Arad"] = new Dictionary<Season, double> { { Season.Iarna, 0.8 }, { Season.Primavara, 12.0 }, { Season.Vara, 22.2 }, { Season.Toamna, 11.8 } },
            ["Pitesti"] = new Dictionary<Season, double> { { Season.Iarna, 0.5 }, { Season.Primavara, 11.0 }, { Season.Vara, 21.5 }, { Season.Toamna, 11.0 } },
            ["Bacau"] = new Dictionary<Season, double> { { Season.Iarna, 0.0 }, { Season.Primavara, 10.5 }, { Season.Vara, 21.5 }, { Season.Toamna, 10.5 } },
            ["Targu Mures"] = new Dictionary<Season, double> { { Season.Iarna, -1.5 }, { Season.Primavara, 9.5 }, { Season.Vara, 20.0 }, { Season.Toamna, 9.5 } },
            ["Suceava"] = new Dictionary<Season, double> { { Season.Iarna, -1.0 }, { Season.Primavara, 9.0 }, { Season.Vara, 19.0 }, { Season.Toamna, 9.0 } },
            ["Baia Mare"] = new Dictionary<Season, double> { { Season.Iarna, -0.5 }, { Season.Primavara, 10.5 }, { Season.Vara, 21.0 }, { Season.Toamna, 10.5 } },
            ["Buzau"] = new Dictionary<Season, double> { { Season.Iarna, 1.0 }, { Season.Primavara, 12.0 }, { Season.Vara, 23.0 }, { Season.Toamna, 12.0 } },
            ["Ploiesti"] = new Dictionary<Season, double> { { Season.Iarna, 1.0 }, { Season.Primavara, 11.5 }, { Season.Vara, 22.5 }, { Season.Toamna, 11.5 } },
            ["Galati"] = new Dictionary<Season, double> { { Season.Iarna, 1.5 }, { Season.Primavara, 12.5 }, { Season.Vara, 23.5 }, { Season.Toamna, 12.5 } },
            ["Braila"] = new Dictionary<Season, double> { { Season.Iarna, 1.5 }, { Season.Primavara, 12.5 }, { Season.Vara, 23.5 }, { Season.Toamna, 12.5 } },
            ["Deva"] = new Dictionary<Season, double> { { Season.Iarna, 0.0 }, { Season.Primavara, 10.5 }, { Season.Vara, 21.0 }, { Season.Toamna, 10.5 } },
        }.ToImmutableDictionary();
    }
}