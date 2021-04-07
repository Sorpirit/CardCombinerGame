using System;

namespace UI.PopupMessageHelper
{
    public class SimpleContentProvider : IContentProvider
    {
        public string Tittle { get; set; }
        public string Content { get; set; }
        
        public Action<string> OnTitleChange { get; set; }
        public Action<string> OnContentChange { get; set; }

        public SimpleContentProvider(string tittle, string content)
        {
            Tittle = tittle;
            Content = content;
        }
    }
}