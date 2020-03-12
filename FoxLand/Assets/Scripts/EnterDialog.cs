using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerController player = new PlayerController();
        if (collision.tag == "Player"  )//&& player.Gem == 3 && player.Cherry ==3)
        {
            enterDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //PlayerController player = new PlayerController();
        if (collision.tag == "Player" )//&& player.Gem == 3 && player.Cherry == 3)
        {
            enterDialog.SetActive(false);
        }
    }
}
