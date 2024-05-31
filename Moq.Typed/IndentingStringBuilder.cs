using System.Buffers;
using System.Text;

namespace Moq.Typed;


internal enum EmptyStringAppendBahaviour
{
    Ignore,
    AppendIndentationOnly
}

internal sealed class IndentingStringBuilder
{
    private readonly StringBuilder builder;
    private readonly EmptyStringAppendBahaviour onEmptyString;

    public int IndentationLevel { get; private set; }

    private static void AtLeastZero(int indentationLevel)
    {
        if (indentationLevel < 0)
            throw new ArgumentException($"{nameof(indentationLevel)} lesser than 0 ({indentationLevel}).");
    }

    public IndentingStringBuilder(int indentationLevel = 0, EmptyStringAppendBahaviour onEmptyString = EmptyStringAppendBahaviour.Ignore)
    {
        AtLeastZero(indentationLevel);
        builder = new();
        IndentationLevel = indentationLevel;
        this.onEmptyString = onEmptyString;
    }

    public override string ToString()
        => builder.ToString();

    public IndentingStringBuilder AppendIgnoringIndentation(string value)
    {
        builder.Append(value);
        return this;
    }

    public IndentingStringBuilder AppendLine()
    {
        builder.AppendLine();
        return this;
    }

    public IndentingStringBuilder AppendLine(string value, int atIndentation = 0)
    {
        if (value.Length is 0 && onEmptyString is EmptyStringAppendBahaviour.Ignore)
            return this;
        builder.AppendLine();
        return Append(value, atIndentation);
    }

    public IndentingStringBuilder Append(string value, int atIndentation = 0)
    {
        if (value.Length is 0 && onEmptyString is EmptyStringAppendBahaviour.Ignore)
            return this;

        using var indentation = Indent(atIndentation);
        const string newline = @"
";

        IEnumerable<int> GetNewlineIndices(int startingFrom)
        {
            if (value.Length <= startingFrom - 1)
                yield break;

            var next = value.IndexOf(newline, startingFrom);
            if (next != -1)
            {
                yield return next;
            }
            else
                yield break;

            var rest = GetNewlineIndices(next + newline.Length);
            foreach (var next_ in rest)
                yield return next_;
        }

        if (IndentationLevel is 0)
        {
            builder.Append(value);
            return this;
        }

        var newlineIndices = GetNewlineIndices(0);
        void AppendIndentation()
        {
            for (int i = 0; i < IndentationLevel; i++)
                builder.Append("    ");
        }

        var previousNewline = 0 - newline.Length;
        var buffer = ArrayPool<char>.Shared.Rent(256);
        void AppendLine(int nextNewline)
        {
            var textStartAt = previousNewline + newline.Length;
            var textLength = nextNewline - textStartAt;
            if (textLength is not 0)
                AppendIndentation();
            var slice = value.AsSpan(textStartAt, textLength);
            slice.CopyTo(buffer);
            builder.Append(buffer, 0, textLength);
            previousNewline = nextNewline;
        }
        try
        {
            foreach (var index in newlineIndices)
            {
                AppendLine(index);
                builder.AppendLine();
            }
            if (previousNewline != value.Length - 1)
                AppendLine(value.Length);
        }
        finally
        {
            ArrayPool<char>.Shared.Return(buffer);
        }

        return this;
    }

    public Indentiation Indent(int times = 1)
    {
        AtLeastZero(times);
        IndentationLevel += times;
        return new(this, times);
    }

    public struct Indentiation : IDisposable
    {
        private readonly IndentingStringBuilder builder;
        private int times;

        public Indentiation(IndentingStringBuilder builder, int times)
        {
            this.builder = builder;
            this.times = times;
        }

        public void Dispose()
        {
            builder.IndentationLevel -= times;
            times = 0;
        }
    }
}
