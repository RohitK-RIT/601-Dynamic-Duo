using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Misc
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text loadingText;

        private const string Loading = "Loading";

        private int _numberOfDots;

        private void OnEnable()
        {
            StartCoroutine(Display());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Display()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                if (++_numberOfDots > 3)
                    _numberOfDots = 0;

                var dots = string.Empty;
                for (var i = 0; i < _numberOfDots; i++)
                    dots += ".";

                loadingText.SetText(Loading + dots);
            }
        }
    }
}