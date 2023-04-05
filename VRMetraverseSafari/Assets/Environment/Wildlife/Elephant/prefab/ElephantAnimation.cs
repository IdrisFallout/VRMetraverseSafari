using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ElephantAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _catchUp = false;
    private int _velocityHash;

    private float _velocity;
    

    private int _walkingHash ;
    private int _idleHash ;
    private int _idleEatHash ;
    private int _idleLookAroundHash ;


    public int nextState;
    private UnityAction _nextMethod;
    private bool _newAnim;
    
    private int[] _states ;
    private UnityAction[] _methods ;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _velocityHash = Animator.StringToHash("Velocity");
        
        SetVelocity(true);
        
        _animator = GetComponent<Animator>();
        
        _idleHash = Animator.StringToHash("IsIdleBreath");
        _walkingHash = Animator.StringToHash("IsWalking");
        _idleEatHash = Animator.StringToHash("IsIdleEat");
        _idleLookAroundHash = Animator.StringToHash("IsIdleLookAround");
        
        _states = new[] { _idleHash, _walkingHash, _idleEatHash, _idleLookAroundHash};
        _methods = new UnityAction[] { Idle, Walk, IdleEat, IdleLookAround};

        _newAnim = true;
        nextState = _idleHash;
        _animator.SetBool(nextState, true);
        _nextMethod = Idle;
    }
    void Update()
    {
        _animator.SetBool(nextState, true);

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f) _newAnim = true;
        
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f  && _newAnim)
        {
            _nextMethod();
            _newAnim = false;
        }
    }
   
    private void SetNextStateAndMethod(int[] ps)
    {
        int randomNumber = Random.Range(0, 10);
        int index = ps[randomNumber];
        nextState = _states[index];
        _nextMethod = _methods[index];
    }
    
    private void Idle()
    {
        _animator.SetBool(_idleHash, false);
        int[] ps = new[] {0 ,0, 1, 2, 1, 3, 0, 2, 1, 0};
        
        SetNextStateAndMethod(ps);
    }
    private void Walk()
    {
        _animator.SetBool(_walkingHash, false);
        int[] ps = new[] {0 ,0, 1, 2, 2, 3, 2, 2, 1, 0};
        
        SetNextStateAndMethod(ps);
    }
    private void IdleEat()
    {
        _animator.SetBool(_idleEatHash, false);
        int[] ps = new[] {0 ,0, 1, 2, 1, 3, 0, 2, 1, 0};
        
        SetNextStateAndMethod(ps);
    }
    private void IdleLookAround()
    {
        _animator.SetBool(_idleLookAroundHash, false);
        int[] ps = new[] {0 ,0, 1, 2, 0, 3, 0, 2, 1, 0};
        
        SetNextStateAndMethod(ps);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject && _animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            _animator.StopPlayback();
            
            _nextMethod();
            _animator.Play("IdleBreath");
        
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ElephantPack"))
        {
            SetVelocity(false);
            
        }
    }

    void SetVelocity(bool inPack)
    {
        _velocity = (inPack)? Random.Range(0.5f, 1.00f): Random.Range(1.70f, 2.00f);
        _animator.SetFloat(_velocityHash, _velocity);

    }
}