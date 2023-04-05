using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    public GameObject girl;
    public void Turn()
    {
        girl.GetComponent<GirlAnim>().turn = true;
    }
}
