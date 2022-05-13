using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObject : MonoBehaviour
{
    // Start is called before the first frame update

    public string objectName;
    public bool isInUse;
    public float timeToUse;

    public void use()
    {
        isInUse = true;
    }

    public void reset()
    {
        isInUse=false;
    }

}
