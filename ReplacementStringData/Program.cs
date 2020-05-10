using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Text;

namespace ReplacementStringData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /* pathway for read */
            string path = @"d:\Downloads\financePM.txt";

            /* pathway for change */
            string pathChange = @"d:\Downloads\financePM_2.txt";

            /* string for readed data */
            string data = "";

            // reading txt file from path
            data = await fileReader(path);

            // changing data
            data = dataChanger(data);

            // writing data to txt file
            await fileWriter(data, pathChange);

        }

        /// <summary> Changes the amount values by dividing by 10000 </summary>
        /// <param name="data"> data fo change </param>
        /// <returns> changed data </returns>
        private static string dataChanger(string data)
        {
            // converting the string data to array
            string[] words = data.Split(new char[] { '"' });

            // replacement of sum data
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "sum")
                {
                    IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                    double d = double.Parse(words[i + 2], formatter) / 10000;

                    if (d > (int)d || d < 1)
                    {
                        words[i + 2] = Convert.ToString(d).Replace(",", ".");
                    }
                    else
                    {
                        words[i + 2] = Convert.ToString(d) + ".0";
                    }
                }
            }

            // converting changed data array to string data
            StringBuilder changingData = new StringBuilder();

            for (int i = 0; i < words.Length - 1; i++)
            {
                changingData.Append(words[i] + '"');
            }

            changingData.Append(words[words.Length - 1]);

            data = changingData.ToString();

            Console.WriteLine("Данные изменены");

            return data;
        }

        /// <summary>
        /// Reading data from txt file
        /// </summary>
        /// <param name="path"> pathway to txt file </param>
        /// <returns> string data from txt file</returns>
        static async Task<string> fileReader(string path)
        {
            string str = "";
            // reading txt file from path
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    str = await sr.ReadToEndAsync();
                    Console.WriteLine("Чтение файла");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Сорян, чувак, такого файла нет");
            }
            Console.WriteLine("Файл прочитан");
            return str;
        }

        /// <summary>
        /// write string data to txt file
        /// </summary>
        /// <param name="str"> string for write </param>
        /// <param name="path"> pathway for write </param>
        static async Task fileWriter(string str, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(str);
                }

                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
