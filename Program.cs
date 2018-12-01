﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MoviesChallenge.Models;
using MoviesChallenge.Models.Config;

namespace MoviesChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var weeklyHoursOfOperation = new WeeklyHoursOfOperation();
            configuration.GetSection("HoursOfOperation").Bind(weeklyHoursOfOperation);

            
            using (var inputStream = File.OpenText(args[0]))
            {
                string line;
                while ((line = inputStream.ReadLine()) != null) {

                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}