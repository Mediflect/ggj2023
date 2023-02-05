using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    public event System.Action PowerAqcuired;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.hasLaser = true;
            GlobalAudio.PlayPickup();
            transform.parent.gameObject.SetActive(false);
            PowerAqcuired?.Invoke();
        }
    }
}
