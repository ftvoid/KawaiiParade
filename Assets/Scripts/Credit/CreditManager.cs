using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CreditManager : MonoBehaviour
{
    [SerializeField] private GameObject _happyEnding;

    [SerializeField] private GameObject _gameoverEnding;

    [SerializeField] private GameObject _goodbyeEnding;

    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ => SceneChanger.Instance.ChangeScene(SceneType.Title));

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
