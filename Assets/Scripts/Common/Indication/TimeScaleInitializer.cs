using UnityEngine;

public class TimeScaleInitializer : MonoBehaviour
{
    [SerializeField] private UseTimeScale _model;
    [SerializeField] private TimeScaleView _view;

    private void Start()
    {
        var presenter = new TimeScalePresenter(_model, _view);
    }
}