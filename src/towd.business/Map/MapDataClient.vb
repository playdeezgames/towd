Imports towd.data

Friend MustInherit Class MapDataClient
    Inherits Entity(Of IMapType, MapData)
    Protected ReadOnly Property EntityId As Integer
    Public Sub New(worldData As data.WorldData, mapId As Integer)
        MyBase.New(worldData, mapId)
        Me.EntityId = mapId
    End Sub
    Protected Overrides ReadOnly Property EntityData As MapData
        Get
            Return WorldData.Maps(EntityId)
        End Get
    End Property
End Class
