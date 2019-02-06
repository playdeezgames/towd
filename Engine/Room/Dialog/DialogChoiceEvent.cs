namespace Engine
{
    public class DialogChoiceEvent
    {
        public DialogEventType EventType { get; set; }
        public int Order { get; set; }
        public string Shoppe { get; set; }
    }
}