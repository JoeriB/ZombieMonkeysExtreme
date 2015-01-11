using UnityEngine;
using UnityEngine.UI;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Inventory: This will contains our items 
 */
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryUI;
    [SerializeField]
    private GameObject[] slots;

    private int counter;

    public void Initiate()
    {
        slots = GameObject.FindGameObjectsWithTag(TagManager.slot);
        UpdateGameText();
    }

    void UpdateGameText()
    {
        inventoryUI.GetComponentInChildren<Text>().text = "Inventory Items: " + counter + "/" + slots.Length + " Completed";
        if (hasAllItems())
            inventoryUI.GetComponentInChildren<Text>().text += Environment.NewLine + "Run towards the Safe house to win!";
    }

    public void addItem(PickUpItem.Item item)
    {
        //Get current empty image.
        Image image = slots[counter++].GetComponent<Image>();
        //Add visibility to our new sprite.
        image.color = new Color(255, 255, 255, 255);
        //Add the sprite of the item.
        image.sprite = item.sprite;
        //Update the game text with our new item.
        UpdateGameText();
    }

    public bool hasAllItems()
    {
        return counter >= slots.Length;
    }

    public int getItemCount()
    {
        return counter;
    }

    public GameObject[] GetSlots()
    {
        return slots;
    }
}
