Public Class Map
    Implements IMap
    Private ReadOnly _worldData As WorldData
    Private ReadOnly _mapName As String
    Sub New(worldData As WorldData, mapName As String)
        _worldData = worldData
        _mapName = mapName
    End Sub

    Public ReadOnly Property Data As MapData Implements IMap.Data
        Get
            Return _worldData.Maps(_mapName)
        End Get
    End Property
End Class
