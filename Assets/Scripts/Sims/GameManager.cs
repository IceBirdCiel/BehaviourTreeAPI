using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Dictionary<string, List<UsableObject>> usableObjects = new Dictionary<string, List<UsableObject>>();

    private void Awake()
    {
        UsableObject[] usableObjectLists = (UsableObject[])GameObject.FindObjectsOfType<UsableObject>();

        List<UsableObject> list;

        foreach (UsableObject usableObject in usableObjectLists)
        {
            if (usableObjects.ContainsKey(usableObject.name))
            {
                usableObjects[usableObject.name].Add(usableObject);
            }
            else
            {
                list = new List<UsableObject>();
                list.Add(usableObject);
                usableObjects.Add(usableObject.name,  list);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
