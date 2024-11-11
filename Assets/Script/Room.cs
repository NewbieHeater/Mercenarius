using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject templates;
    // Start is called before the first frame update
    void Awake()
    {

        GameObject prefab = Instantiate(templates, transform.position, transform.rotation);
        prefab.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
