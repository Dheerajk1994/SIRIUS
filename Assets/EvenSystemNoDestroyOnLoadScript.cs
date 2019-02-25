using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenSystemNoDestroyOnLoadScript : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
