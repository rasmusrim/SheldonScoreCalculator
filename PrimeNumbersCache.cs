namespace SheldonNumber;

public class PrimeNumbersCache
{
    public static List<int> GetPrimeNumbers()
    {

        var filename = Path.Combine(Directory.GetCurrentDirectory(), "PrimeNumbers.txt");

        return File.ReadAllLines(filename).Select(n => int.Parse(n)).ToList();
    }
}
