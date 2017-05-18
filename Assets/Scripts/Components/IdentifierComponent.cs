using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

/// <summary>Used to help identify the particular object of which this component is attached to.</summary>
public class IdentifierComponent : MonoBehaviour {

    /// <summary>
    /// Custom name aside from <see cref="Object.name"/> attached to this particular game object.
    /// </summary>
    public string customName;

    /// <summary>
    /// Mainly used to identify whom of which this object as Instantiated by.
    /// </summary>
    public string ownerTag;

}
