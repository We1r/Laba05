using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laba05
{
  internal class Laba05
  {
    static void Main(string[] args)
    {
      string path = "C:\\Users\\Егор\\source\\repos\\Laba05\\Derictory\\",
             typeFile = ".txt",
             str = "",
             text = "";

      Dictionary<string, string> words = new Dictionary<string, string>()
      {
        ["helo"] = "hello",
        ["hillo"] = "hello",
        ["helllo"] = "hello",
        ["hilo"] = "hello",
        ["hlo"] = "hello",
      };


      Console.Write("Введите имя файла:");
      string name = Console.ReadLine();

      FileStream fileReader = File.OpenRead(path + name + typeFile);
      byte[] array = new byte[fileReader.Length];
      fileReader.Read(array, 0, array.Length);
      string textFromFile = System.Text.Encoding.Default.GetString(array);
      fileReader.Close();

      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("(1 - исправить слово hello)");
      Console.WriteLine("(2 - заменить номер (012) 345-67-89)");
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("");
      Console.Write("Введите номер действия: ");
      int number = int.Parse(Console.ReadLine());
      
      if (number == 1)
      {
        char[] FileText = textFromFile.ToCharArray();
        for (int symbolCount = 0; symbolCount < FileText.Length; ++symbolCount)
        {
          if (FileText[symbolCount] == ' ' || FileText[symbolCount] == '.')
          {
            foreach (var Dict in words)
            {
              if (str == Dict.Key)
              {
                str = Dict.Value;
              }
            }
            text = text + str + FileText[symbolCount];
            str = "";
          }
          else if (FileText[symbolCount] == '\n')
          {
            text = text + str + FileText[symbolCount];
            str = "";
          }
          else
          {
            str = str + FileText[symbolCount];
          }

        }
        foreach (var Dict in words)
        {
          if (str == Dict.Key)
          {
            str = Dict.Value;
          }
        }
        text += str;

        FileStream fileWriter = new FileStream(path + name + typeFile, FileMode.OpenOrCreate);
        byte[] array1 = System.Text.Encoding.Default.GetBytes(text);
        fileWriter.Write(array1, 0, array1.Length);
        fileWriter.Close();

      } else if (number == 2)
      {
        Regex regex = new Regex(@".012.\s?345-67-89");
        textFromFile = regex.Replace(textFromFile, "+380 12 345 67 89");
        Console.WriteLine(textFromFile);
        FileStream fileWriter = new FileStream(path + name + typeFile, FileMode.OpenOrCreate);
        byte[] array1 = System.Text.Encoding.Default.GetBytes(textFromFile);
        fileWriter.Write(array1, 0, array1.Length);
        fileWriter.Close();

      }
      Console.WriteLine("Файл изменён");
      Console.ReadKey();
    }
  }
}
