Public Class MapCellData
    Public Property TerrainType As TerrainType
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property Trigger As Integer?
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property Creature As CreatureData
End Class
