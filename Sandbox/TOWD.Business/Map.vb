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

    Public ReadOnly Property Columns As Integer Implements IMap.Columns
        Get
            Return Data.Columns
        End Get
    End Property

    Public ReadOnly Property Rows As Integer Implements IMap.Rows
        Get
            Return Data.Rows
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

    Public Function GetCell(column As Integer, row As Integer) As ICell Implements IMap.GetCell
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return New Cell(_worldData, _mapName, column, row)
    End Function
End Class
