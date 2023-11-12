using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject sectionDestroyer;

    // Update is called once per frame

    private void Start()
    {
        sectionDestroyer = GameObject.Find("SectionDestroyer");
    }

    void Update()
    {
        if (transform.position.z < sectionDestroyer.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
    
}
