namespace PaymentSimplify.Common.Strings;

public static class Extensions
{
    public static bool ValueOnlyRepeat(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        var firstLetter = value[0];
        var countRepeat = 1;

        for (var i = 1; i < value.Length; i++)
        {
            if (value[i] == firstLetter)
                countRepeat++;
        }

        return countRepeat == value.Length;
    }

    public static Guid ToGuid(this string value)
    {
        return new Guid(value);
    }
}