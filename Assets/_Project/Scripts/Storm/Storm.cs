using Cysharp.Threading.Tasks;
using DBGA.AI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace DBGA.AI.Storm
{
	public class Storm : MonoBehaviour, IStorm
	{
		public List<Damageable> Agents { get => agents; }
		public int DamageAmmount { get => damageAmmount; }

		// SerializeFields are used only for debug purposes
		[SerializeField]
		private int damageAmmount;

		[SerializeField]
		private float radius;

		[SerializeField]
		private VisualStorm visualStorm;

		[SerializeField]
		private float amountToReduce;

		[SerializeField, Tooltip("Value in milliSeconds")]
		private int timeToReduce;

		[SerializeField, Tooltip("Value in milliSeconds")]
		private int timeForDamage;

		[SerializeField]
		private List<Damageable> agents = new List<Damageable>();

		private bool isStormActive = false;

		private Vector3 center;

		private CancellationToken cancellationToken;

		private CancellationTokenSource source;

		[SerializeField]
		Vector3 CenterDebug;
		private void Start()
		{
			agents = FindAllDamageablesObjects();
			SetCenter(CenterDebug);
			visualStorm.SetVisualRadious(radius);
		}

        private List<Damageable> FindAllDamageablesObjects()
        {
            return GameObject.FindObjectsOfType<Damageable>().ToList<Damageable>();
        }

		public Vector3 GetCenter()
		{
			return center;
		}

        private void SetCenter(Vector3 newCenter)
        {
            center = newCenter;
            visualStorm.SetVisualCenter(newCenter);
        }
        public int GetDamageAmout()
        {
            return damageAmmount;
        }

		public float GetRadius()
		{
			return radius;
		}

		public void StartStorm()
		{
			isStormActive = true;
			cancellationToken = new CancellationToken();

			source = new CancellationTokenSource();

			cancellationToken = source.Token;

			AsyncStartStorm();
			AsyncStartDamage();
		}

		public void StopStorm()
		{
			isStormActive = false;
			source.Cancel();
		}
		private async UniTask DamageTimer(int timeForDamage, CancellationToken cancellationToken)
		{

			while (true)
			{

				try
				{
					if (cancellationToken.IsCancellationRequested)
						break;

					await UniTask.Delay(timeForDamage, cancellationToken: cancellationToken);
					AppyDamage();

				}
				catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
				{

				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					throw;
				}
			}
		}

		private void AppyDamage()
		{
			if (agents.Count == 0)
				return;

            List<Damageable> toRemove = new List<Damageable>();
            foreach (Damageable agent in agents)
            {
                try
                {
                    Vector3 temp = agent.transform.position;
                }
                catch (MissingReferenceException)
                {
                    toRemove.Add(agent);
                }
            }

            agents.RemoveAll(a => toRemove.Contains(a));

            foreach (Damageable agent in agents)
            {
                float distanceFromCenter = (agent.transform.position - GetCenter()).magnitude;

				if (distanceFromCenter < GetRadius())
					continue;

                agent.TakeDamage(GetDamageAmout());
            }
        }

        private async UniTask StormTimer(int timeToReduced, CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    await UniTask.Delay(timeToReduced, cancellationToken: cancellationToken);
                    ModifyStorm();
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {

				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					throw;
				}
			}
		}
		private void ModifyStorm()
		{
			radius -= amountToReduce;
			visualStorm.SetVisualRadious(radius);
		}

		private async void AsyncStartStorm()
		{
			await StormTimer(timeToReduce, cancellationToken);
		}

		private async void AsyncStartDamage()
		{
			await DamageTimer(timeForDamage, cancellationToken);

		}
	}
}

