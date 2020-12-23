using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    Player player;
    [SerializeField]GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead)
        {
            canvas.SetActive(true);
        }

    }
}
