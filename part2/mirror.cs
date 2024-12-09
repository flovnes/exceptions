using System.Text.RegularExpressions;
class Mirror {
    static void Main() {
        Console.WriteLine("Enter folder name (e.g. src/textures/grass):");
        string directoryPath = Console.ReadLine();
        directoryPath = directoryPath.Trim().Length==0 ? Directory.GetCurrentDirectory() : directoryPath;
        string[] files = null;
        try {
            files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + directoryPath);
        } 
        catch { Console.WriteLine("! bad path"); }

        foreach (string fileName in files) {
            try {
                using Bitmap image = new(fileName);
                image.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                string fileNameNoExt;
                int i;
                fileNameNoExt = (i=fileName.LastIndexOf('.')) == -1 ? fileName : fileName[..i];
                string newName = Path.Combine(directoryPath, $"{fileNameNoExt}-mirrored.gif");
                image.Save(newName, System.Drawing.Imaging.ImageFormat.Gif);
            } catch {
                Regex regexExtForImage = new(@"^\.(bmp)|(gif)|(tiff?)|(jpe?g)|(png)$", RegexOptions.IgnoreCase);
                if (regexExtForImage.IsMatch(fileName)) {
                    MessageBox.Show($"{fileName}\n is not an image!!!!", "caption", buttons: MessageBoxButtons.YesNoCancel);
                }
            }
        }
    }
}