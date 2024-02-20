using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static Action OnObstacleHit;
    
    [SerializeField] private PlayerMovement Player;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            Player.SetIsGrounded();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnObstacleHit?.Invoke();
        }
    }
}
