namespace RestService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				CreateWebHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: {0}", ex.Message);
				Console.ReadKey();
			}
		}

		public static IHostBuilder CreateWebHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args).
				ConfigureWebHostDefaults(webBuilder =>
					webBuilder.UseStartup<Startup>());
	}
}
