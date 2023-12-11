using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 5.0f;

        [ShowInInspector, ReadOnly]
        private bool _moveRequired;

        [ShowInInspector, ReadOnly]
        private Vector3 _moveDirection;

        [Space]
        [SerializeField]
        private int _resourceCapacity = 5;

        [ShowInInspector, ReadOnly]
        private int _resourceAmount;

        [Space]
        [SerializeField]
        private Animator _animator;

        private Tree _choppingTree;

        [Button]
        public void Move(Vector3 direction)
        {
            _moveRequired = true;
            _moveDirection = direction;
        }

        [Button]
        public void Chop(Tree tree)
        {
            if (IsResourceBagFull())
            {
                return;
            }

            _choppingTree = tree;
            _animator.Play("Chop", -1, 0);
        }

        //Called by animator
        [UsedImplicitly]
        private void OnChopAnim()
        {
            if (_choppingTree.TakeResource())
            {
                _resourceAmount++;
            }
        }

        public bool IsResourceBagFull()
        {
            return _resourceAmount >= _resourceCapacity;
        }

        [Button]
        public int UnloadResources()
        {
            var unloadResources = _resourceAmount;
            _resourceAmount = 0;
            return unloadResources;
        }

        private void Update()
        {
            if (_moveRequired)
            {
                _animator.SetBool("IsMoving", _moveRequired);
                
                transform.position += _moveSpeed * Time.deltaTime * _moveDirection;
                transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
                _moveRequired = false;
            }
            else
            {
                _animator.SetBool("IsMoving", _moveRequired);
            }
        }
    }
}