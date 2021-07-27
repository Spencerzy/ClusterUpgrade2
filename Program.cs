using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ClusterUpgrade2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var nodes = new List<string> { "node1", "node2", "node3" };
            //var apps = new List<Application> {
            //    new Application()
            //    {
            //        appName = "app1",
            //        nodeName = "node1",
            //    },
            //    new Application()
            //    {
            //        appName = "app1",
            //        nodeName = "node2",
            //    } ,
            //    new Application()
            //    {
            //        appName = "app2",
            //        nodeName = "node1",
            //    } ,
            //    new Application()
            //    {
            //        appName = "app2",
            //        nodeName = "node2",
            //    } ,
            //    new Application()
            //    {
            //        appName = "app3",
            //        nodeName = "node2",
            //    },
            //    new Application()
            //    {
            //        appName = "app3",
            //        nodeName = "node3",
            //    } };
            //var budget = new Dictionary<string, int>()
            //{
            //    {"app1", 1},
            //    {"app2", 1 },
            //    {"app3", 1 }
            //};
            Solution s = new Solution();

            var (nodes, apps, budgets) = GenereateTest(5000, 4000);
            var fileContent = new FileContent() { nodes = nodes, apps = apps, budgets = budgets };

            string fileTime = DateTime.Now.ToString("yyyy-MM-ddTHHmmssZ");
            using (var file = new System.IO.StreamWriter($"C:\\Users\\yuazho.FAREAST\\source\\repos\\ClusterUpgrade2\\Testcases\\TestCase{fileTime}.json"))
            {
                file.WriteLine(JsonConvert.SerializeObject(fileContent));
            };

            var ans = s.GetSolution(nodes, apps, budgets);
            using (var file = new System.IO.StreamWriter($"C:\\Users\\yuazho.FAREAST\\source\\repos\\ClusterUpgrade2\\Testcases\\TestCase{fileTime}_output.json"))
            {
                file.WriteLine(JsonConvert.SerializeObject(ans));
            };

            //var nodes = new List<string> { "node1", "node2", "node3","node4", "node5"};
            //var apps = new List<Application> {
            //    new Application()
            //    {
            //        appName = "app1",
            //        nodeName = "node2",
            //    },
            //    new Application()
            //    {
            //        appName = "app1",
            //        nodeName = "node3",
            //    } ,
            //                    new Application()
            //    {
            //        appName = "app1",
            //        nodeName = "node5",
            //    } ,
            //    new Application()
            //    {
            //        appName = "app2",
            //        nodeName = "node1",
            //    } ,
            //    new Application()
            //    {
            //        appName = "app2",
            //        nodeName = "node2",
            //    } ,
            //                    new Application()
            //    {
            //        appName = "app2",
            //        nodeName = "node3",
            //    } ,
            //                                    new Application()
            //    {
            //        appName = "app2",
            //        nodeName = "node4",
            //    } ,
            //                                                    new Application()
            //    {
            //        appName = "app2",
            //        nodeName = "node5",
            //    } ,
            //    new Application()
            //    {
            //        appName = "app3",
            //        nodeName = "node5",
            //    },
            //    new Application()
            //    {
            //        appName = "app4",
            //        nodeName = "node5",
            //    } ,
            //    new Application()
            //    {
            //        appName = "app4",
            //        nodeName = "node1",
            //    },
            //    new Application()
            //    {
            //        appName = "app4",
            //        nodeName = "node2",
            //    },
            //    new Application()
            //    {
            //        appName = "app5",
            //        nodeName = "node1",
            //    },
            //    new Application()
            //    {
            //        appName = "app5",
            //        nodeName = "node2",
            //    },                new Application()
            //    {
            //        appName = "app5",
            //        nodeName = "node3",
            //    },                new Application()
            //    {
            //        appName = "app5",
            //        nodeName = "node4",
            //    },                new Application()
            //    {
            //        appName = "app5",
            //        nodeName = "node5",
            //    },};
            //var budget = new Dictionary<string, int>()
            //{
            //    {"app1", 2},
            //    {"app2", 4 },
            //    {"app3", 1 },
            //    {"app4", 3 },
            //    {"app5", 5 },
            //};
            //var ans = s.GetSolution(nodes, apps, budget);
            //var i = 2;
        }


        public static (List<string>, List<Application>, Dictionary<string, int>) GenereateTest(int numberOfNodes, int numberOfApplications)
        {
            List<string> nodes = new List<string>();
            for (int i = 1; i <= numberOfNodes; i++) nodes.Add($"node{i}");

            Random rnd = new Random();
            List<Application> apps = new List<Application>();
            Dictionary<string, int> budegt = new Dictionary<string, int>();
            for (int i = 1; i <= numberOfApplications; i++)
            {
                var appName = $"app{i}";
                int numberOfReplica = rnd.Next(1, Math.Min(200, numberOfNodes) + 1);
                var choosedNodes = nodes.OrderBy(_ => rnd.Next(nodes.Count)).Take(numberOfReplica);
                foreach (var node in choosedNodes)
                {
                    apps.Add(new Application() { appName = appName, nodeName = node });
                }

                budegt[appName] = rnd.Next(1, numberOfReplica+1);
            }

            return (nodes, apps, budegt);
        }
    }
}
