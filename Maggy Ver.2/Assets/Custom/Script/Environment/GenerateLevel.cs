using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public int zPos = 120;
    public bool creatingSection = false;
    public int secNum;
    public Transform playerTransform;
    void Update()
    {
        float playerZPosition = playerTransform.position.z;
        if (playerZPosition >= zPos - 200)
        {
            if (!creatingSection)
            {
                creatingSection = true;
                StartCoroutine(GenerateSection());
            }
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, section.Length);
        Instantiate(section[secNum], new Vector3(-15, 0, zPos), Quaternion.identity);
        zPos += 120;
        creatingSection = false;
        yield return null;
    }
}
