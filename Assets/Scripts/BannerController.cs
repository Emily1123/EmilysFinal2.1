using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerController : MonoBehaviour
{
    public Animator animator;
    public GameObject winner;
    public GameObject overlay;
    public Text textWinner;
    private AudioSource _audioPlayer;

    private bool _animating;

    void Start()
    {
        animator.GetComponent<Animator>();
        _audioPlayer = GetComponent<AudioSource>();
    }

    public IEnumerator HideAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        winner.SetActive(false);
        overlay.SetActive(false);
    }

    public void showRoundFight()
    {
        _animating = true;
        animator.SetTrigger("ShowRoundFight");
    }

    public void showWinner(Fighter fighter)
    {
        textWinner.text = fighter.fighterName;
        _animating = true;
        animator.SetTrigger("ShowYouWin");
        winner.SetActive(true);
        overlay.SetActive(true);
        StartCoroutine(HideAfterDelay(1.7f));
    }

    public void playVoice(AudioClip voice)
    {
        AudioManager.PlaySound(voice, _audioPlayer);
    }

    public void animationEnded()
    {
        _animating = false;
    }

    public bool isAnimating
    {
        get{ return _animating; }
    }
}
