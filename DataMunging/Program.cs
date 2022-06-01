// PART ONE: Weather

using System.Text.RegularExpressions;

var weather = File.ReadAllText("Data/weather.dat");
var weatherReader = new StringReader(weather);
var daysToTempSpreads = new Dictionary<string, int>();
var dayWithMinTempSpread = "";

while (weatherReader.Peek() != -1)
{
    var currentLine = weatherReader.ReadLine();
    try
    {
        var day = currentLine.Substring(0, 4).Trim();

        if (int.TryParse(Regex.Match(currentLine[4..9], @"\d+").Value, out int maxTemp)
            && int.TryParse(Regex.Match(currentLine[9..15], @"\d+").Value, out int minTemp))
        {
            if (!daysToTempSpreads.Any())
            {
                dayWithMinTempSpread = day;
            }

            var tempSpread = maxTemp - minTemp;
            daysToTempSpreads.Add(day, tempSpread);
            daysToTempSpreads.TryGetValue(dayWithMinTempSpread, out int MinTempSpread);

            if (tempSpread < MinTempSpread)
            {
                dayWithMinTempSpread = day;
            }
        }
    }
    catch (Exception)
    {
        continue;
    }
}

// PART TWO: Football

var football = File.ReadAllText("Data/football.dat");
var footballReader = new StringReader(football);
var teamToForAgainstSpreads = new Dictionary<string, int>();
var teamWithMinForAgainstSpread = "";

while (footballReader.Peek() != -1)
{
    var currentLine = footballReader.ReadLine();
    try
    {
        var team = currentLine.Substring(7, 16).Trim();

        if (int.TryParse(Regex.Match(currentLine[43..46], @"\d+").Value, out int f)
            && int.TryParse(Regex.Match(currentLine[50..56], @"\d+").Value, out int a))
        {
            if (!teamToForAgainstSpreads.Any())
            {
                teamWithMinForAgainstSpread = team;
            }

            var spread = Math.Abs(f - a);
            teamToForAgainstSpreads.Add(team, spread);
            teamToForAgainstSpreads.TryGetValue(teamWithMinForAgainstSpread, out int MinScoreSpread);

            if (spread < MinScoreSpread)
            {
                teamWithMinForAgainstSpread = team;
            }
        }
    }
    catch (Exception)
    {
        continue;
    }
}

// PART 3: DRY Fusion

var fileName = "football.dat";
var fileContents = File.ReadAllText($"Data/{fileName}");
var fileContentsReader = new StringReader(fileContents);
var fieldToSpreads = new Dictionary<string, int>();
var fieldWithMinSpread = "";
int fieldStart = 7, fieldEnd = 16,
    minStart = 43, minEnd = 46,
    maxStart = 50, maxEnd = 56;

while (fileContentsReader.Peek() != -1)
{
    var currentLine = fileContentsReader.ReadLine();
    try
    {
        var fieldValue = currentLine.Substring(fieldStart, fieldEnd).Trim();

        if (int.TryParse(Regex.Match(currentLine[minStart..minEnd], @"\d+").Value, out int max)
            && int.TryParse(Regex.Match(currentLine[maxStart..maxEnd], @"\d+").Value, out int min))
        {
            if (!fieldToSpreads.Any())
            {
                fieldWithMinSpread = fieldValue;
            }

            var spread = Math.Abs(max - min);
            fieldToSpreads.Add(fieldValue, spread);
            fieldToSpreads.TryGetValue(fieldWithMinSpread, out int minSpread);

            if (spread < minSpread)
            {
                fieldWithMinSpread = fieldValue;
            }
        }
    }
    catch (Exception)
    {
        continue;
    }
}

Console.WriteLine("The day with the lowest temperature spread is: {0}", dayWithMinTempSpread);
Console.WriteLine("The team with the lowest for - against spread is: {0}", teamWithMinForAgainstSpread);
Console.WriteLine("PART 3: The item with the lowest for - against spread is: {0}", fieldWithMinSpread);