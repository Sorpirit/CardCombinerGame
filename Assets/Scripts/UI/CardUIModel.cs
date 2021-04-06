using Models;
using UnityEngine;

namespace UI
{
    public struct CardUIModel
    {
        private Sprite _image;
        private Sprite _icon;
        private string _lable;
        private Card _parent;

        public static CardUIModel Empty => Card.Empty.ExportUI();
        
        public Sprite Image => _image;
        public string Lable => _lable;
        public Card Parent => _parent;

        public Sprite Icon => _icon;

        public CardUIModel(Sprite image, Sprite icon, string label, Card parent)
        {
            _image = image;
            _parent = parent;
            _icon = icon;
            _lable = label;
        }

        public bool Equals(CardUIModel other)
        {
            return Equals(_image, other._image) && _lable == other._lable && Equals(_parent, other._parent);
        }

        public override bool Equals(object obj)
        {
            return obj is CardUIModel other && Equals(other);
        }
    }
}