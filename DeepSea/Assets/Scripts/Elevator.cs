using game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float WinTimer = 5;

    private float WinTimerReal = 0;

    [SerializeField]
    private GameObject Block;

    public bool Inside;

    private bool Finished;

    public GameObject Player;


    void Start()
    {

    }

    void Update()
    {
        if (Inside)
        {
            Block.SetActive(true);
            transform.Translate(Vector3.up * Time.deltaTime * 10f);

            if (WinTimerReal < WinTimer)
            {
                WinTimerReal += Time.deltaTime;
            }
            else
            {
                if (!Finished)
                {
                    Player.GetComponent<PlayerOxygenController>().PlayerWin();
                    Finished = true;
                }
            }
        }

    }

}
