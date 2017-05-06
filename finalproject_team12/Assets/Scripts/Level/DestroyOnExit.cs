using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour {
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Registered");
        Destroy(other.gameObject);
    }
}
