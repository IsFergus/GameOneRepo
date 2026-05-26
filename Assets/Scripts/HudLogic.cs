using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HudLogic : MonoBehaviour
{
    //Non-Basic
    [SerializeField] private ManagerLogic _managerLogic;
    
    //UI
    private UIDocument _endScreen;
    private Button _restartButton;
    private Button _quitButton;
    private Label _scoreLabel;
    private Label _highScoreLabel;
    
    private void Awake()
    {
        if (TryGetComponent(out _endScreen)){}
    }

    private void OnEnable()
    {
        //Defining UI Elements
        _quitButton = _endScreen.rootVisualElement.Q<Button>("quitButton");
        _restartButton = _endScreen.rootVisualElement.Q<Button>("restartButton");
        _scoreLabel = _endScreen.rootVisualElement.Q<Label>("CurrentScore");
        _highScoreLabel = _endScreen.rootVisualElement.Q<Label>("HighScore");
        
        //Setting Label Values
        _scoreLabel.text = new string("Score: " + _managerLogic.score);
        
        if (_managerLogic.score > PlayerPrefs.GetFloat("CurrentHighScore"))
        {
            PlayerPrefs.SetFloat("CurrentHighScore", _managerLogic.score);
            _highScoreLabel.text = "High Score: " + PlayerPrefs.GetFloat("CurrentHighScore");
        }
        else
        {
            _highScoreLabel.text = "High Score: " + PlayerPrefs.GetFloat("CurrentHighScore");
        }
        
        //Setting UI Button Callbacks
        _restartButton.RegisterCallback<ClickEvent>(restartLevel);
        _quitButton.RegisterCallback<ClickEvent>(evt => Application.Quit());
    }

    private void OnDisable()
    {
        _restartButton.UnregisterCallback<ClickEvent>(restartLevel);
    }

    private void restartLevel(ClickEvent evt)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
