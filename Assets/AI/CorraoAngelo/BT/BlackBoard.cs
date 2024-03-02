using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class BlackBoard
    {
        private Dictionary<string, object> blackboard = new Dictionary<string, object>();

		public void SetValueToDictionary<T>(string key, T value) {
			if (!blackboard.ContainsKey(key)) {
				blackboard.Add(key, value);
				return;
			}

			blackboard[key] = value;
		}

		public bool TryGetValueFromDictionary<T>(string key, out T result) {
			bool tempResult = blackboard.TryGetValue(key, out object temp);
			if (tempResult)
				result = (T)temp;
			else
				result = default(T);

			return tempResult;
		}
	}
}
