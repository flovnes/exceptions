using System.Text.RegularExpressions;
class Mirror {
    static void Main() {
        Console.WriteLine("enter dir path:");
        string? directoryPath = Console.ReadLine();
        directoryPath = directoryPath==null || directoryPath.Trim().Length==0 ? Directory.GetCurrentDirectory() : directoryPath;
        string[] files = Directory.GetFiles(directoryPath);

        foreach (string fileName in files) {
            try {
                using Bitmap image = new(fileName);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                string fileNameNoExt;
                int i;
                fileNameNoExt = (i=fileName.LastIndexOf('.')) == -1 ? fileName : fileName[..i];
                string newName = Path.Combine(directoryPath, $"{fileNameNoExt}-mirrored.gif");

                image.Save(newName, System.Drawing.Imaging.ImageFormat.Gif);
            }
            catch (Exception ex) {
                Regex regexExtForImage = new("^((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);
                if (regexExtForImage.IsMatch(fileName)) {
                    MessageBox.Show($"{fileName} is not an image: {ex.Message}", "try again");
                }
            }
        }
    }
}