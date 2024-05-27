using SheldonNumber;

class SheldonScoreCalculator
{

    public List<int> PrimeNumbers { get; set; }
    private int PrimeNumbersCalculatedTo { get; set; }


    public SheldonScoreCalculator()
    {
        PrimeNumbers = PrimeNumbersCache.GetPrimeNumbers();
        PrimeNumbersHashSet = PrimeNumbers.ToHashSet();
        PrimeNumbersCalculatedTo = PrimeNumbers.Max();

    }

    public HashSet<int> PrimeNumbersHashSet { get; set; }

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
            primeNumberPosition = PrimeNumbers.FindIndex(primeNumber => primeNumber == number) + 1;
            response.PrimeNumberPosition = primeNumberPosition;
        }

        if (response.ReverseIsPrime)
        {
            primeNumberPositionOfReversedNumber =
                PrimeNumbers.FindIndex(primeNumber => primeNumber == reversedNumber) + 1;
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
        if (number > PrimeNumbersCalculatedTo)
        {
            AddPrimeNumbersUpTo(number);
        }


        return PrimeNumbersHashSet.Contains(number);
    }
    

    public void AddPrimeNumbersUpTo(int newMax)
    {
        if (newMax <= PrimeNumbersCalculatedTo)
        {
            return;
        }

        for (var i = PrimeNumbersCalculatedTo; i <= newMax; i++)
        {

           
            if (IsPrimeNumberByCalculation(i))
            {
                PrimeNumbers.Add(i);
            }

        }

        PrimeNumbersCalculatedTo = newMax;
        PrimeNumbersHashSet = PrimeNumbers.ToHashSet();
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

    public string PrimeNumbersToCode()
    {
        var retString = "return new List<int> {";
        PrimeNumbers.ForEach(number => { retString += number + ",\n"; });

        retString += "};";

        return retString;
    }
}
