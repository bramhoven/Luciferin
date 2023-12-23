namespace Luciferin.Core.Abstractions;

using Enums;

public interface ISetting
{
    bool HasValue { get; }

    string Key { get; }

    string Category { get; }

    ValueTypeEnum ValueType { get; }
}