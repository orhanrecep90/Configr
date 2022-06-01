// See https://aka.ms/new-console-template for more information
using Configt.ConfigurationReaderLibrary;


ConfigurationReader _configurationReader = new ConfigurationReader("SERVICE-A", "", 1000 * 60*60);
while (true)
{
    Thread.Sleep(2000);
    var data = await _configurationReader.GetValue<string>("SiteName");
    Console.WriteLine(data);
}
Console.ReadLine();