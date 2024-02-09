using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBinScript : MonoBehaviour
{
    [SerializeField] GameObject BinTop;
    [SerializeField] GameObject openedRotationGO;


    Quaternion closedRotation;
    Quaternion openedRotation;

    IEnumerator openCoroutine;

    enum BinState
    {
        closed,
        opened,
        changing
    }

    BinState binState;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && binState != BinState.changing)
        {
            StopAllCoroutines();
            openCoroutine = OpenBin();
            StartCoroutine(openCoroutine);
        }
    }

    private IEnumerator OpenBin()
    {
        if (binState == BinState.closed)
        {
            while (BinTop.transform.rotation != openedRotation)
            {
                BinTop.transform.rotation = Quaternion.Lerp(BinTop.transform.rotation, openedRotation, 10 * Time.deltaTime);
                binState = BinState.changing;
                yield return null;
                Debug.Log(binState);
            }
            binState = BinState.opened;
            Debug.Log(binState);
        }
        else if (binState == BinState.opened)
        {
            while (BinTop.transform.rotation != closedRotation)
            {
                BinTop.transform.rotation = Quaternion.Lerp(BinTop.transform.rotation, closedRotation, 10 * Time.deltaTime);
                binState = BinState.changing;
                yield return null;
                Debug.Log(binState);
            }
            binState = BinState.closed;
            Debug.Log(binState);
        }

        yield return null;
    }
}

