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
End Class
