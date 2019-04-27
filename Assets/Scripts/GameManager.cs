using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int roundTime;
    private float _lastTimeUpdate;
    private bool _battleStarted;
    private bool _battleEnded;

    public bool isthereP2;
    public Fighter player1;
    public Fighter player2;

    public BannerController banner;
    public AudioSource musicPlayer;
    public AudioClip backgroundMusic;

    void Start()
    {
        banner.showRoundFight();
    }

    void Update()
    {
        if (!_battleStarted && !banner.isAnimating)
        {
            _battleStarted = true;

            player1.enable = true;
            player1.UpdateHumanInput();

            if (isthereP2 == true)
            {
                player2.enable = true;
                player2.UpdateHumanInput();
            }

            AudioManager.PlaySound(backgroundMusic, musicPlayer);
        }

        if (_battleStarted && !_battleEnded)
        {
            if (roundTime > 0 && Time.time - _lastTimeUpdate > 1)
            {
                roundTime--;
                _lastTimeUpdate = Time.time;

                if (roundTime == 0)
                {
                    ExpireTime();
                }
            }

            if (player1.HealthPercent <= 0)
            {
                banner.showWinner(player2);
                _battleEnded = true;
                StartCoroutine(LoadAfterDelay(6f));
            }
            else if (player2.HealthPercent <= 0)
            {
                banner.showWinner(player1);
                _battleEnded = true;
                StartCoroutine(LoadAfterDelay(6f));
            }
        }
    }

    public IEnumerator LoadAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("MainMenu");
    }

    private void ExpireTime()
    {
        if (player1.HealthPercent > player2.HealthPercent)
        {
            player2.health = 0;
        }
        else
        {
            player1.health = 0;
        }
    }
}
