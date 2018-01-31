﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.Common.Extensions;

namespace SC.Common.Helpers
{
    public static class ParserHelper
    {

        /// <summary>
        /// X ,Y and Distance
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IList<object[]> GetPoints(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException("Log file not found");

            //iterate lines
            var lines = File.ReadLines(file);
            var points = new List<object[]>();

            //read lines
            foreach (var line in lines)
            {
                var values = line.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime dt;

                if (!DateTime.TryParseExact(values[0], "yyyy-MM-dd hh:mm:ss:fff", null, DateTimeStyles.None, out dt))
                    continue;

                var counter = 0;
                var row = new List<object>();
                for (var position = 1; position < values.Length - 1; position++)
                {
                    counter++;

                    //x,y and distance size
                    if (counter % 3 == 0 && counter > 0)
                    {
                        row.Add( new
                        {
                            x = values[position - 2].ToType<int>(),
                            y = values[position - 1].ToType<int>(),
                            z = values[position].ToType<int>()
                        });
                    }
                }

                points.Add(row.ToArray());
            }

            return points;
        }
    }
}