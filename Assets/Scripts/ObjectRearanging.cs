using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRearanging : MonoBehaviour
{
    List<MessObject> objects_list = new List<MessObject>();
    
    // Update is called once per frame
    void Update()
    {
        for (int index = 0; index < objects_list.Count; index++)
        {
            if(!objects_list[index].properly_set)
            {
                objects_list[index].messed_prefab.SetActive(true);
                objects_list[index].initial_prefab.SetActive(false);
            }
        }
    }

    
}
