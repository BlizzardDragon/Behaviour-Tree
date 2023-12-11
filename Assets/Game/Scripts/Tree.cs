using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Tree : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private int remainingResources = 10;
        private int startResources;

        public event Action<Tree> OnDeactivated;

        private void Start()
        {
            this.startResources = this.remainingResources;
        }

        public bool HasResources()
        {
            return this.remainingResources > 0;
        }

        [Button]
        public bool TakeResource()
        {
            if (this.remainingResources <= 0)
            {
                return false;
            }

            this.remainingResources--;

            if (this.remainingResources <= 0)
            {
                this.gameObject.SetActive(false);
                OnDeactivated?.Invoke(this);
            }
            else
            {
                this.animator.Play("Chop", -1, 0);
            }

            return true;
        }

        public void RestoreTree()
        {
            this.remainingResources = this.startResources;
            this.gameObject.SetActive(true);
        }
    }
}
