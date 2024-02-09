using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTheDoor : MonoBehaviour
{
    Quaternion targetRotation;
    Quaternion originalRotation;

    bool doorRattled;
    bool up, down;

    bool doorBeingHeld;
    bool inRange;

    int counter;
    float timer;
    float doorTimer;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, -40f);

        doorRattled = false;
        up = false;
        down = true;

        doorBeingHeld = false;
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(UserInterfaceManager.userInterfaceInstance.suspicionMeter.IsActive())
        {
            timer += Time.deltaTime;
        }
        
        if (timer > 15 && !doorRattled)
        {
            int a = Random.Range(0, 3);

            if (a == 0)
            {
                doorRattled = true;
                timer = 0;
                UserInterfaceManager.userInterfaceInstance.doorForced.SetActive(true);
            }
            else
            {
                timer = 0;
            }
        }

        if (inRange && Input.GetKey(KeyCode.E))
        {
            doorBeingHeld = true;
            UserInterfaceManager.userInterfaceInstance.holdEtoInteract.SetActive(false);
            UserInterfaceManager.userInterfaceInstance.holdingE.SetActive(true);

            UserInterfaceManager.userInterfaceInstance.circularLoadingPart.transform.rotation *= Quaternion.Euler(0, 0, -100 * Time.deltaTime);
        }
        else if (inRange)
        {
            doorBeingHeld = false;
            UserInterfaceManager.userInterfaceInstance.holdEtoInteract.SetActive(true);
            UserInterfaceManager.userInterfaceInstance.holdingE.SetActive(false);
        }
       
        if (doorRattled)
        {
            doorTimer += Time.deltaTime;
            if (down)
            {
                transform.rotation *= Quaternion.Euler(0f, 0f, -0.5f);
                if (targetRotation.z > transform.rotation.z)
                {
                    down = false;
                    up = true;
                }
            }

            if (up)
            {
                transform.rotation *= Quaternion.Euler(0f, 0f, 0.5f);
                if (originalRotation.z < transform.rotation.z)
                {
                    down = true;
                    up = false;
                    counter++;
                }
            }

            if (doorTimer > 10 && doorBeingHeld == false && !UserInterfaceManager.userInterfaceInstance.gameFinished)
            {
                doorRattled = false;
                down = true;
                inRange = false;
                counter = 0;
                UserInterfaceManager.userInterfaceInstance.suspicionMeter.value = UserInterfaceManager.userInterfaceInstance.suspicionMeter.maxValue;
                UserInterfaceManager.userInterfaceInstance.holdEtoInteract.SetActive(false);
                UserInterfaceManager.userInterfaceInstance.doorForced.SetActive(false);
                UserInterfaceManager.userInterfaceInstance.holdingE.SetActive(false);
                timer = 0;
                doorTimer = 0;
            }
            else if (doorTimer > 10 && doorBeingHeld == true)
            {
                doorRattled = false;
                down = true;
                inRange = false;
                counter = 0;
                UserInterfaceManager.userInterfaceInstance.holdEtoInteract.SetActive(false);
                UserInterfaceManager.userInterfaceInstance.doorForced.SetActive(false);
                UserInterfaceManager.userInterfaceInstance.holdingE.SetActive(false);
                timer = 0;
                doorTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (doorRattled)
        {
            UserInterfaceManager.userInterfaceInstance.holdEtoInteract.SetActive(true); inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        doorBeingHeld = false;
        UserInterfaceManager.userInterfaceInstance.holdEtoInteract.SetActive(false);
        UserInterfaceManager.userInterfaceInstance.holdingE.SetActive(false);
    }
}
