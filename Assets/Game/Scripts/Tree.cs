using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Tree : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private int _remainingResources = 9;
        private int _startResources;

        public event Action<Tree> OnDeactivated;

        private void Start()
        {
            _startResources = _remainingResources;
        }

        public bool HasResources()
        {
            return _remainingResources > 0;
        }

        [Button]
        public bool TakeResource()
        {
            if (_remainingResources <= 0)
            {
                return false;
            }

            _remainingResources--;

            if (_remainingResources <= 0)
            {
                gameObject.SetActive(false);
                OnDeactivated?.Invoke(this);
            }
            else
            {
                _animator.Play("Chop", -1, 0);
            }

            return true;
        }

        public void RestoreTree()
        {
            _remainingResources = _startResources;
            gameObject.SetActive(true);
        }
    }
}
