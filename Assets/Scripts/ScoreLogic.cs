using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;

public class ScoreLogic : MonoBehaviour
{
    [SerializeField]
    private PlayerControls _playerControls;
    private int _score;
    private Coroutine _scoreCoroutine;
    [CreateProperty]
    public int score
    {
        get { return _score; }
    }

    private void Start()
    {
        _scoreCoroutine = StartCoroutine(StartScore());
    }
    
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
