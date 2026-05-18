using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HudLogic : MonoBehaviour
{
    private UIDocument _endScreen;
    private Button _restartButton;
    private Label _scoreLabel;
    
    private void Awake()
    {
        if (TryGetComponent(out _endScreen)){}
        //_restartButton = _endScreen.rootVisualElement.Q<Button>("restartButton");
    }

    private void OnEnable()
    {
        Debug.Log("enabled");
        _restartButton = _endScreen.rootVisualElement.Q<Button>("restartButton");
        _scoreLabel = _endScreen.rootVisualElement.Q<Label>("CurrentScore");
        
        _restartButton.RegisterCallback<ClickEvent>(debugFunction);
        
        
    }

    private void OnDisable()
    {
        Debug.Log("disabled");
        _restartButton.UnregisterCallback<ClickEvent>(debugFunction);
    }

    private void debugFunction(ClickEvent evt)
    {
        //Debug.Log("debugFunction");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
