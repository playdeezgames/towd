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

    Public Sub SetSize(columns As Integer, rows As Integer) Implements IMap.SetSize
        Data.Columns = columns
        Data.Rows = rows
        Data.Cells.Clear()
        While Data.Cells.Count < Data.Columns * Data.Rows
            Data.Cells.Add(New MapCellData)
        End While
    End Sub
End Class
