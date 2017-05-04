using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameObjectIdentifier {

    public List<string> tagNames;
    //public string[] componentNames;
    
    public bool AddTagName(string tag)
    {
        bool isAdded = false;

        if (!tagNames.Contains(tag))
        {
            tagNames.Add(tag);
            isAdded = true;
        }

        return isAdded;
    }

    public void RemoveTagName(string tag)
    {
        tagNames.Remove(tag);
    }

    public bool HasTagName(string targetTag)
    {
        return tagNames.Contains(targetTag);
    }
    
    //public bool hasComponentName(Component targetComponent)
    //{
    //    string[] splitted = targetComponent.ToString().Split('.');
        
    //    if (Array.IndexOf(componentNames, splitted[splitted.Length - 1]) > -1)
    //         return true;

    //    return false;
    //}

}
