using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrain : MonoBehaviour
{
    private int _speed = 5;
    private bool _isMove;
     
     private void Start() {
          _isMove = false;
     }
   private void Update() {
    
     if(_isMove)
     transform.position += Vector3.back *(_speed * Time.deltaTime);         
        
   }

   private void OnTriggerEnter(Collider other) {
     
          if(other.CompareTag("Player")){

               _isMove = true;
          }
   }
}
