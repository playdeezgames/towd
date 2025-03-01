Friend Class Map
    Inherits MapDataClient
    Implements IMap

    Public Sub New(worldData As data.WorldData, mapId As Integer)
        MyBase.New(worldData, mapId)
    End Sub

    Public ReadOnly Property Id As Integer Implements IMap.Id
        Get
            Return MapId
        End Get
    End Property

    Public ReadOnly Property Columns As Integer Implements IMap.Columns
        Get
            Return MapData.Columns
        End Get
    End Property

    Public ReadOnly Property Rows As Integer Implements IMap.Rows
        Get
            Return MapData.Rows
        End Get
    End Property

    Public Property EntityType As IMapType Implements IMap.EntityType
        Get
            Return MapData.MapType.ToDescriptor
        End Get
        Set(value As IMapType)
            Throw New NotImplementedException
        End Set
    End Property

    Public ReadOnly Property Locations As IEnumerable(Of ILocation) Implements IMap.Locations
        Get
            Return MapData.Locations.Select(Function(x) New Location(WorldData, x))
        End Get
    End Property

    Public ReadOnly Property World As IWorld Implements IMap.World
        Get
            Return New World(WorldData)
        End Get
    End Property

    Public Sub AdvanceTime(amount As Integer) Implements IMap.AdvanceTime
        EntityType.AdvanceTime(Me, amount)
    End Sub

    Public Function GetLocation(column As Integer, row As Integer) As ILocation Implements IMap.GetLocation
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return New Location(WorldData, MapData.Locations(row * Columns + column))
    End Function
End Class
