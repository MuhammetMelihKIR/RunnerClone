using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    private float _speed ;

    private void Start() {
        _speed = 5f;
    }
    private void Update() {

        transform.position += Vector3.back *( _speed * Time.deltaTime);
    }
}
