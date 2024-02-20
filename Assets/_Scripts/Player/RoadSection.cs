using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadSection : MonoBehaviour
{
    public GameObject[] roads;
    private int currentIndex = 0;
    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("RoadTrigger")) {
            roads[currentIndex].transform.position = new Vector3(0,0, transform.position.z + 60);
            currentIndex = (currentIndex + 1) % roads.Length;
            roads[currentIndex].SetActive(true);
        }
    }
}
