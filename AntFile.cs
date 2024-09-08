using System.Text;
using System.Text.RegularExpressions;

public class AntFile {
    public List<TextBlock> Blocks { get; private set; }

    public AntFile(string filepath)
    {
        Blocks = new List<TextBlock>();
        loadFile(filepath);
    }

    private void loadFile(string filepath) {
        TextBlock? currentBlock = null;
        StringBuilder builder = new StringBuilder();
        string[] lines = File.ReadAllLines(filepath);

        foreach (var line in lines) {
            if (String.IsNullOrWhiteSpace(line)) continue;
            if (line[0] == '#') {
                if (currentBlock != null) {
                    currentBlock.Text = builder.ToString();
                    Blocks.Add(currentBlock);
                }
                currentBlock = new TextBlock();
                builder = new StringBuilder();
                var match = Regex.Match(line, @"P\d+(\s*-\s*[a-zA-Z0-9]+)?");
                currentBlock.Name = match.Groups[0].Value;
                continue;
            }
            else {
                builder.Append(line);
            }
        }

        if (currentBlock != null) {
            currentBlock.Text = builder.ToString();
            Blocks.Add(currentBlock);
        }
    }
}