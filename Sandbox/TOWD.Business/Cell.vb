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

    Public WriteOnly Property Creature As CreatureData Implements ICell.Creature
        Set(value As CreatureData)
            _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns).Creature = value
        End Set
    End Property

    Public WriteOnly Property Trigger As EventData Implements ICell.Trigger
        Set(value As EventData)
            _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns).Trigger = value
        End Set
    End Property

    Public Property Data As MapCellData Implements ICell.Data
        Private Get
            Return _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns)
        End Get
        Set(value As MapCellData)
            _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns) = value
        End Set
    End Property

    Public Property TerrainType As TerrainType Implements ICell.TerrainType
        Get
            Return Data.TerrainType
        End Get
        Set(value As TerrainType)
            Data.TerrainType = value
        End Set
    End Property
End Class
