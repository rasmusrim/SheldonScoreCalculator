namespace SheldonNumber;

public class SheldonNumberScore
{
    public bool IsPrime { get; set; }
    public bool ReverseIsPrime { get; set; }
    public bool PrimeNumberPositionIsReverseOfReversedPrimeNumberPosition { get; set; }
    public bool BinaryIsPalindrome { get; set; }
    public bool BinaryOfReversedNumberIsPalindrome { get; set; }
    public bool ProductOfDigitsEqualPrimeNumberPosition { get; set; }
    public bool ProductOfDigitsInReverseEqualPrimeNumberPositionOfReverseNumber { get; set; }
    public int? PrimeNumberPosition { get; set; }

    public int ToInt()
    {
        var score = 0;
        GetType()
            .GetProperties()
            .ToList()
            .ForEach(property =>
            {
                var value = property.GetValue(this);

                if (value is bool and true)
                {
                    score++;
                }
            });

        return score;
    }

    public string ToString()
    {
        var response = "";
        GetType()
            .GetProperties()
            .ToList()
            .ForEach(property =>
            {
                response += property.Name + ": " + property.GetValue(this) + "\n";
            });

        return response;
    }
}
