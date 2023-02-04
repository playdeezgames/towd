Public Class World
    Implements IWorld
    Private ReadOnly _worldData As WorldData
    Public Sub New()
        _worldData = New WorldData
    End Sub
    Friend Sub New(worldData As WorldData)
        _worldData = worldData
    End Sub

    Public ReadOnly Property Maps As IEnumerable(Of IMap) Implements IWorld.Maps
        Get
            Return _worldData.Maps.Select(Function(x) New Map(_worldData, x.Key))
        End Get
    End Property

    Public Function CreateMap(name As String) As IMap Implements IWorld.CreateMap
        _worldData.Maps(name) = New MapData
        Return New Map(_worldData, name)
    End Function

    Public Function CreateEvent() As IEvent Implements IWorld.CreateEvent
        Dim index = _worldData.Events.Count
        _worldData.Events.Add(New EventData)
        Return New EventInstance(_worldData, index)
    End Function
End Class
