using System.Collections.Generic;

namespace ClusterUpgrade2
{
    public class DisruptionBudget
    {
        public int disruptionAllowed;
        public string appName;
    };

    public class Application
    {
        public string nodeName;
        public string appName;
    };

    public class Group
    {
        public HashSet<string> nodeList;
        public int totalAppCnt;
        public Dictionary<string, int> appCnt;
        public Group()
        {
            totalAppCnt = 0;
            nodeList = new HashSet<string>();
            appCnt = new Dictionary<string, int>();
        }
    };
}
