// See https://aka.ms/new-console-template for more information

using SheldonNumber;

var calculator = new SheldonNumberCalculator();

var numberWithHighestSheldonScore = 0;
var highestSheldonScoreAsInt = 0;
SheldonNumberScore? highestSheldonScoreResult = null;

for (var i = 10; i < 100_000_000; i++)
{
    var score = calculator.GetSheldonNumberScore(i);
    var asInt = score.ToInt();

    if (asInt > highestSheldonScoreAsInt)
    {
        numberWithHighestSheldonScore = i;
        highestSheldonScoreAsInt = asInt;
        highestSheldonScoreResult = score;
    }
}

Console.WriteLine(
    "Number " + numberWithHighestSheldonScore + " has Sheldon score: " + highestSheldonScoreAsInt
);
Console.WriteLine(highestSheldonScoreResult.ToString());

class SheldonNumberCalculator
{
    public List<int> primeNumbers { get; set; } = new();
    private int primeNumbersCalculatedTo { get; set; } = 0;

    public SheldonNumberScore GetSheldonNumberScore(int number)
    {
        var response = new SheldonNumberScore();

        response.IsPrime = IsPrime(number);

        var reversedNumber = ReverseInt(number);

        response.ReverseIsPrime = IsPrime(reversedNumber);

        int? primeNumberPosition = null;
        int? primeNumberPositionOfReversedNumber = null;

        if (response.IsPrime)
        {
            primeNumberPosition = primeNumbers.FindIndex(primeNumber => primeNumber == number) + 1;
            response.PrimeNumberPosition = primeNumberPosition;
        }

        if (response.ReverseIsPrime)
        {
            primeNumberPositionOfReversedNumber =
                primeNumbers.FindIndex(primeNumber => primeNumber == reversedNumber) + 1;
        }

        if (primeNumberPosition != null && primeNumberPositionOfReversedNumber != null)
        {
            response.PrimeNumberPositionIsReverseOfReversedPrimeNumberPosition =
                primeNumberPosition == ReverseInt((int)primeNumberPositionOfReversedNumber);
        }

        if (primeNumberPosition != null)
        {
            response.ProductOfDigitsEqualPrimeNumberPosition = ProductOfDigitsEquals(
                number,
                (int)primeNumberPosition
            );
        }

        if (primeNumberPositionOfReversedNumber != null)
        {
            response.ProductOfDigitsInReverseEqualPrimeNumberPositionOfReverseNumber =
                ProductOfDigitsEquals(reversedNumber, (int)primeNumberPositionOfReversedNumber);
        }

        response.BinaryIsPalindrome = BinaryRepresentationIsPalindrome(number);
        response.BinaryOfReversedNumberIsPalindrome = BinaryRepresentationIsPalindrome(
            reversedNumber
        );

        return response;
    }

    private bool ProductOfDigitsEquals(int number, int shouldEqual)
    {
        var product = 1;

        foreach (var digit in number.ToString())
        {
            product *= int.Parse(digit.ToString());
        }

        return product == shouldEqual;
    }

    private bool BinaryRepresentationIsPalindrome(int number)
    {
        var binary = Convert.ToString(number, 2);
        var reversedBinary = new string(binary.Reverse().ToArray());

        return binary == reversedBinary;
    }

    bool IsPrime(int number)
    {
        if (number > primeNumbersCalculatedTo)
        {
            AddPrimeNumbersUpTo(number);
        }

        return primeNumbers.Contains(number);
    }

    public void AddPrimeNumbersUpTo(int newMax)
    {
        if (newMax <= primeNumbersCalculatedTo)
        {
            return;
        }

        for (var i = primeNumbersCalculatedTo; i <= newMax; i++)
        {
            if (IsPrimeNumberByCalculation(i) && !primeNumbers.Contains(i))
            {
                primeNumbers.Add(i);
            }
        }

        primeNumbersCalculatedTo = newMax;
    }

    bool IsPrimeNumberByCalculation(int number)
    {
        if (number <= 1)
            return false;
        if (number == 2)
            return true;
        if (number % 2 == 0)
            return false;

        int boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }

    public static int ReverseInt(int number)
    {
        char[] numberArray = number.ToString().ToCharArray();
        Array.Reverse(numberArray);

        return int.Parse(new string(numberArray));
    }
}
