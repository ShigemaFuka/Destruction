using UniRx;

public class TimeScalePresenter
{
    public TimeScalePresenter(UseTimeScale model, TimeScaleView view)
    {
        var model1 = model;
        var view1 = view;

        // Modelの変更をViewに反映
        model1.Num
            .Subscribe(x => view1.UpdateText(x))
            .AddTo(view1); // Viewのライフサイクルに紐付け

        // Viewのボタン操作をModelに反映
        view1._targetButton.onClick.AsObservable()
            .Subscribe(_ => model1.ChangeNum())
            .AddTo(view1);
    }
}