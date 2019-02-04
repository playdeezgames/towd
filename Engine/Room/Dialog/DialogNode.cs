using System;
using System.Collections.Generic;

namespace Engine
{
    public class DialogNode
    {
        public string Prompt { get; set; }
        public Dictionary<string, DialogChoice> Choices { get; set; }

        internal DialogChoice GetChoice(string option)
        {
            if (Choices == null)
            {
                Choices = new Dictionary<string, DialogChoice>();
            }
            if (!Choices.ContainsKey(option))
            {
                Choices[option] = new DialogChoice();
            }
            return Choices[option];
        }
    }
}