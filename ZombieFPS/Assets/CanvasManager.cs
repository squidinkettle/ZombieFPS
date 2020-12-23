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
   
    }

  
    public void HandleDeath()
    {
        canvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
