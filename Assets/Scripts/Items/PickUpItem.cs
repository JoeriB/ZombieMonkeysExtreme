using UnityEngine;
using System.Collections;
using System;

public class PickUpItem : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public Sprite sprite;
        public String description;
    }

    public GameObject player;
    public Item item;

    bool canPickUpItem = false;
    Inventory inventory;
    // Use this for initialization
    void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUpItem && Input.GetKeyDown("e"))
        {
            inventory.addItem(item);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if (collider.name.Equals("Player"))
        {
            canPickUpItem = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Leaving" + collider.name);
        if (collider.name.Equals("Player"))
        {
            canPickUpItem = true;
        }
    }
}
