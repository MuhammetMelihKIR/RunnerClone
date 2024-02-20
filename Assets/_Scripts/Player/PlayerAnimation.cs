using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string RUN = "Run";
    private const string JUMP = "Jump";
    private const string ROLL = "Roll";
    
    private Animator _animator;       

    private void Start()
    {
        var animator = transform.GetChild(0);
        _animator = animator.GetComponent<Animator>();        
    }
    
    public void Run()
    {
        _animator.SetTrigger(RUN);        
    }
    
    public void Jump()
    {
        _animator.SetTrigger(JUMP);
    }
    
    public void Roll()
    {
        _animator.SetTrigger(ROLL);
    }
    
}
