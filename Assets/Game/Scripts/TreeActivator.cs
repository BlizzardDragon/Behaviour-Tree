using System.Collections;
using Unity.Collections;
using UnityEngine;
using Tree = Sample.Tree;

public class TreeActivator : MonoBehaviour
{
    [SerializeField] private float _recoveryTime = 60f;
    [SerializeField, ReadOnly] private Tree[] _trees;


    private void OnEnable()
    {
        _trees = FindObjectsOfType<Tree>();

        foreach (var tree in _trees)
        {
            tree.OnDeactivated += InvokeActivate;
        }
    }

    private void OnDisable()
    {
        foreach (var tree in _trees)
        {
            tree.OnDeactivated -= InvokeActivate;
        }
    }

    public void InvokeActivate(Tree tree) => StartCoroutine(TreeRestoration(tree));

    private IEnumerator TreeRestoration(Tree tree)
    {
        yield return new WaitForSeconds(_recoveryTime);
        tree.RestoreTree();
    }
}