using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Game_Systems.UI_System
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private List<UIPage> pages;

        public T GetPage<T>(PageID pageID) where T : UIPage => GetPage(pageID) as T;

        public UIPage GetPage(PageID pageID) => pages.FirstOrDefault(page => page.PageID == pageID);

        public void ShowPage(PageID pageID)
        {
            var page = GetPage(pageID);
            page.ShowPage();
        }

        public void HidePage(PageID pageID)
        {
            var page = GetPage(pageID);
            page.HidePage();
        }

        public void HideAllPages()
        {
            foreach (var page in pages.Where(page => page.Active))
                page.HidePage();
        }
    }
}