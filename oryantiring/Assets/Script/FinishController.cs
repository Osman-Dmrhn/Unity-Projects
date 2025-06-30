using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="player")
        {
            if (game.GetComponent<GameController>().top==9)
            {
                game.GetComponent<GameController>().kazanma();
            }
        }
    }
}
