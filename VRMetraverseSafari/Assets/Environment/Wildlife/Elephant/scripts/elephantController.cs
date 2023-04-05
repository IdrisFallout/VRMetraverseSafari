using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elephantController : MonoBehaviour
{
    private Animator animator;
    private int velocityHash;
    // Start is called before the first frame update
    void Start()
    {
        velocityHash = Animator.StringToHash("Velocity");
        animator = GetComponent<Animator>();
        animator.SetFloat(velocityHash, 1.28f);
    }
}
