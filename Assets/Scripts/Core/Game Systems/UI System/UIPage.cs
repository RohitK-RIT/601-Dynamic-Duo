using UnityEngine;

namespace Core.Game_Systems.UI_System
{
    public abstract class UIPage : MonoBehaviour
    {
        public abstract PageID PageID { get; }

        protected UISystem UISystem { get; private set; }
        public bool Active => gameObject && gameObject.activeSelf;

        public virtual void Init(UISystem owner)
        {
            UISystem = owner;
        }

        public virtual void ShowPage()
        {
            gameObject.SetActive(true);
        }

        public virtual void HidePage()
        {
            gameObject.SetActive(false);
        }
    }
}