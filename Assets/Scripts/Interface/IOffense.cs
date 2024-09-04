using UnityEngine;

public interface IOffense
{
    /// <summary>
    /// 対象に作用する処理を実行する
    /// </summary>
    /// <param name="target"> 対象 </param>
    public void Offense(GameObject target);
}