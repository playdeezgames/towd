namespace Engine
{
    public class CreatureDeathEvent
    {
        public int Order { get; set; }
        public DeathEventType EventType { get; set; }
        public string Counter { get; set; }
    }
}