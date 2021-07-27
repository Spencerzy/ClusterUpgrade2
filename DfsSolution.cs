using System.Collections.Generic;
using System.Linq;

namespace ClusterUpgrade2
{
	class DfsSolution
	{
		private int ans;

		private bool IfCanMergeGroups(Group g1, Group g2, Dictionary<string, int> budgets)
		{
			var cnt1 = g1.appCnt;
			var cnt2 = g2.appCnt;
			foreach (var kvp in cnt1)
			{
				string app = kvp.Key;
				int sum = kvp.Value + (!cnt2.ContainsKey(app) ? 0 : cnt2[app]);
				if (sum > budgets[app]) return false;
			}
			return true;
		}

		private SearchState Merge(SearchState rawState, Group group1, Group group2)
		{
			// Merge two groups
			Group newGroup = new Group();
			newGroup.nodeList = group1.nodeList.Union(group2.nodeList).ToHashSet();
			newGroup.totalAppCnt = group1.totalAppCnt + group2.totalAppCnt;
			newGroup.appCnt = new Dictionary<string, int>(group1.appCnt);
			foreach (var kvp in group2.appCnt)
			{
				string app = kvp.Key;
				if (!newGroup.appCnt.ContainsKey(app))
				{
					newGroup.appCnt[app] = 0;
				}
				newGroup.appCnt[app] += kvp.Value;
			}

			// Get new state
			SearchState ret = new SearchState();
			ret.fixedGroups = new HashSet<Group>(rawState.fixedGroups);
			ret.notFixedGroups.Add(newGroup);
			foreach (var group in rawState.notFixedGroups)
			{
				if (group == group1 || group == group2) continue;
				ret.notFixedGroups.Add(group);
			}

			return ret;
		}

		public List<List<string>> GetSolution(List<string> nodes, List<Application> apps, Dictionary<string, int> budgets)
		{
			Queue<SearchState> q = new Queue<SearchState>();
			HashSet<SearchState> visited = new HashSet<SearchState>();

			ans = nodes.Count();
			SearchState ansState = new SearchState(nodes, apps);
			visited.Add(ansState);
			q.Enqueue(ansState);
			while (q.Count() > 0)
			{
				SearchState state = q.Dequeue();

				if (state.notFixedGroups.Count == 0) continue;

				bool canMerge = false;
				Group group1 = state.notFixedGroups[0];
				int newStateCnt = 0;
				for (int i = 1; i < state.notFixedGroups.Count(); ++i)
				{
					var group2 = state.notFixedGroups[i];
					if (IfCanMergeGroups(group1, group2, budgets))
					{
						canMerge = true;
						SearchState newState = Merge(state, group1, group2);

						if (visited.Contains(newState))
                        {
							continue;
                        }

						visited.Add(newState);
						q.Enqueue(newState);

						if (newState.fixedGroups.Count() + newState.notFixedGroups.Count() < ans)
						{
							ans = newState.fixedGroups.Count() + newState.notFixedGroups.Count();
							ansState = newState;
						}

						newStateCnt++;
						if (newStateCnt == 100) break;
					}
				}

				if (!canMerge)
				{
					state.fixedGroups.Add(group1);
					state.notFixedGroups.RemoveAt(0);
					q.Enqueue(state);
				}
			}

			var ret = new List<List<string>>();
			ret.AddRange(ansState.fixedGroups.Select(_ => _.nodeList.ToList()));
			ret.AddRange(ansState.notFixedGroups.Select(_ => _.nodeList.ToList()));
			return ret;
		}
	};
}
