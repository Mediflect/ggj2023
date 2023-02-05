using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.hasLaser = true;
            GlobalAudio.PlayPickup();
            transform.parent.gameObject.SetActive(false);
        }
    }
}
