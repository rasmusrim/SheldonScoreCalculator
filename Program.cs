// See https://aka.ms/new-console-template for more information

using SheldonNumber;

Console.Write("Initializing calculator...");
var calculator = new SheldonScoreCalculator();
Console.WriteLine("Done");

var numberWithHighestSheldonScore = 0;
var highestSheldonScoreAsInt = 0;
SheldonNumberScore? highestSheldonScoreResult = null;

for (var i = 1; i < 1_000_000; i++)
{
    var score = calculator.GetSheldonNumberScore(i);
    var asInt = score.ToInt();

    if (asInt > highestSheldonScoreAsInt)
    {
        numberWithHighestSheldonScore = i;
        highestSheldonScoreAsInt = asInt;
        highestSheldonScoreResult = score;
    }

    if (i % 100_000 == 0)
    {
        Console.WriteLine(i);
    }
}

Console.WriteLine(
    "Number " + numberWithHighestSheldonScore + " has Sheldon score: " + highestSheldonScoreAsInt
);
Console.WriteLine(highestSheldonScoreResult.ToString());


