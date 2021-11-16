using System;

namespace MultiValueDictionary.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            InputDriver driver = new InputDriver();
            bool appRunning  = true;

            while(appRunning)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                appRunning = driver.HandleInput(input);
            }
        }
    }
}
