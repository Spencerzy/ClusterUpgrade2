using System.Collections.Generic;

namespace ClusterUpgrade2
{
    class FileContent
    {
        public List<string> nodes;

        public List<Application> apps;

        public Dictionary<string, int> budgets;
    }
}
