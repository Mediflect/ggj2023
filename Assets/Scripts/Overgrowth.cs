using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overgrowth : MonoBehaviour
{
    public event System.Action Destroyed;

    public List<LaserDestroyable> nodes;

    private void Awake()
    {
        if (nodes.Count < 1)
        {
            Debug.LogWarning("Overgrowth has no attached nodes");
        }
        
        foreach (LaserDestroyable node in nodes)
        {
            node.DestroyedByLaser += OnNodeDestroyed;
        }
    }

    private void OnNodeDestroyed()
    {
        foreach (LaserDestroyable node in nodes)
        {
            if (!node.hasBeenDestroyed)
            {
                // a node still lives, so exit
                return;
            }
        }

        // otherwise, all nodes have been destroyed, so destroy this
        gameObject.SetActive(false);
        Destroyed?.Invoke();
    }
}
