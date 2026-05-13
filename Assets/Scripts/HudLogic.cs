using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HudLogic : MonoBehaviour
{
    //Non-Basic
    [SerializeField] private UIDocument _root;
    [SerializeField] private ScoreLogic scoreLogic;
    private Label _scoreLabel;
    
    private void Awake()
    {
        _scoreLabel = _root.rootVisualElement.Q<Label>("scoreLabel");
        
        DataBinding _scoreBinding = new DataBinding
        {
            dataSource = scoreLogic,
            dataSourcePath = new Unity.Properties.PropertyPath("score"),
            bindingMode = BindingMode.ToTarget
        };
        _scoreBinding.updateTrigger = BindingUpdateTrigger.OnSourceChanged;
        _scoreLabel.SetBinding("text", _scoreBinding);

    }
}
