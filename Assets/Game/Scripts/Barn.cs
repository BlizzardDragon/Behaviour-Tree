using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Barn : MonoBehaviour
    {
        [SerializeField]
        private int _resourceCapacity = 50;

        [SerializeField]
        private float _clearPeriod = 35f;

        [ShowInInspector, ReadOnly]
        private int _resourceAmount;

        private Coroutine _coroutine;

        public void AddResources(int range)
        {
            if (CanAddResources(range))
            {
                _resourceAmount += range;
            }

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(ClearResources());
            }
        }


        public bool CanAddResources(int range)
        {
            return _resourceAmount + range <= _resourceCapacity;
        }

        public bool IsFull()
        {
            return _resourceAmount >= _resourceCapacity;
        }

        [Button]
        public void Clear()
        {
            _resourceAmount = 0;
        }

        private IEnumerator ClearResources()
        {
            yield return new WaitForSeconds(_clearPeriod);
            Clear();
            _coroutine = null;
        }
    }
}