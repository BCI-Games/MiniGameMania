using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Submissions.StudioSomething;

public class StationBehaviour : MonoBehaviour
{
    public float InteractionTime = 2f;

    public Item Item;
    private IEnumerator startCo = null;

    public virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            Begin(player);
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Debug.Log("Leave");
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            Cancel(player);
        }
    }

    public virtual void Begin(PlayerController player)
    {
        if (startCo != null) return;
        Debug.Log("Begin");
        player.ProcessBar.gameObject.SetActive(true);
        player.ProcessBar.SetMaxProcess(InteractionTime);
        startCo = StartCo(player);
        StartCoroutine(startCo);
    }

    public virtual void Cancel(PlayerController player)
    {
        Debug.Log("Cancelled");
        StopCoroutine(startCo);
        startCo = null;
        player.ProcessBar.gameObject.SetActive(false);
    }

    public virtual void Action(PlayerController player)
    {
        Item craftableItem = null;
        if (player.HasItem)
        {
            foreach (Item.Recipe recipe in player.HeldItem.recipes)
            {
                if (Item.name.Equals(recipe.Items[0].name))
                {
                    craftableItem = recipe.Items[1];
                }
            }

            if (craftableItem != null)
            {
                player.InventoryManager.Remove(player.HeldItem);
                Destroy(player.HeldItemObject);

                player.InventoryManager.Add(craftableItem);
                player.HasItem = true;
                GameObject newItem = GameObject.Instantiate(craftableItem.prefab, player.ItemHolder.transform.position, Quaternion.identity, player.ItemHolder.transform);
                player.HeldItem = craftableItem;
                player.HeldItemObject = newItem;
                player.FoodSprite.sprite = craftableItem.icon;
            }
            else
            {
                Debug.Log("Fizzle");
            }
        }
        else
        {

            player.InventoryManager.Add(Item);
            player.HasItem = true;
            GameObject newItem = GameObject.Instantiate(Item.prefab, player.ItemHolder.transform.position, Quaternion.identity, player.ItemHolder.transform);
            player.HeldItem = Item;
            player.HeldItemObject = newItem;
            player.FoodSprite.sprite = Item.icon;
        }
    }

    public virtual IEnumerator StartCo(PlayerController player)
    {
        float i = 0;
        while (i < InteractionTime)
        {
            player.ProcessBar.SetProcess(i);
            i += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Action(player);
    }
}
