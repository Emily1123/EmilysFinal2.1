using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Fighter player1;
    public Fighter player2;

    public Text player1Tag;
    public Text player2Tag;
    public Text timerText;
    public Scrollbar leftBar;
    public Scrollbar rightBar;

    public GameManager battle;

    void Start()
    {
        player1Tag.text = player1.fighterName;
        player2Tag.text = player2.fighterName;
    }

    void Update()
    {
        timerText.text = battle.roundTime.ToString();

        if (leftBar.size > player1.HealthPercent)
        {
            leftBar.size -= 0.01f;
        }
        if (rightBar.size > player2.HealthPercent)
        {
            rightBar.size -= 0.01f;
        }
    }
}
