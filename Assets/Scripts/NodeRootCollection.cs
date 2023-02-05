using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRootCollection : MonoBehaviour
{
    public LaserDestroyable node;
    public List<Root> connectedRoots;

    private void Awake()
    {
        node.DestroyedByLaser += OnNodeDestroyed;
    }

    private void OnNodeDestroyed()
    {
        foreach (Root root in connectedRoots)
        {
            root.Shrivel();
        }
    }

    [ContextMenu("Gather Roots")]
    private void ContextGatherRoots()
    {
        connectedRoots.Clear();
        for(int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            Root root = child.GetComponent<Root>();
            if (root != null)
            {
                connectedRoots.Add(root);
            }
        }
    }
}
