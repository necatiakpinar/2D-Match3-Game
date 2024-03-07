using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public class BaseWindow : MonoBehaviour
    {
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}