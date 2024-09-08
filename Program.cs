// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

string removeSpaces(string str) {
    string s = Regex.Replace(str, @"\s{2,}", " ");
    s = s.Replace('\n', ' ');
    return s;
}

void print(string filepath, string text) {
    Console.WriteLine(text);
}

async Task runBlock(TextBlock block) {
    string text = removeSpaces(block.Text);
    string fname = String.Format("./{0}.mp3", block.Name);
    Console.WriteLine(fname);
    TextReader reader = new TextReader(text);
    List<Task> tasks = new List<Task>();
    StringBuilder sBuild = new StringBuilder();

    sBuild.Append("Read this entire block of text in a monotone, sleepy, droning way. ");
    while (reader != null && !reader.IsEnd) {
        sBuild.Append(reader.GetNextBlock());
        await ElevenLabs.TextToSpeech(fname, sBuild.ToString());

        sBuild.Clear();
        sBuild.Append("Read this entire block of text in a monotone, sleepy, droning way. ");
    }
}

AntFile file = new AntFile("file.txt");
List<Task> tasks = new List<Task>();
foreach(var block in file.Blocks) {
    tasks.Add(runBlock(block));
}
Task.WaitAll(tasks.ToArray());
