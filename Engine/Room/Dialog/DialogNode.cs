using System;
using System.Collections.Generic;

namespace Engine
{
    public class DialogNode
    {
        public string Caption { get; set; }
        public Dictionary<int, string> Prompts { get; set; }
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

        internal void SetPrompt(int index, string prompt)
        {
            if(Prompts==null)
            {
                Prompts = new Dictionary<int, string>();
            }
            Prompts[index] = prompt;
        }

        public string GetPrompt(int index)
        {
            if(Prompts!=null)
            {
                if(Prompts.ContainsKey(index))
                {
                    return Prompts[index];
                }
            }
            return string.Empty;
        }
    }
}