using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 5.0f;

        [ShowInInspector, ReadOnly]
        private bool moveRequired;

        [ShowInInspector, ReadOnly]
        private Vector3 moveDirection;

        [Space]
        [SerializeField]
        private int resourceCapacity;

        [ShowInInspector, ReadOnly]
        private int resourceAmount;

        [Space]
        [SerializeField]
        private Animator animator;

        private Tree choppingTree;

        [Button]
        public void Move(Vector3 direction)
        {
            this.moveRequired = true;
            this.moveDirection = direction;
        }

        [Button]
        public void Chop(Tree tree)
        {
            if (this.IsResourceBagFull())
            {
                return;
            }

            this.choppingTree = tree;
            this.animator.Play("Chop", -1, 0);
        }

        //Called by animator
        [UsedImplicitly]
        private void OnChopAnim()
        {
            if (this.choppingTree.TakeResource())
            {
                this.resourceAmount++;
            }
        }

        public bool IsResourceBagFull()
        {
            return this.resourceAmount >= this.resourceCapacity;
        }

        [Button]
        public int UnloadResources()
        {
            var unloadResources = this.resourceAmount;
            this.resourceAmount = 0;
            return unloadResources;
        }

        private void Update()
        {
            if (this.moveRequired)
            {
                this.animator.SetBool("IsMoving", this.moveRequired);
                
                this.transform.position += this.moveSpeed * Time.deltaTime * this.moveDirection;
                this.transform.rotation = Quaternion.LookRotation(this.moveDirection, Vector3.up);
                this.moveRequired = false;
            }
            else
            {
                this.animator.SetBool("IsMoving", this.moveRequired);
            }
        }
    }
}