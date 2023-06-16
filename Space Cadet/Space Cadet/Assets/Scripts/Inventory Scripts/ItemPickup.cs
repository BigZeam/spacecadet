using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            InventoryManager.Instance.Add(item);
            InventoryManager.Instance.ListItems();
            AudioManager.Instance.Play(pickupSound);
            Destroy(gameObject);
        }
    }

}
