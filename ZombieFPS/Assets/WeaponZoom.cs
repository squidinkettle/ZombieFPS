using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera FPScamera;
    [SerializeField] float zoomSpeed;
    [SerializeField] float zoomIn = 35f;
    [SerializeField] float zoomOut = 60f;
    [SerializeField] float zoomOutSensitivity;
    [SerializeField] float zoomInSensitivity;
    [SerializeField] RigidbodyFirstPersonController fpsController;


    bool isZoomed=true;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SmoothZoom(zoomSpeed));
        
    }
    IEnumerator SmoothZoom(float speed)
    {
        while (true)
        {
        
            if (Input.GetButtonDown("Fire2"))
            {
                isZoomed = !isZoomed;
                if(isZoomed)
                {

            
                    while (FPScamera.fieldOfView > zoomIn)
                    {
                        FPScamera.fieldOfView-=2;
                        yield return new WaitForSeconds(speed);
                    }
                    fpsController.mouseLook.XSensitivity = zoomInSensitivity;
                    fpsController.mouseLook.YSensitivity = zoomInSensitivity;

                }
                else
                {
                    while (FPScamera.fieldOfView < zoomOut)
                    {
                        FPScamera.fieldOfView+=2;
                        yield return new WaitForSeconds(speed);
                    }
                    fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
                    fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
                }
            }


            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
