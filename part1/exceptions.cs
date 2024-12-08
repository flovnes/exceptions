class Lab {
    static void Main() {
        List<int> file_values  = new();
        List<string> no_files  = new();
        List<string> bad_files = new();
        List<string> overflows = new();

        for (int i = 10; i < 30; ++i) {
            string path = $"input/{i}.txt";
            try {
                var lines = File.ReadAllLines(path);
                try { file_values.Add(checked(int.Parse(lines[0].Trim()) * int.Parse(lines[1].Trim()))); }
                catch { overflows.Add(path[6..]); }
            }
            catch ( FileNotFoundException ) { no_files.Add(path[6..]); }
            catch { bad_files.Add(path[6..]); }
        }

        try {
            File.WriteAllLines("no_file.txt", no_files);
            File.WriteAllLines("bad_data.txt", bad_files);
            File.WriteAllLines("overflow.txt", overflows);  
            Console.WriteLine(file_values.Any() ? $"answer: {file_values.Average()}" : "no valid products");
        }
        catch { Console.WriteLine("! output blocked"); }
    }
}