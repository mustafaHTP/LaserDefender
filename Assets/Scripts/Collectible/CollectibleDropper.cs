using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleDropper : MonoBehaviour
{
    [Header("Collectibles")]
    [Space(2)]
    [SerializeField] private List<GameObject> collectibles;
    [Tooltip("Speed that when loot drops")]
    [SerializeField] private float lootSpeed;

    private int minDropChange = 0;
    private int maxDropChange = 100;

    private GameObject GenerateDrop()
    {
        int dropValue = Random.Range(minDropChange, maxDropChange);
        List<GameObject> lootItems = new();

        for (int i = 0; i < collectibles.Count; i++)
        {
            if (dropValue <= collectibles[i].GetComponent<Collectible>().dropChange)
            {
                lootItems.Add(collectibles[i]);
            }
        }

        //Get only one item if there is
        if (lootItems.Count > 0)
        {
            GameObject droppedItem = lootItems[Random.Range(0, lootItems.Count)];
            return droppedItem;
        }
        else
        {
            return null;
        }
    }

    public void InstantiateLoot()
    {
        GameObject droppedItem = GenerateDrop();

        //If there is no item 
        if (droppedItem == null)
            return;

        GameObject cloneLoot = Instantiate(
            droppedItem,
            transform.position,
            Quaternion.identity);

        if (cloneLoot.TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = new Vector2(0f, -lootSpeed);
        }

        if(cloneLoot.TryGetComponent(out Collectible collectible))
        {
            Destroy(cloneLoot, collectible.destroyTime);
        }
    }
}
