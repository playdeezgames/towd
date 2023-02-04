Public Interface ICell
    Property Creature As ICreature
    Property Trigger As IEvent
    WriteOnly Property CreatureData As CreatureData
    WriteOnly Property TriggerData As EventData
    Property TerrainType As TerrainType
End Interface
