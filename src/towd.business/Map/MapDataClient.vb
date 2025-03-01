Imports towd.data

Friend Class MapDataClient
    Inherits WorldDataClient
    Protected ReadOnly Property EntityId As Integer
    Protected ReadOnly Property EntityData As MapData
        Get
            Return WorldData.Maps(EntityId)
        End Get
    End Property


    Public Sub New(worldData As data.WorldData, mapId As Integer)
        MyBase.New(worldData)
        Me.EntityId = mapId
    End Sub
End Class
