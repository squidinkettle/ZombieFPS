using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    [SerializeField] AmmoType typeOfAmmo;
    [SerializeField] int ammoAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var ammo = FindObjectOfType<Ammo>();
            ammo.ReloadCurrentAmmo(typeOfAmmo, ammoAmount, true);
            Destroy(this.gameObject);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
