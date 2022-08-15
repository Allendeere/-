using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Update : MonoBehaviour
{
    public List<GameObject> Slot = new List<GameObject>();
    [SerializeField] GameObject Grid;

    private void Start()
    {
        Refrash();
    }

    public void Refrash()
    {
        for (int i = 0; i < Slot.Count; i++)
        {
            Instantiate(Slot[i], transform.position, transform.rotation);
            Slot[i].transform.SetParent(Grid.transform);
        }
    }
}
