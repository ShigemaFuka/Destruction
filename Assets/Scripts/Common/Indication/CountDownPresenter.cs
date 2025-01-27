using UnityEngine;
using UniRx;

public class CountDownPresenter : MonoBehaviour
{
    [SerializeField] private CountDownView _view;
    private WaveManager _model;

    private void Start()
    {
        _model = WaveManager.Instance;
        _model.FirstWaveInterval.Subscribe(x =>
        {
            if (_model.FirstWaveInterval.Value <= 0.05)
            {
                _view.SetActive(false);
            }
            else _view.UpdateText(x); // Viewに反映
        }).AddTo(this);
    }
}