/**
 * Copyright (C) 2021 Miris Wisdom
 * 
 * This file is part of Delvar.
 * 
 * Delvar is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; version 2.
 * 
 * Delvar is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Delvar.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.IO;
using Mono.Options;
using static System.Console;
using static System.Environment;
using static System.IO.File;

namespace Delvar
{
  internal class Program
  {
    public static OptionSet OptionSet = new()
    {
      {
        "f=|file=|path=",
        "file with tab-delimited data; default = 'data.tsv'",
        s => Path = new FileInfo(s)
      },
      {
        "d=|del=|delimiter=",
        "delimiter character; default = '\\t'",
        s => Delimiter = s
      },
      {
        "v=|var=|variable=|id|identifier=",
        "symbol to use to identify a variable; default = $",
        s => Variable = s
      },
      {
        "q=|quote=",
        "symbol to use for value quotation; default = '",
        s => Quote = s
      },
      {
        "h|help", "show usage instructions",
        s =>
        {
          if (s != null)
            Help();
        }
      }
    };

    public static FileInfo Path      { get; set; } = new("data.tsv");
    public static string   Delimiter { get; set; } = "\t";
    public static string   Variable  { get; set; } = "$";
    public static string   Quote     { get; set; } = "'";

    private static void Main(string[] args)
    {
      var raw  = string.Join(' ', OptionSet.Parse(args));
      var data = ReadAllLines(Path.FullName);

      foreach (var line in data)
      {
        var seq = line.Split(Delimiter);
        var lex = raw;

        for (var i = 0; i < seq.Length; i++)
        {
          var id = $"{Variable}{i}";

          if (lex.Contains(id))
            lex = lex.Replace(id, $"{Quote}{seq[i]}{Quote}");
        }

        WriteLine(lex);
      }
    }

    private static void Help()
    {
      OptionSet.WriteOptionDescriptions(Out);
      Exit(0);
    }
  }
}