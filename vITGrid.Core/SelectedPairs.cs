using System.Collections.Generic;

namespace vITGrid.Core
{
    public class SelectedPairs
    {
        public SelectedPairs()
        {
            StartCurrencies = new List<string>();
            EndCurrencies = new List<string>();
        }

        public List<string> StartCurrencies { get; set; }
        public List<string> EndCurrencies { get; set; }
    }
}