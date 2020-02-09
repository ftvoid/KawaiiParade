using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CreditManager : MonoBehaviour
{
    [SerializeField] private GameObject _happyEnding;

    [SerializeField] private GameObject _gameoverEnding;

    [SerializeField] private GameObject _goodbyeEnding;

    [SerializeField] private float _ignoreDuration = 5;

    private void Start()
    {
        var allowInputTime = Time.time + _ignoreDuration;

        this.UpdateAsObservable()
            .Where(_ => Time.time >= allowInputTime)
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ =>
            {
                SoundManager.PlaySound(SoundID.Click);
                SceneChanger.Instance.ChangeScene(SceneType.Title);
            });

        var param = SceneChanger.Instance.Param;
        var senario = param != null ? (string)param : "Happy";

        switch ( senario )
        {
            case "Happy":
                _happyEnding.SetActive(true);
                break;

            case "Gameover":
                _gameoverEnding.SetActive(true);
                break;

            case "Goodbye":
                _goodbyeEnding.SetActive(true);
                break;
        }
    }
}
