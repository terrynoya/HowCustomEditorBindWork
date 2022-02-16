using System;

namespace HowOdinCustomEditorWork
{
    public class ButtonAttribute:Attribute
    {
        public string Text;

        public ButtonAttribute(string text)
        {
            Text = text;
        }
    }    
}
