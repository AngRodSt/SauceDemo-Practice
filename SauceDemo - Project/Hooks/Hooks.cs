using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine("Starting scenario execution...");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("Scenario execution completed.");
        }
    }
}
