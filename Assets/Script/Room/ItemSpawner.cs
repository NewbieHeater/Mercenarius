using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private int rand;
    private bool spawned = false;
    public List<GameObject> Items;
    void Start()
    {
        Invoke("Spawn", 0.1f);
    }
    private void Spawn()
    {
        if (spawned == false)
        {
            rand = Random.Range(0, Items.Count);
            Instantiate(Items[rand], transform.position, Items[rand].transform.rotation);
            spawned = true;
        }
    }
}
