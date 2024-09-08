using System.Text.RegularExpressions;

public class TextReader {
    int cursor = 0;
    const int MaxLen = 400;
    private string text;
    public bool IsEnd { get; private set; } = false;
    public TextReader(string text) {
        this.text = text;
    }

    private int GetNextIndex() {
        if (text.Length - cursor > MaxLen) {
            return cursor + MaxLen;
        }
        else {
            return text.Length - 1;
        }
    }

    private bool isStopChar(char c) {
        return Regex.IsMatch(c.ToString(), @"[.!?:]");
    }

    private bool isSpaceChar(char c) {
        return Regex.IsMatch(c.ToString(), @"[\s]");
    }

    private int ReverseFindStopIndex(int start, int end) {
        if (start > text.Length - 1) {
            return text.Length - 1;
        }
        else {
            for (int n = start; n > end; --n) {
                if (isStopChar(text[n])) {
                    return n;
                }
            }
            for (int n = start; n > end; --n) {
                if (isSpaceChar(text[n])) {
                    return n;
                }
            }
        }

        return start;
    }

    private string CleanString(string str) {
        string oStr = Regex.Replace(str, @"^\s+|\s+$", "");
        return oStr;
    }

    public string? GetNextBlock() {
        int nextIndex = GetNextIndex();
        nextIndex = ReverseFindStopIndex(nextIndex, cursor);
        string output = text.Substring(cursor, nextIndex - cursor + 1);
        output = CleanString(output);
        cursor = nextIndex + 1;
        if (cursor >= text.Length) {
            IsEnd = true;
        }
        return output;
    }
}