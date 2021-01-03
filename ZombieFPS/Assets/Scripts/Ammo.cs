using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{





    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int maxAmmo;
        public int maxClips;
        public int _ammo;
        public int _clips;

    
        public int Ammo
        {
            get { return _ammo; }
            set { _ammo = value; }
        }
        public int Clips
        {
            get { return _clips; }
            set { _clips = value; }
        }
    }
    
   
    public int GetCurrentAmmo(AmmoType ammoType)
    {
        var currentAmmo = GetAmmoSlot(ammoType);

        return currentAmmo.Ammo;
    }

    public void SetCurrentAmmo(AmmoType ammoType)
    {
        var currentAmmo = GetAmmoSlot(ammoType);
        currentAmmo.Ammo--;
    }
    public void ReloadCurrentAmmo(AmmoType ammoType, int num, bool isAmmoBox=false)
    {
        var currentAmmo = GetAmmoSlot(ammoType);
        currentAmmo.Ammo=num;
        if (currentAmmo.Ammo > currentAmmo.maxAmmo)
        {
            currentAmmo.Ammo = currentAmmo.maxAmmo;
        }
        if(!isAmmoBox)
            SetClips(ammoType);
    }
    public int GetMaxAmmo(AmmoType ammoType)
    {
        var maxA = GetAmmoSlot(ammoType);
        return maxA.maxAmmo;
    }

    public int GetClips(AmmoType ammoType)
    {
        var numClips = GetAmmoSlot(ammoType);
        return numClips.Clips;
    }
    public void SetClips(AmmoType ammoType)
    {
        var numClips = GetAmmoSlot(ammoType);
        numClips.Clips--;
    }




    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if (ammoType == slot.ammoType)
            {
                return slot;
            }
        }

        return null;
    }




    // Start is called before the first frame update
    void Start()
    {
       
  

    }

}
