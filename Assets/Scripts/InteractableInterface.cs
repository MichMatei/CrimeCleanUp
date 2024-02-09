using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IInteractable
{
    GameObject Initial_prefab { set; get; }

    GameObject Messed_prefab { set; get; }

    Collider Ground_check {  get; }

    GameObject Feedback_button { set; get; }

    TextMeshProUGUI hint { set; get; }

    public Objects_Types object_type { set; get; }

    float Progress  { set; get; }

    bool Arranged { set; get; } 

    void Start()
    {
        Initial_prefab.SetActive(false);
        Messed_prefab.SetActive(true);
        Feedback_button.SetActive(true);
    }
}
