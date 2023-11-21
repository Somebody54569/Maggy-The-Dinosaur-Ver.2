using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableControl.boneCount += 1;
            SoundManager.instance.Play(SoundManager.SoundName.CollectItem);
            Destroy(this.gameObject);
        }
    }
}
