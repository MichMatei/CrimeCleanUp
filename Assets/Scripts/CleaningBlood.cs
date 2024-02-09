using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningBlood : MonoBehaviour
{
    UserInterfaceManager userIM;
    bool canClean = false;

    [SerializeField]
    GameObject bloodSpot;

    // Start is called before the first frame update
    void Start()
    {
        userIM = UserInterfaceManager.userInterfaceInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (canClean && Input.GetKeyDown(KeyCode.E) && bloodSpot.activeInHierarchy)
        {
            Debug.Log("hapening");
            bloodSpot.SetActive(false);           
            userIM.pressEtoInteract.SetActive(false);
            userIM.CheckTagForCleaning(gameObject);
        }
    }
}
