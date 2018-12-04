using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using MoviesChallenge.Models;
using MoviesChallenge.Models.Config;
using MoviesChallenge.Models.Enums;

namespace MoviesChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            IConfiguration configuration = builder.Build();

            var weeklyHoursOfOperation = configuration.GetSection("HoursOfOperation").Get<WeeklyHoursOfOperation>();
            var configInputFileHeaders = configuration.GetSection("InputFileHeaders").Get<Dictionary<string, string>>();

            var inputMetaData = new InputMetaData();
            mapInputValueHeaders(configInputFileHeaders, inputMetaData); // var x = Enum.GetName(typeof(MovieDataField), 0);


            using (var inputStream = File.OpenText(args[0]))
            {
                string line;
                getInputDataIndicies(inputStream.ReadLine(), inputMetaData);


                while ((line = inputStream.ReadLine()) != null)
                {
                    var lineInputArray = line.Split(',');
                    var dataItem = new MovieDataItem();
                    for (int i = 0; i < lineInputArray.Length; i++)
                    {
                        if (i == inputMetaData.getMetaDataValue(MovieDataField.RunTimeMinutes).ValueIndex)
                        {
                            //convert time to minutes
                            dataItem.setValue(MovieDataField.RunTimeMinutes, TimeSpan.Parse(lineInputArray[i]).TotalMinutes.ToString());
                        }
                        else
                        {
                            //find the data item whose valueIndex matches i, assign the value to it
                            var movieDataField = inputMetaData.getMovieDataFieldFromIndexValue(i);
                            dataItem.setValue(movieDataField, lineInputArray[i].Trim());


                        }
                    }
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("Hello World!");
        }

        static void getInputDataIndicies(string line, InputMetaData inputMetaData)
        {
            //tell me which index in a line of input is each value of MovieDataItem
            var lineArr = line.Split(',');
            for (int index = 0; index < lineArr.Length; index++)
            {
                MovieDataField dataField = inputMetaData.getMovieDataFieldFromHeaderValue(lineArr[index].Trim());
                inputMetaData.setMetaDataIndexValue(dataField, index);
            }
        }

        static void mapInputValueHeaders(Dictionary<string, string> configInputFileHeaders, InputMetaData inputMetaData)
        {
            foreach (string key in configInputFileHeaders.Keys)
            {
                Enum.TryParse(key, out MovieDataField dataField);
                inputMetaData.setMetaDataHeaderValue(dataField, configInputFileHeaders[key]);
            }
        }
    }
}
