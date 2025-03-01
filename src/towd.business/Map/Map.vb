Friend Class Map
    Inherits MapDataClient
    Implements IMap
    Public Sub New(worldData As data.WorldData, mapId As Integer)
        MyBase.New(worldData, mapId)
    End Sub
    Public ReadOnly Property Columns As Integer Implements IMap.Columns
        Get
            Return EntityData.Columns
        End Get
    End Property
    Public ReadOnly Property Rows As Integer Implements IMap.Rows
        Get
            Return EntityData.Rows
        End Get
    End Property
    Public ReadOnly Property Locations As IEnumerable(Of ILocation) Implements IMap.Locations
        Get
            Return EntityData.Locations.Select(Function(x) New Location(WorldData, x))
        End Get
    End Property
    Public Overrides Property EntityType As IMapType
        Get
            Return EntityData.MapType.ToDescriptor
        End Get
        Set(value As IMapType)
            EntityData.MapType = value.MapType
            value.Initialize(Me)
        End Set
    End Property
    Public Overrides Sub AdvanceTime(amount As Integer)
        EntityType.AdvanceTime(Me, amount)
    End Sub
    Public Function GetLocation(column As Integer, row As Integer) As ILocation Implements IMap.GetLocation
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return New Location(WorldData, EntityData.Locations(row * Columns + column))
    End Function
End Class
