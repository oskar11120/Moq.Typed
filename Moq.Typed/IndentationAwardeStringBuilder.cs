using System.Buffers;
using System.Text;

namespace Moq.Typed;

internal sealed class IndentationAwardeStringBuilder
{
    private readonly StringBuilder builder;
    public int IndentationLevel { get; private set; }

    private static void AtLeastZero(int indentationLevel)
    {
        if (indentationLevel < 0)
            throw new ArgumentException($"{nameof(indentationLevel)} lesser than 0 ({indentationLevel}).");
    }

    public IndentationAwardeStringBuilder(StringBuilder builder, int indentationLevel)
    {
        AtLeastZero(indentationLevel);
        this.builder = builder;
        IndentationLevel = indentationLevel;
    }

    public StringBuilder Append(string value)
    {
        IEnumerable<int> GetNewlineIndices(int startingFrom)
        {
            const string newline = @"
";

            if (value.Length >= startingFrom - 1)
                yield break;

            var next = value.IndexOf(newline, startingFrom);
            if (next != -1)
            {
                yield return next;
            }

            var rest = GetNewlineIndices(next + 1);
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

        var previousIndex = 0;
        var buffer = ArrayPool<char>.Shared.Rent(2 ^ 8);
        try
        {
            foreach (var index in newlineIndices)
            {
                if (index is 0)
                    continue;

                AppendIndentation();
                value.AsSpan(previousIndex, index).CopyTo(buffer);
                builder.Append(buffer, previousIndex, index);
            }
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
        private readonly IndentationAwardeStringBuilder builder;
        private readonly int times;

        public Indentiation(IndentationAwardeStringBuilder builder, int times)
        {
            this.builder = builder;
            this.times = times;
        }

        public void Dispose() => builder.IndentationLevel -= times;
    }
}
