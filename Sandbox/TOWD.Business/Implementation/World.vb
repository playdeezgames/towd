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

    Public ReadOnly Property Avatar As IAvatar Implements IWorld.Avatar
        Get
            If _worldData.Avatar Is Nothing Then
                Return Nothing
            End If
            Return New Avatar(_worldData)
        End Get
    End Property

    Public Sub CreateAvatar(name As String, column As Integer, row As Integer) Implements IWorld.CreateAvatar
        _worldData.Avatar = New AvatarData With
            {
                .MapColumn = column,
                .MapName = name,
                .MapRow = row
            }
    End Sub

    Public Function CreateMap(name As String) As IMap Implements IWorld.CreateMap
        _worldData.Maps(name) = New MapData
        Return New Map(_worldData, name)
    End Function

    Public Function CreateEvent() As IEvent Implements IWorld.CreateEvent
        Dim index = _worldData.Events.Count
        _worldData.Events.Add(New EventData)
        Return New EventInstance(_worldData, index)
    End Function

    Public Function Serialize() As String Implements IWorld.Serialize
        Return JsonSerializer.Serialize(_worldData)
    End Function
End Class
