Imports towd.data

Public Interface IMapType
    ReadOnly Property MapType As MapType
    ReadOnly Property Name As String
    ReadOnly Property LocationType As ILocationType
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    ReadOnly Property SpawnCount As Integer
    Sub Initialize(map As IMap)
    Sub AdvanceTime(map As IMap, amount As Integer)
End Interface
