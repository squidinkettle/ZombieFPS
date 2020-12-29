using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{

    [SerializeField] List<GameObject> weapons;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentValue = Input.GetAxis("MouseScrollWheel");
        print("current value: "+currentValue+" scroll: "+Input.GetAxis("MouseScrollWheel"));


        if (Input.GetAxis("MouseScrollWheel") > currentValue)
        {
            UpdateWeapon(-1);
        }
        else if (Input.GetAxis("MouseScrollWheel") < currentValue) ;
        {
            UpdateWeapon(1);
        }

    }

    void UpdateWeapon(int n)
    {
        print("updating");
        if (index+1 > weapons.Count) { return; }
        if (index < 0) { return; }

        weapons[index].SetActive(false);
        index += n;

        weapons[index].SetActive(true);
    }


}
