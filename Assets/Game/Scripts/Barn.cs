using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Barn : MonoBehaviour
    {
        [SerializeField]
        private int resourceCapacity = 50;

        [SerializeField]
        private float clearPeriod = 30f;

        [ShowInInspector, ReadOnly]
        private int resourceAmount;

        private Coroutine _coroutine;

        public void AddResources(int range)
        {
            if (this.CanAddResources(range))
            {
                this.resourceAmount += range;
            }

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(ClearResources());
            }
        }


        public bool CanAddResources(int range)
        {
            return this.resourceAmount + range <= this.resourceCapacity;
        }

        public bool IsFull()
        {
            return this.resourceAmount >= this.resourceCapacity;
        }

        [Button]
        public void Clear()
        {
            this.resourceAmount = 0;
        }

        private IEnumerator ClearResources()
        {
            yield return new WaitForSeconds(clearPeriod);
            Clear();
            _coroutine = null;
        }
    }
}