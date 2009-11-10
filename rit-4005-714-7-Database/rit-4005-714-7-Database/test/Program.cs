using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ATS.Database {

  /// <summary> tests a flat database and the facade with string arrays. </summary>
  /// <remarks> <c>Test</c> is implemented as extension methods. </remarks>
  public static class Program {

    /// <summary> process using console i/o. </summary>
    public static void Main (string[] args) {
        new LocalDB<string>().Test(args);
    }

    /// <summary> process commands until end of file. </summary>
    /// <remarks> Can pass an input file name as argument to simplify use from VS 2008. </remarks>
    public static void Test (this IModel<string> db, string[] args) {
      if (args != null && args.Length > 0)
        Test(db, new StreamReader(args[0]), Console.Out);
      else
        Test(db, Console.In, Console.Out);
    }

    /// <summary> process commands until end of file. </summary>
    public static void Test (this IModel<string> db, TextReader input, TextWriter output) {
      // tab separates command words
      var tab = new Regex("\t");

      // until end of file
      string line;
      while ((line = input.ReadLine()) != null)
        try {
          // separate into words, if any
          var words = tab.Split(line);

          // trace
          output.WriteLine("|" + line + "| " + words.Length + " word(s)");

          // dispatch on first word
          switch (words[0]) {

          // add word...
          case "add": {
              var tail = new string[words.Length - 1];
              Array.Copy(words, 1, tail, 0, tail.Length);
              output.WriteLine("\t" + (db as DB<string>).Add(tuple => {
                if (tail.Length != tuple.Length)
                  return false;
                for (int n = 0; n < tail.Length; ++n)
                  if (tail[n] == null && tuple[n] != null
                      || tail != null && !tail[n].Equals(tuple[n]))
                    return false;
                return true;
              }, tail));
              continue;
            }

          // extract key-position pattern value-position // select valPos where pattern ~ keyPos (0..)
          case "extract": {
              var keyPos = int.Parse(words[1]); // non-negative?
              var key = new Regex(words[2]);
              var valPos = int.Parse(words[3]); // non-negative?
              string[] values = (db as DB<string>).Extract(
                tuple =>
                  tuple.Length > keyPos
                    && tuple.Length > valPos
                    && tuple[keyPos] != null // cannot match null
                    && key.IsMatch(tuple[keyPos]),
                tuple => tuple[valPos]);

              foreach (string value in values)
                output.WriteLine("\t" + value);
              continue;
            }

          // delete key-position pattern
          case "delete": {
              var keyPos = int.Parse(words[1]);
              var key = new Regex(words[2]);
              output.WriteLine("\t" + (db as DB<string>).Delete(
                tuple =>
                  tuple.Length > keyPos
                    && tuple[keyPos] != null // cannot match null
                    && key.IsMatch(tuple[keyPos])
              ));
              continue;
            }

          // enter word... // facade
          case "enter": {
              var tail = new string[words.Length - 1];
              Array.Copy(words, 1, tail, 0, tail.Length);
              output.WriteLine("\t" + db.Enter(tail));
              continue;
            }

          // search word... // facade
          case "search": {
              var tail = new string[words.Length - 1];
              Array.Copy(words, 1, tail, 0, tail.Length);
              string[][] values = db.Search(tail);

              // all have same length
              for (var j = 0; j < values[0].Length; ++ j) {
                for (var i = 0; i < values.Length; ++ i)
                  output.Write("\t" + values[i][j]);
                output.WriteLine();
              }
              continue;
            }

          // remove word... // facade
          case "remove": {
              var tail = new string[words.Length - 1];
              Array.Copy(words, 1, tail, 0, tail.Length);
              output.WriteLine("\t" + db.Remove(tail));
              continue;
            }

          default:
            throw new ArgumentException();
          }

        }
        catch {
          output.WriteLine("add word...\n"
            + "delete key-position pattern\n"
            + "enter word...\n"
            + "extract key-position pattern value-position\n"
            + "remove word...\n"
            + "search word...\n");
        }
    }
  }
}
