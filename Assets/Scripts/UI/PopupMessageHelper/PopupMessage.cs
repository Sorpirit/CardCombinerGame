using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.PopupMessageHelper;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{

    [SerializeField] private TMP_Text titleLb;
    [SerializeField] private TMP_Text contentLb;
    [SerializeField] private CanvasGroup massageCanvas;

    private IPopupMassageEvents _events;
    private IContentProvider _contentProvider;

    public Action<PopupMessage> OnPopup
    {
        get => _events.OnCloseComplete;
        set => _events.OnCloseComplete = value;
    }
    public Action<PopupMessage> OnClose
    {
        get => _events.OnCloseComplete;
        set => _events.OnCloseComplete = value;
    }
    
    public void Init(IPopupMassageEvents eventsStrategy,IContentProvider contentProvider)
    {
        _events = eventsStrategy;
        _contentProvider = contentProvider;

        _contentProvider.OnTitleChange += (str) => Tittle = str;
        _contentProvider.OnContentChange += (str) => Content = str;

        Tittle = _contentProvider.Tittle;
        Content = _contentProvider.Content;
    }
    
    public RectTransform Container { get; private set; }
    public string Tittle
    {
        get => titleLb.text;
        set => titleLb.text = value;
    }
    public string Content
    {
        get => contentLb.text;
        set => contentLb.text = value;
    }

    private void Awake()
    {
        Container = GetComponent<RectTransform>();
    }

    public void Popup()
    {
        _events.Popup(this);
    }

    public void Close()
    {
        _events.Close(this);
    }
}
