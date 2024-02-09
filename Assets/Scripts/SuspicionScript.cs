using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionScript : MonoBehaviour
{
    UserInterfaceManager myUIManager;

    public Collider flashlightCollider, playerCollider;
    [SerializeField] Material coneMaterial;
    [SerializeField] GameObject coneGameObject;

    // Start is called before the first frame update
    void Start()
    {
        myUIManager = UserInterfaceManager.userInterfaceInstance;
        coneGameObject.gameObject.GetComponent<Renderer>().material = coneMaterial;
        flashlightCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            myUIManager.suspicionMeter.value += 10f;
        }

        if (flashlightCollider.bounds.Intersects(playerCollider.bounds))
        {
            myUIManager.suspicionMeter.value += 0.1f;
        }
    }

    private void FixedUpdate()
    {
        if (!myUIManager.gameFinished)
        {
            myUIManager.suspicionMeter.value += 0.9f * Time.deltaTime;
        }
       
    }
}