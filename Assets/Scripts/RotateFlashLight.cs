using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFlashLight : MonoBehaviour
{
    [SerializeField] GameObject flashLight;
    [SerializeField] GameObject flashLightLeftRotation;
    [SerializeField] GameObject flashLightRightRotation;

    int counter;
    int speed = 5;

    Quaternion initialRotation;
    Quaternion leftRotation;
    Quaternion rightRotation;

    enum RotationState
    {
        reachedLeft,
        reachedRight,
        rotating
    }

    RotationState flashState;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = flashLight.transform.rotation;
        leftRotation = flashLightLeftRotation.transform.rotation;
        rightRotation = flashLightRightRotation.transform.rotation;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(RotateLantern());
        }
    }

    IEnumerator RotateLantern()
    {     
        while (counter < 5)
        {        
            if (counter == 0)
            {
                while (transform.rotation != leftRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, leftRotation, speed * Time.deltaTime);
                    yield return null;
                }
            }

            if (counter > 0 && flashState == RotationState.reachedLeft)
            {
                while (transform.rotation != rightRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, rightRotation, speed * Time.deltaTime);
                    yield return null;
                }
                flashState = RotationState.reachedRight;
            }

            if (counter > 0 && flashState == RotationState.reachedRight)
            {
                while (transform.rotation != leftRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, leftRotation, speed * Time.deltaTime);
                    yield return null;
                }
                flashState = RotationState.reachedLeft;
            }
            counter++;                      
        }

        while (transform.rotation != initialRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, speed * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }
}
