Public Interface ICell
    ReadOnly Property Creature As ICreature
    Property Trigger As IEvent
    Property TerrainType As TerrainType
    Function CreateCreature() As ICreature
End Interface
