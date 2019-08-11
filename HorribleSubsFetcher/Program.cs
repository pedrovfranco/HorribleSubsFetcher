using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    class Program
    {
        private static Program _singleton = null;

        static void Main(string[] args)
        {
            Fetcher fetcher = new Fetcher();

            Task.Run(async () => await fetcher.Run()).GetAwaiter().GetResult();
        }
    }
}
