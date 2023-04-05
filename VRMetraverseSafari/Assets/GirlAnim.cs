using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAnim : MonoBehaviour
{
    private Animator _animator;

    private int _isTurnHash;
    public bool turn = false;

    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _isTurnHash = Animator.StringToHash("isTurning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isTurning = _animator.GetBool(_isTurnHash);
        
        if (!isTurning && turn)
        {
            _animator.SetBool(_isTurnHash, true);
            turn = false;
        }
        else if (isTurning && !turn)
        {
            _animator.SetBool(_isTurnHash, false);

        }
    }
}
