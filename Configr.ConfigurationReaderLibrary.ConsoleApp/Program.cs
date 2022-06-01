using Configt.ConfigurationReaderLibrary;


ConfigurationReader _configurationReader = new ConfigurationReader("SERVICE-A", "", 10 * 60 * 1000);
while (true)
{
    Thread.Sleep(2000);
    var data = await _configurationReader.GetValue<string>("SiteName");
    Console.WriteLine(data);
}
Console.ReadLine();