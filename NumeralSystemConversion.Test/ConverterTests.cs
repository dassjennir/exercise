namespace NumeralSystemConversion.Test
{
    public class ConverterTests
    {
        [InlineData(31, "1F")]
        [InlineData(0, "0")]
        [InlineData(75, "4B")]
        [InlineData(14654, "393E")]
        [InlineData(16777215, "FFFFFF")]
        [Theory]
        public void Convert_ArgumentsInRangeWOptionals_ConvertedReturned(ulong decimalNumber, string expected)
        {
            string stringMap = "0123456789ABCDEF";
            int maxPositions = 6;
            var result = Converter.ConvertFromDecimal(decimalNumber, maxPositions, stringMap);

            Assert.Equal(expected, result);
        }

        [InlineData(16777216)]
        [InlineData(19777216)]
        [Theory]
        public void Convert_ArgumentOutOfRangeWOptionals_ThrowArgumentOutOfRange(ulong decimalNumber)
        {
            string stringMap = "0123456789ABCDEF";
            int maxPositions = 6;
            Assert.Throws<ArgumentOutOfRangeException>(() => Converter.ConvertFromDecimal(decimalNumber, maxPositions, stringMap));
        }

        [InlineData(0, "a")]
        [InlineData(75, "bx")]
        [InlineData(19770609663, "ZZZZZZ")]
        [Theory]
        public void Convert_ArgumentsInRangeWithDefaults_ConvertedReturned(ulong decimalNumber, string expected)
        {
            var result = Converter.ConvertFromDecimal(decimalNumber);
            Assert.Equal(expected, result);
        }

        [InlineData(19770609664)]
        [InlineData(29770609664)]
        [Theory]
        public void Convert_ArgumentOutOfRangeWDefaults_ThrowArgumentOutOfRange(ulong decimalNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Converter.ConvertFromDecimal(decimalNumber));
        }
    }
}