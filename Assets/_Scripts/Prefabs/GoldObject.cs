using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GoldObject : MonoBehaviour
{
    public static Action OnGoldCollected;
   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("Player"))
       {
           OnGoldCollected?.Invoke();
           gameObject.SetActive(false);           
           Invoke(nameof(ActivateGoldObject), 1f);
       }
   }
    private void Start() {
        gameObject.SetActive(true);
    }
    private void ActivateGoldObject() {
        gameObject.SetActive(true);
    }
}
