using System;
using System.Collections.Generic;

namespace Engine
{
    public class DialogChoice
    {
        public int Order { get; set; }
        public string Option { get; set; }
        public string OptionText { get; set; }
        public List<DialogChoiceEvent> Events { get; set; }
        public List<DialogChoiceCondition> Conditions { get; set; }

        internal void AddEvent(DialogChoiceEvent choiceEvent)
        {
            if(Events==null)
            {
                Events = new List<DialogChoiceEvent>();
            }
            Events.Add(choiceEvent);
        }

        internal void AddCondition(DialogChoiceCondition condition)
        {
            if(Conditions==null)
            {
                Conditions = new List<DialogChoiceCondition>();
            }
            Conditions.Add(condition);
        }

        public IEnumerable<DialogChoiceCondition> GetConditions()
        {
            if(Conditions==null)
            {
                return new DialogChoiceCondition[0];
            }
            else
            {
                return Conditions;
            }
        }

        public IEnumerable<DialogChoiceEvent> GetEvents()
        {
            if (Events == null)
            {
                return new DialogChoiceEvent[0];
            }
            else
            {
                return Events;
            }
        }
    }
}