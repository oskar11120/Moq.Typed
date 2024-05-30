using System.Buffers;
using System.Text;

namespace Moq.Typed;

internal sealed class IndentingStringBuilder
{
    private readonly StringBuilder builder;
    public int IndentationLevel { get; private set; }

    private static void AtLeastZero(int indentationLevel)
    {
        if (indentationLevel < 0)
            throw new ArgumentException($"{nameof(indentationLevel)} lesser than 0 ({indentationLevel}).");
    }

    public IndentingStringBuilder(int indentationLevel = 0)
    {
        AtLeastZero(indentationLevel);
        builder = new();
        IndentationLevel = indentationLevel;
    }

    public override string ToString()
        => builder.ToString();

    public StringBuilder AppendIgnoringIndentation(string value)
        => builder.Append(value);

    public StringBuilder Append(string value)
    {
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
            return builder;
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
            if(textLength is not 0)
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

        return builder;
    }

    public Indentiation Indent(int times = 1)
    {
        AtLeastZero(times);
        IndentationLevel += times;
        return new(this, times);
    }

    public readonly struct Indentiation : IDisposable
    {
        private readonly IndentingStringBuilder builder;
        private readonly int times;

        public Indentiation(IndentingStringBuilder builder, int times)
        {
            this.builder = builder;
            this.times = times;
        }

        public void Dispose() => builder.IndentationLevel -= times;
    }
}
