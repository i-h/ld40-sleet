using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour {
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = transform.position;
        }
    }
}
