using System.Collections.Generic;

namespace Engine
{
    public class DialogChoice
    {
        public int Order { get; set; }
        public string Option { get; set; }
        public List<DialogChoiceEvent> Events { get; set; }
    }
}