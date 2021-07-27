using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterUpgrade2
{
    class FileContent
    {
        public List<string> nodes;

        public List<Application> apps;

        public Dictionary<string, int> budgets;
    }
}
