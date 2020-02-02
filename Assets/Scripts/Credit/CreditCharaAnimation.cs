using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditCharaAnimation : MonoBehaviour
{
    [SerializeField] private Sprite _spripte1;
    [SerializeField] private Sprite _spripte2;

    private IEnumerator Start()
    {
        var image = GetComponent<Image>();

        while ( true )
        {
            image.sprite = _spripte1;
            yield return new WaitForSeconds(0.361f);

            image.sprite = _spripte2;
            yield return new WaitForSeconds(0.361f);
        }
    }
}
