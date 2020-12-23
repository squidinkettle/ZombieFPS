using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private bool _isDead;


    private int _health;
    public int Health
    {
        get{ return _health; }
        set { _health -= value;
            if (_health <= 0) {
                _isDead = true;
                CallCanvasManager();
            }
        }
    }

    private void CallCanvasManager()
    {
        CanvasManager canvasManager = FindObjectOfType<CanvasManager>();
        canvasManager.HandleDeath();
    }

    public bool IsDead
    {
        get { return _isDead; }
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
