namespace Engine
{
    public class DialogChoiceEvent
    {
        public DialogEventType EventType { get; set; }
        public int Order { get; set; }
        public string Shoppe { get; set; }
        public string Flag { get; set; }
        public string State { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int DestinationColumn { get; set; }
        public int DestinationRow { get; set; }
        public string DestinationRoom { get; set; }
        public string Prompt { get; set; }
        public string Counter { get; set; }
        public int Value { get; set; }
    }
}