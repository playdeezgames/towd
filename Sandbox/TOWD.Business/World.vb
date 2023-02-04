Public Class World
    Implements IWorld
    Private ReadOnly _worldData As New WorldData

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
        Return New EventInstance()
    End Function
End Class
