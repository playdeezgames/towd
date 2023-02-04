Public Class Cell
    Implements ICell

    Private ReadOnly _worldData As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer

    Public Sub New(worldData As WorldData, mapName As String, column As Integer, row As Integer)
        _worldData = worldData
        _mapName = mapName
        _column = column
        _row = row
    End Sub

    Public Property Creature As CreatureData Implements ICell.Creature
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As CreatureData)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Trigger As EventData Implements ICell.Trigger
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As EventData)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Data As MapCellData Implements ICell.Data
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As MapCellData)
            Throw New NotImplementedException()
        End Set
    End Property
End Class
