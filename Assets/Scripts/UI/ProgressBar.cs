using UnityEngine;
using UnityEngine.UI;

namespace MyKitchenChaos
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

        private void Awake()
        {
            SetProgressValue(0f);
        }
        public void SetProgressValue(float value)
        {
            if (progressBar != null)
            {
                progressBar.value = value;
            }
        }
    }
}
