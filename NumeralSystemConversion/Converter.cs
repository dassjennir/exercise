namespace NumeralSystemConversion
{
    public static class Converter
    {
        public static char[] CharMap { get; set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public static int MaxPositions { get; set; } = 6;
        public static string ConvertFromDecimal(ulong decimalNumber, int maxPositions = 0, string stringMap = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            char[] map;
            if (maxPositions == 0) maxPositions = MaxPositions;

            if (stringMap == null) map = CharMap;
            else map = stringMap.ToCharArray();

            if (decimalNumber > ulong.MaxValue || decimalNumber > Math.Pow(map.Length, maxPositions) - 1)
            {
                throw new ArgumentOutOfRangeException($"decimalNumber must be a positive integer lesser than {Math.Pow(map.Length, maxPositions)}.");
            }

            char[] result = new char[maxPositions];
            int i = result.Length - 1;

            if (decimalNumber == 0) return map[0].ToString();
            while (decimalNumber > 0)
            {
                ulong mod = decimalNumber % (uint)map.Length;
                result[i] = map[mod];
                i--;
                decimalNumber = decimalNumber / (uint)map.Length;
            }
            var resultFiltered = result.Where(c => c != '\0').ToArray();
            return new string(resultFiltered);
        }
    }
}