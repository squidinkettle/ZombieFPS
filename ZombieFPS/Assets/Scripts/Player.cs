using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHealth;


    private int _health;
    public int Health
    {
        get{ return _health; }
        set { _health -= value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;

    }

    



    // Update is called once per frame
    void Update()
    {
        
    }
}
