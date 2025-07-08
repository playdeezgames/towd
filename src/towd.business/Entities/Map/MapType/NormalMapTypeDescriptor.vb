Imports towd.data
Friend Class NormalMapTypeDescriptor
    Inherits MapTypeDescriptor

    Public Sub New()
        MyBase.New(MapType.Normal, "Normal", 1, 9, 9, data.LocationType.Grass)
    End Sub

    Private ReadOnly terrainTable As IReadOnlyDictionary(Of LocationType, Integer) =
        New Dictionary(Of LocationType, Integer) From
        {
            {data.LocationType.Grass, 15},
            {data.LocationType.Pine, 10},
            {data.LocationType.Rock, 5},
            {data.LocationType.Pond, 3}
        }

    Public Overrides Sub Initialize(map As IMap)
        For Each column In Enumerable.Range(0, map.Columns)
            For Each row In Enumerable.Range(0, map.Rows)
                map.GetLocation(column, row).EntityType = terrainTable.Generate().ToDescriptor
            Next
        Next
        For Each characterType In CharacterTypes.Descriptors.Values
            For Each index In Enumerable.Range(0, characterType.GetSpawnCount(map))
                characterType.Spawn(map)
            Next
        Next
    End Sub

    Protected Overrides Sub OnAdvanceTime(amount As Integer)
    End Sub
End Class
