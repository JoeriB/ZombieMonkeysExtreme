using UnityEngine;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Pick Up item: Contains an Item class with information about a certain item, such as Sprite and Description.
 * This class will add an item to the players inventory.
 */
public class PickUpItem : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public Sprite sprite;
        public String description;
    }

    public Item item;

    private GameObject player;
    bool canPickUpItem = false;
    Inventory inventory;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.player);
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
        if (collider.tag.Equals(TagManager.player))
        {
            canPickUpItem = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals(TagManager.player))
            canPickUpItem = true;
    }
    private Quaternion LookAtTarget(Vector3 target)
    {
        var newRotation = Quaternion.LookRotation(target);
        newRotation.x = 0;
        newRotation.z = 0;
        return Quaternion.Slerp(transform.rotation, newRotation, 10 * Time.deltaTime);
    }
}
