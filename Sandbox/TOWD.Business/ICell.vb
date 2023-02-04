Public Interface ICell
    WriteOnly Property Creature As CreatureData
    WriteOnly Property Trigger As EventData
    WriteOnly Property Data As MapCellData
    Property TerrainType As TerrainType
End Interface
