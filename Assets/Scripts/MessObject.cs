using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MessObject : MonoBehaviour
{
    public GameObject initial_prefab;
    public GameObject messed_prefab;

    public Collider ground_check;

    public GameObject e_button;
    public GameObject skill_check;
    public Image button_fill;
    public TextMeshProUGUI hint;
    public TextMeshProUGUI skill_check_hint;
    public Slider indicator_skill_check;
    public Scrollbar skill_check_background;

    public Objects_Types object_type;

    float object_progress = 0.0f;
    public bool properly_set = false;
    public float min_skill_check;
    public float max_skill_check;
    public bool end_reached = false;

    // Start is called before the first frame update
    void Start()
    {
        initial_prefab.SetActive(false);
        messed_prefab.SetActive(true);
        e_button.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !properly_set)
        {
            
            switch (this.object_type)
            {
                case Objects_Types.Hold_E_Objects:
                    {
                        e_button.SetActive(true);
                        hint.alignment = TextAlignmentOptions.CenterGeoAligned;
                        hint.text = "Hold";
                        break;
                    }
                case Objects_Types.Hammer_E_Objects:
                    {
                        e_button.SetActive(true);
                        hint.alignment = TextAlignmentOptions.CenterGeoAligned;
                        hint.text = "Press";
                        break;
                    }
                case Objects_Types.Skill_Check_Objects:
                    {
                        skill_check.SetActive(true);
                        min_skill_check = Random.Range(0.25f, 0.75f);
                        max_skill_check = Random.Range(min_skill_check + 0.1f, 1.0f);
                        end_reached = false;
                        hint.alignment = TextAlignmentOptions.CenterGeoAligned;
                        hint.text = "Hold";
                        break;
                    }
            }
        }

        button_fill.fillAmount = object_progress;
    }

    private void OnTriggerExit(Collider other)
    {
        e_button.SetActive(false);
        skill_check.SetActive(false);
        if(!properly_set)
        {
            if(this.object_type != Objects_Types.Hammer_E_Objects)
            {
                object_progress = 0.0f;
            }
            end_reached = false;
            hint.text = "";
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (this.object_type)
            {
                case Objects_Types.Hold_E_Objects:
                    {
                        if (Input.GetKey(KeyCode.E) && object_progress < 1.0f)
                        {
                            object_progress += 0.025f;
                        }
                        else if (object_progress >= 1.0f && initial_prefab.activeSelf == false)
                        {
                            e_button.SetActive(false);
                            messed_prefab.SetActive(false);
                            initial_prefab.SetActive(true);
                            properly_set = true;
                            UserInterfaceManager.userInterfaceInstance.CheckTagForCleaning(gameObject);
                        }
                        else if (object_progress >= 0.01f)
                        {
                            object_progress -= 0.01f;
                        }
                        break;
                    }

                case Objects_Types.Hammer_E_Objects:
                    {
                        Quaternion temp_rotation = messed_prefab.transform.rotation;
                        Vector3 temp_position = messed_prefab.transform.position;

                        if(object_progress <= 0.9f && Input.GetKeyDown(KeyCode.E))
                        {                     
                            object_progress += 0.2f;
                            messed_prefab.transform.rotation = Quaternion.Lerp(temp_rotation, initial_prefab.transform.rotation, object_progress);
                            messed_prefab.transform.position = Vector3.Lerp(temp_position, initial_prefab.transform.position, object_progress);
                            break;                            
                        }
                        else if (object_progress >= 0.9f && initial_prefab.activeSelf == false)
                        {
                            e_button.SetActive(false);
                            messed_prefab.SetActive(false);
                            initial_prefab.SetActive(true);
                            properly_set = true;
                            UserInterfaceManager.userInterfaceInstance.CheckTagForCleaning(gameObject);
                        }
                        //else if (object_progress >= 0.01f)
                        //{
                        //    object_progress -= 0.01f;
                        //}
                        break;
                    }

                case Objects_Types.Skill_Check_Objects:
                    {
                        skill_check_background.size = 1 - (min_skill_check + 1 - max_skill_check);
                        skill_check_background.value =  0 + max_skill_check - skill_check_background.size /2;
                                     
                       
                        if (Input.GetKey(KeyCode.E) && skill_check.activeSelf)
                        {
                            if (!end_reached)
                            {
                                object_progress += 0.02f;
                                if (object_progress >= 0.9f)
                                {
                                    end_reached = true;
                                }
                            }
                            else 
                            {
                                object_progress -= 0.02f;
                                if (object_progress <= 0.1f)
                                {
                                    end_reached = false;
                                }
                            }
                        }
                        if (object_progress >= min_skill_check && object_progress <= max_skill_check)
                        {
                            skill_check_hint.text = "Release";

                            if (Input.GetKey(KeyCode.E).Equals(false) && initial_prefab.activeSelf == false)
                            {
                                skill_check.SetActive(false);
                                messed_prefab.SetActive(false);
                                initial_prefab.SetActive(true);
                                properly_set = true;
                                UserInterfaceManager.userInterfaceInstance.CheckTagForCleaning(gameObject);
                                Debug.Log("in here");
                            }                           

                        }    
                        else
                        {
                            skill_check_hint.text = "Hold";
                        }
                       
                        break;
                    }

            }


        }

        button_fill.fillAmount = object_progress;
        indicator_skill_check.value = object_progress;
    }

   
}

public enum Objects_Types
{
    Hold_E_Objects,
    Hammer_E_Objects,
    Skill_Check_Objects
}
