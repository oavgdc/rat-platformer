using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private string requiredInventoryItemName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if (NewPlayer.Instance.inventory.ContainsKey(requiredInventoryItemName))
            {
                NewPlayer.Instance.RemoveInventoryItem(requiredInventoryItemName);
                Destroy(gameObject);
            }
        }
    }
}
