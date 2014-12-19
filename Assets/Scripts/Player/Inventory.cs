using UnityEngine;
using UnityEngine.UI;
using System;

/**
 * @Author: Joeri Boons
 * @ZombieMonkeysExtreme Inventory: This will contains our items 
 */
public class Inventory : MonoBehaviour
{

    public GameObject inventoryUI;
    public GameObject[] slots;

    int counter;

    public void Initiate()
    {
        slots = GameObject.FindGameObjectsWithTag("Slot");
        Debug.Log(slots.Length);
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
}
