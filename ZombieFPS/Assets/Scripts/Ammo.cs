using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [SerializeField] int maxAmmo;
    private int _ammo;
    [SerializeField] int maxClips;
    private int _clips;

    public int MaxAmmo
    {
        get { return maxAmmo; }
    }
    public int MaxClips
    {
        get { return maxClips; }
    }

    public int AmmoNum
    {
        get { return _ammo; }
        set { _ammo = value; }
    }

    public int Clips
    {
        get { return _clips; }
        set { _clips = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _ammo = maxAmmo;
        _clips = maxClips;

        
    }

}
