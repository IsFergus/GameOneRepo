using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class ManagerLogic : MonoBehaviour
{
    //Non-Basic
    [SerializeField] private UIDocument _hudRoot;
    [SerializeField] private UIDocument _endScreen;
    [SerializeField] private GameObject player;
    private PlayerControls _playerControls;
    private GameObject _playerInstance;
    private Label _scoreLabel;
    
    //Basic
    private Coroutine _scoreCoroutine;
    
    //Coroutines
    private int _score;

    //Properties
    [CreateProperty]
    public int score
    {
        get { return _score; }
    }

    private void Start()
    {
        if (!player.TryGetComponent(out _playerControls)) { }
        SpawnPlayer();
        ScoreSetup();
        
        _playerControls.onDeath += OpenEndScreen;

        _endScreen.gameObject.SetActive(false);
    }

    private void SpawnPlayer()
    {
        _playerInstance = Instantiate(player, new Vector3(-7.5f, -3.8f, 0),  Quaternion.identity);
        _playerControls.Alive = true;
    }

    private void ScoreSetup()
    {
        ScoreUISetup();
        StartScoreLogic();
    }
    
    private void ScoreUISetup()
    {
        _scoreLabel = _hudRoot.rootVisualElement.Q<Label>("scoreLabel");
        DataBinding _scoreBinding = new DataBinding
        {
            dataSource = this,
            dataSourcePath = new PropertyPath("score"),
            bindingMode = BindingMode.ToTarget
        };
        _scoreBinding.updateTrigger = BindingUpdateTrigger.OnSourceChanged;
        _scoreLabel.SetBinding("text", _scoreBinding);
    }
    
    private void StartScoreLogic()
    {
        _scoreCoroutine = StartCoroutine(StartScore());
    }

    private void OpenEndScreen()
    {
        Debug.Log("player opened");
        _endScreen.gameObject.SetActive(true);
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
