using System;
using UnityEngine;

namespace UI.PopupMessageHelper
{
    public interface IContentProvider
    {
        string Tittle { get; set; }
        string Content { get; set; }
        Action<string> OnTitleChange { get; set; }
        Action<string> OnContentChange { get; set; }
    }
}