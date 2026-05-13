using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;

public class ScoreLogic : MonoBehaviour
{
    //Non-Basic
    [SerializeField] private PlayerControls _playerControls;
    
    //Basics
    private int _score;
    
    //Coroutines
    private Coroutine _scoreCoroutine;
    
    //Public Getter&Setters
    [CreateProperty]
    public int score
    {
        get { return _score; }
    }

    private void Start()
    {
        _scoreCoroutine = StartCoroutine(StartScore());
    }
    
    //Starts a timer to tick the score up.
    private IEnumerator StartScore()
    {
        if (!_playerControls.Alive)
            yield return new WaitForSeconds(.1f);

        while (_playerControls.Alive)
        {
            yield return new WaitForSeconds(.2f);
            _score += 1;
        }
    }
}
