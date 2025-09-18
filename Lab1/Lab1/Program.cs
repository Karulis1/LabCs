using System.Text;

namespace GeneticSearch
{
    class Program
    {
        struct GeneticData
        {
            public string protein;
            public string organism;
            public string amino_acids;
        }

        static void Main(string[] args)
        {
            try
            {
                List<GeneticData> data = ReadGeneticData("sequences.txt");
                ProcessCommands("commands.txt", data, "genedata.txt");
                Console.WriteLine("Готово! Результат в genedata.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static List<GeneticData> ReadGeneticData(string filename)
        {
            var data = new List<GeneticData>();
            foreach (string line in File.ReadAllLines(filename))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('\t');
                if (parts.Length < 3) continue;

                string decoded = RLDecoding(parts[2]);
                if (!ValidAmino(decoded)) continue;

                data.Add(new GeneticData
                {
                    protein = parts[0].Trim(),
                    organism = parts[1].Trim(),
                    amino_acids = decoded
                });
            }
            return data;
        }

        static void ProcessCommands(string commandsFile, List<GeneticData> data, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                writer.WriteLine("Ivan");
                writer.WriteLine("Genetic search");
                writer.WriteLine(new string('-', 50));

                string[] commands = File.ReadAllLines(commandsFile);
                for (int i = 0; i < commands.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(commands[i])) continue;

                    string[] parts = commands[i].Split('\t');
                    if (parts.Length == 0) continue;

                    writer.Write($"{(i + 1):D3} ");

                    switch (parts[0].ToLower())
                    {
                        case "search":
                            if (parts.Length >= 2)
                                Search(data, RLDecoding(parts[1].Trim()), writer);
                            break;
                        case "diff":
                            if (parts.Length >= 3)
                                Diff(data, parts[1].Trim(), parts[2].Trim(), writer);
                            break;
                        case "mode":
                            if (parts.Length >= 2)
                                Mode(data, parts[1].Trim(), writer);
                            break;
                    }
                    writer.WriteLine(new string('-', 50));
                }
            }
        }

        static bool ValidAmino(string sequence)
        {
            if (string.IsNullOrEmpty(sequence)) return false;
            string valid = "ACDEFGHIKLMNPQRSTVWY";
            foreach (char c in sequence)
                if (valid.IndexOf(c) == -1) return false;
            return true;
        }

        static void Search(List<GeneticData> data, string sequence, TextWriter output)
        {
            output.WriteLine($"search\t{RLEncoding(sequence)}");
            bool found = false;
            foreach (var item in data)
            {
                if (item.amino_acids.Contains(sequence))
                {
                    output.WriteLine($"{item.organism}\t{item.protein}");
                    found = true;
                }
            }
            if (!found) output.WriteLine("NOT FOUND");
        }

        static void Diff(List<GeneticData> data, string protein1, string protein2, TextWriter output)
        {
            output.WriteLine($"diff\t{protein1}\t{protein2}");
            output.WriteLine("amino-acids difference: ");

            GeneticData p1 = FindProtein(data, protein1);
            GeneticData p2 = FindProtein(data, protein2);

            if (p1.protein == null || p2.protein == null)
            {
                string missing = "";
                if (p1.protein == null) missing += protein1;
                if (p2.protein == null) missing += (missing != "" ? ", " : "") + protein2;
                output.WriteLine($"MISSING: {missing}");
                return;
            }

            int diff = 0;
            int minLen = Math.Min(p1.amino_acids.Length, p2.amino_acids.Length);
            for (int i = 0; i < minLen; i++)
                if (p1.amino_acids[i] != p2.amino_acids[i]) diff++;

            diff += Math.Abs(p1.amino_acids.Length - p2.amino_acids.Length);
            output.WriteLine(diff);
        }

        static void Mode(List<GeneticData> data, string protein, TextWriter output)
        {
            output.WriteLine($"mode\t{protein}");
            output.WriteLine("amino-acid occurs: ");

            GeneticData p = FindProtein(data, protein);
            if (p.protein == null)
            {
                output.WriteLine($"MISSING: {protein}");
                return;
            }

            string seq = p.amino_acids;
            char mostCommon = seq[0];
            int maxCount = 0;

            foreach (char acid in "ACDEFGHIKLMNPQRSTVWY")
            {
                int count = 0;
                foreach (char c in seq)
                    if (c == acid) count++;

                if (count > maxCount || (count == maxCount && acid < mostCommon))
                {
                    mostCommon = acid;
                    maxCount = count;
                }
            }

            output.WriteLine($"{mostCommon} {maxCount}");
        }

        static GeneticData FindProtein(List<GeneticData> data, string proteinName)
        {
            foreach (var item in data)
                if (item.protein.Equals(proteinName, StringComparison.OrdinalIgnoreCase))
                    return item;
            return new GeneticData();
        }

        static string RLDecoding(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var result = new StringBuilder();
            int i = 0;
            while (i < s.Length)
            {
                if (char.IsDigit(s[i]) && i + 1 < s.Length)
                {
                    int count = s[i] - '0';
                    result.Append(s[i + 1], count);
                    i += 2;
                }
                else
                {
                    result.Append(s[i]);
                    i++;
                }
            }
            return result.ToString();
        }

        static string RLEncoding(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var result = new StringBuilder();
            int count = 1;
            for (int i = 1; i <= s.Length; i++)
            {
                if (i < s.Length && s[i] == s[i - 1])
                {
                    count++;
                }
                else
                {
                    if (count > 2) result.Append(count).Append(s[i - 1]);
                    else result.Append(s[i - 1], count);
                    count = 1;
                }
            }
            return result.ToString();
        }
    }
}