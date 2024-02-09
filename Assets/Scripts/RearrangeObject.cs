using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearrangeObject : MonoBehaviour
{
    public GameObject objectToBeMoved;
    public Transform correctPosition;

    bool inRange;
    bool objectRearranged;

    Vector3 originalObjectPosition;
    Vector3 messedUpObjectPosition;


    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        objectRearranged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            objectToBeMoved.transform.SetPositionAndRotation(correctPosition.position, correctPosition.rotation);
            objectRearranged = true;
            UserInterfaceManager.userInterfaceInstance.pressEtoInteract.SetActive(false);

            Debug.Log(tag);
            UserInterfaceManager.userInterfaceInstance.CheckTagForCleaning(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectRearranged)
        {
            inRange = true;
            UserInterfaceManager.userInterfaceInstance.pressEtoInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        UserInterfaceManager.userInterfaceInstance.pressEtoInteract.SetActive(false);
    }
}
