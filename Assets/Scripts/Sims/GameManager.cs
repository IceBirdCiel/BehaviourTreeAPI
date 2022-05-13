using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Dictionary<string, List<UsableObject>> usableObjects = new Dictionary<string, List<UsableObject>>();

    void Start()
    {
        UsableObject[] usableObjectLists = (UsableObject[])GameObject.FindObjectsOfType<UsableObject>();

        List<UsableObject> list;

        foreach (UsableObject usableObject in usableObjectLists)
        {
            if (usableObjects.ContainsKey(usableObject.objectName))
            {
                usableObjects[usableObject.objectName].Add(usableObject);
            }
            else
            {
                list = new List<UsableObject>();
                list.Add(usableObject);
                usableObjects.Add(usableObject.objectName, list);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
