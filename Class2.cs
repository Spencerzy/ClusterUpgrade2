using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterUpgrade2
{
    class DisruptionBudget
    {
        public int disruptionAllowed;
        public string appName;
    };

    class Application
    {
        public string nodeName;
        public string appName;
    };

    class Group
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

    //bool operator ==(const Group& s1, const Group& s2)
    //{
    //    return s1.totalAppCnt == s2.totalAppCnt && s1.nodeList == s2.nodeList;
    //}
}
