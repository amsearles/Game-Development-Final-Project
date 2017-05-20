using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MapBoundary : MonoBehaviour {

    public static Bounds MAP_BOUNDS;
    public static Vector3 MAP_SIZE;

	// Use this for initialization
	void Start () {
        BoxCollider box = GetComponent<BoxCollider>();
        MAP_BOUNDS = box.bounds;
        MAP_SIZE = box.size;
	}
	
}
