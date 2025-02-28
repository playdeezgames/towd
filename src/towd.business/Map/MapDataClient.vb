Imports towd.data

Friend Class MapDataClient
    Inherits WorldDataClient
    Protected ReadOnly Property MapId As Integer
    Protected ReadOnly Property MapData As MapData
        Get
            Return WorldData.Maps(MapId)
        End Get
    End Property


    Public Sub New(worldData As data.WorldData, mapId As Integer)
        MyBase.New(worldData)
        Me.MapId = mapId
    End Sub
End Class
