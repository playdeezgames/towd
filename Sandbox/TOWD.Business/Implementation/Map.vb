Public Class Map
    Implements IMap
    Private ReadOnly _worldData As WorldData
    Private ReadOnly _mapName As String
    Friend Sub New(worldData As WorldData, mapName As String)
        _worldData = worldData
        _mapName = mapName
    End Sub

    Private ReadOnly Property Data As MapData
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

    Public ReadOnly Property Cells As IEnumerable(Of ICell) Implements IMap.Cells
        Get
            Dim result As New List(Of ICell)
            For row = 0 To Rows - 1
                For column = 0 To Columns - 1
                    result.Add(GetCell(column, row))
                Next
            Next
            Return result
        End Get
    End Property

    Public ReadOnly Property World As IWorld Implements IMap.World
        Get
            Return New World(_worldData)
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IMap.Name
        Get
            Return _mapName
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
