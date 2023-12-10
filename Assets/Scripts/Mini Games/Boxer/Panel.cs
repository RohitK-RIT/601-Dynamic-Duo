using UnityEngine;
using UnityEngine.UI;

namespace Mini_Games.Boxer
{
    public class Panel
    {
        private readonly Image _highlight;
        private readonly Image _smallPanelImage;
        private readonly Image _bigPanelImage;

        public GameObject GameObject { get; }
        public Transform Transform { get; }

        public bool IsEmpty => _smallPanelImage.color == Color.clear && _bigPanelImage.color == Color.clear;

        public Panel(Transform panel)
        {
            Transform = panel;
            GameObject = panel.gameObject;

            _highlight = panel.GetChild(0).GetComponent<Image>();
            _bigPanelImage = panel.GetChild(2).GetComponent<Image>();
            _smallPanelImage = panel.GetChild(3).GetComponent<Image>();

            SetHighlightColor(Color.clear);
        }

        public void SetHighlightColor(Color color)
        {
            _highlight.color = color;
        }

        public Image this[int i] => i == 0 ? _bigPanelImage : _smallPanelImage;
    }
}