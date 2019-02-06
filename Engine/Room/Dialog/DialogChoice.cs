using System;
using System.Collections.Generic;

namespace Engine
{
    public class DialogChoice
    {
        public int Order { get; set; }
        public string Option { get; set; }
        public List<DialogChoiceEvent> Events { get; set; }

        internal void AddEvent(DialogChoiceEvent choiceEvent)
        {
            if(Events==null)
            {
                Events = new List<DialogChoiceEvent>();
            }
            Events.Add(choiceEvent);
        }
    }
}