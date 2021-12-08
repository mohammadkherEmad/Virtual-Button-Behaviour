
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtonEventHandler : MonoBehaviour 
{

    public GameObject[] VBTN;
    public GameObject[] FlowingObjects;
    //public Material VirtualButtonDefault;
    //public Material VirtualButtonPressed;
    public float ButtonReleaseTimeDelay;
    //public GameObject AcademicRecord;
    //public GameObject RegisteredCourses;
    private int i=0;
    VirtualButtonBehaviour[] MyVB;

    void Awake()
    {

        // Register with the virtual buttons ObserverBehaviour
        
        MyVB = GetComponentsInChildren<VirtualButtonBehaviour>();


        // making the Buttons Touchable and Untouchable
        for (i = 0; i < MyVB.Length; ++i)
        {
            MyVB[i].RegisterOnButtonPressed(OnButtonPressed);
            MyVB[i].RegisterOnButtonReleased(OnButtonReleased);
        }
        
        for(i = 0; i<VBTN.Length; i++)
        {
            VBTN[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
            VBTN[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        }

        // making Other Objects UnVisable
        for(i =0; i<FlowingObjects.Length ; i++)
        {
            FlowingObjects[i].SetActive(false);
        }
        
        
    }


    //handling errors
    public void Destroy()
    {
        
        MyVB = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (var i = 0; i < MyVB.Length; ++i)
        {
            MyVB[i].UnregisterOnButtonPressed(OnButtonPressed);
            MyVB[i].UnregisterOnButtonReleased(OnButtonReleased);
        }
        for (int j = 0; j < VBTN.Length; ++j)
        {
            VBTN[j].GetComponent<VirtualButtonBehaviour>().UnregisterOnButtonPressed(OnButtonPressed);
            VBTN[j].GetComponent<VirtualButtonBehaviour>().UnregisterOnButtonPressed(OnButtonReleased);
        }
    }


    
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //StopAllCoroutines();
        BroadcastMessage("HandleVirtualButtonPressed", SendMessageOptions.DontRequireReceiver);
        
        //////////////////////////////////////////////////// TEST ///////////////////////////////
        switch (vb.VirtualButtonName)
        {
            case "Courses Requests":
                //SetVirtualButtonMaterial(VirtualButtonPressed, vb);
                Debug.Log(vb.VirtualButtonName);
                break;
            case "Academic Record":
                //SetVirtualButtonMaterial(VirtualButtonPressed, vb);
                Debug.Log(vb.VirtualButtonName);

                for(i=0;i<FlowingObjects.Length;i++)
                {
                    if(FlowingObjects[i].name == "AcademicRecord")
                    {
                        FlowingObjects[i].SetActive(true);
                    }
                    else
                    {
                        FlowingObjects[i].SetActive(false);
                    }
                }
                //AcademicRecord.SetActive(true);

                for (i = 0; i < VBTN.Length; i++)
                {
                    if (VBTN[i].name == "BackBTN1")
                    {
                        VBTN[i].GetComponent<VirtualButtonBehaviour>().enabled = true;
                        VBTN[i].SetActive(true);
                    }
                    else
                    {
                        VBTN[i].GetComponent<VirtualButtonBehaviour>().enabled = false;
                        VBTN[i].SetActive(false);
                    }
                }
                break;
            case "Show Registered Courses":
                //SetVirtualButtonMaterial(VirtualButtonPressed, vb);
                Debug.Log(vb.VirtualButtonName);
                for (i = 0; i < FlowingObjects.Length; i++)
                {
                    if (FlowingObjects[i].name == "RegisteredCourses")
                    {
                        FlowingObjects[i].SetActive(true);
                    }
                    else
                    {
                        FlowingObjects[i].SetActive(false);
                    }
                }
                
                for(i =0;i< VBTN.Length; i++)
                {
                    if(VBTN[i].name == "BackBTN1")
                    {
                        VBTN[i].GetComponent<VirtualButtonBehaviour>().enabled = true;
                        VBTN[i].SetActive(true);
                    }
                    else
                    {
                        VBTN[i].GetComponent<VirtualButtonBehaviour>().enabled = false;
                        VBTN[i].SetActive(false);
                    }
                }
                
                break;

            case "BackBTN1":
                Debug.Log(vb.VirtualButtonName);
                for (i = 0; i < FlowingObjects.Length; i++)
                {
                    FlowingObjects[i].SetActive(false);
                }


                for (i = 0; i < VBTN.Length; i++)
                {
                    if (VBTN[i].name == "BackBTN1")
                    {
                        VBTN[i].GetComponent<VirtualButtonBehaviour>().enabled = false;
                        VBTN[i].SetActive(false);
                    }
                    else
                    {
                        VBTN[i].GetComponent<VirtualButtonBehaviour>().enabled = true;
                        VBTN[i].SetActive(true);
                    }
                }
                break;
            default:
                break;
        }


    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "Courses Requests":
                //SetVirtualButtonMaterial(VirtualButtonDefault, vb);
                Debug.Log(vb.VirtualButtonName);
                StartCoroutine(DelayOnButtonReleasedEvent(ButtonReleaseTimeDelay));
                break;
            case "Academic Record":
                //SetVirtualButtonMaterial(VirtualButtonDefault, vb);
                Debug.Log(vb.VirtualButtonName);

                
                StartCoroutine(DelayOnButtonReleasedEvent(ButtonReleaseTimeDelay));
                break;
            case "Show Registered Courses":
                //SetVirtualButtonMaterial(VirtualButtonDefault, vb);
                Debug.Log(vb.VirtualButtonName);
                
                StartCoroutine(DelayOnButtonReleasedEvent(ButtonReleaseTimeDelay));
                break;
            default:
                break;
        }
        
        
    }

    /*void SetVirtualButtonMaterial(Material material, VirtualButtonBehaviour vb)
    {
        //for (var i = 0; i < MyVB.Length; ++i)
        //{
        if (material != null)
            vb.GetComponent<MeshRenderer>().sharedMaterial = material;
        //}
    }*/

    IEnumerator DelayOnButtonReleasedEvent(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        BroadcastMessage("HandleVirtualButtonReleased", SendMessageOptions.DontRequireReceiver);
    }
}