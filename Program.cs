using System;
using System.Collections.Generic;

namespace COVID19Simulation
{
    class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int InfectionLevel { get; set; }
        public List<int> ConnectedCities { get; set; }

        public City(int id, string name)
        {
            CityID = id;
            CityName = name;
            InfectionLevel = 0;
            ConnectedCities = new List<int>();
        }
    }

    class Program
    {
        static List<City> cities = new List<City>();

        static void Main(string[] args)
        {
            Console.Write("Enter the number of cities: ");
            int numberOfCities = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCities; i++)
            {
                Console.WriteLine($"City {i}:");
                City city = CreateCity(i);
                cities.Add(city);
            }

            while (true)
            {
                Console.WriteLine("1.2 City Details:");
                foreach (var city in cities)
                {
                    Console.WriteLine($"City ID: {city.CityID}\tCity Name: {city.CityName}\tInfection Level: {city.InfectionLevel}");
                }

                Console.WriteLine("1.3 Enter an event (Outbreak, Vaccinate, Lock down, Spread, or Exit):");
                string eventInput = Console.ReadLine();

                switch (eventInput)
                {
                    case "Outbreak":
                    case "Vaccinate":
                    case "Lock down":
                        Console.Write("Enter the city ID: ");
                        int cityID = int.Parse(Console.ReadLine());

                        if (ValidateCityID(cityID))
                        {
                            switch (eventInput)
                            {
                                case "Outbreak":
                                    HandleOutbreak(cityID);
                                    break;
                                case "Vaccinate":
                                    HandleVaccination(cityID);
                                    break;
                                case "Lock down":
                                    HandleLockdown(cityID);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID");
                        }
                        break;

                    case "Spread":
                        HandleSpread();
                        break;

                    case "Exit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid event");
                        break;
                }
            }
        }

        static City CreateCity(int id)
        {
            Console.Write("Enter the city name: ");
            string cityName = Console.ReadLine();

            Console.Write("Enter the number of connected cities: ");
            int numberOfConnections = int.Parse(Console.ReadLine());

            List<int> connectedCities = new List<int>();

            for (int i = 0; i < numberOfConnections; i++)
            {
                Console.Write($"Enter the ID of connected city {i}: ");
                int connectedCityID = int.Parse(Console.ReadLine());

                if (ValidateConnectedCity(connectedCityID, connectedCities))
                {
                    connectedCities.Add(connectedCityID);
                }
                else
                {
                    Console.WriteLine("Invalid ID");
                    i--;
                }
            }

            return new City(id, cityName) { ConnectedCities = connectedCities };
        }

        static bool ValidateCityID(int cityID)
        {
            if (cityID < 0 || cityID >= cities.Count)
                return false;

            return true;
        }

       

