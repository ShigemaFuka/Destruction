using UnityEngine;

/// <summary>
/// 爆風を受けるか
/// </summary>
public interface IReceiveBlast
{
    public void DoReceiveBlast(float power, GameObject epicenter);
}