using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HudLogic : MonoBehaviour
{
    [SerializeField] 
    private UIDocument _root;
    private Label _scoreLabel;
    [SerializeField]
    private ScoreLogic scoreLogic;
    
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
