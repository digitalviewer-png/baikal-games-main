using UnityEngine;

public class AnimatorShowingAnimation : UiShowingAnimation
{
    [SerializeField] private Animator _animator;

    [Header("Active states")]
    [SerializeField] protected string _showAnimation;
    [SerializeField] protected string _hideAnimation;

    [Header("Static states")]
    [SerializeField] protected string _showedState;
    [SerializeField] protected string _hidedState;

    private bool _isShowed;

    public override bool IsShowed => _isShowed;

    public override void Show()
    {
        _isShowed = true;
        _animator.Play(_showAnimation);
    }

    public override void Hide()
    {
        _isShowed = false;
        _animator.Play(_hideAnimation);
    }

    public override void ShowInstantly()
    {
        _isShowed = true;
        _animator.Play(_showedState);
    }

    public override void HideInstantly()
    {
        _isShowed = false;
        _animator.Play(_hidedState);
    }
}