class Lab {
    static void Main() {
        int[] file_values = new int[20];
        string[] no_files = new string[20];
        string[] bad_files = new string[20];
        string[] overflows = new string[20];

        int nofile_index = 0, bad_index = 0, overflow_index = 0, good_index = 0;

        for (int i = 10; i < 30; ++i) {
            string str = $"data/{i}.txt";
            try {
                string[] lines = File.ReadAllLines(str);
                try {
                    int num1 = int.Parse(lines[0].Trim());
                    int num2 = int.Parse(lines[1].Trim());
                    int result = checked(num1 * num2);
                    file_values[good_index++] = result;
                } catch (OverflowException) {
                    overflows[overflow_index++] = str[5..];
                } catch {
                    bad_files[bad_index++] = str;
                }
            } catch (FileNotFoundException) {
                no_files[nofile_index++] = str[5..];
            } catch {
                bad_files[bad_index++] = str[5..];
            }
        }

        try {
            File.WriteAllLines("output/no_file.txt", no_files.Take(nofile_index));
            File.WriteAllLines("output/bad_data.txt", bad_files.Take(bad_index));
            File.WriteAllLines("output/overflow.txt", overflows.Take(overflow_index));
            try {
                double average = file_values.Take(good_index).Average();
                Console.WriteLine($"answer: {average}");
            } catch {
                Console.WriteLine("no valid products");
            }
        } catch {
            Console.WriteLine("! output blocked");
        }
    }
}
