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

    Public WriteOnly Property CreatureData As CreatureData Implements ICell.CreatureData
        Set(value As CreatureData)
            _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns).Creature = value
        End Set
    End Property

    Public WriteOnly Property TriggerData As EventData Implements ICell.TriggerData
        Set(value As EventData)
            _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns).Trigger = value
        End Set
    End Property

    Private ReadOnly Property Data As MapCellData
        Get
            Return _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns)
        End Get
    End Property

    Public Property TerrainType As TerrainType Implements ICell.TerrainType
        Get
            Return Data.TerrainType
        End Get
        Set(value As TerrainType)
            Data.TerrainType = value
        End Set
    End Property

    Public Property Creature As ICreature Implements ICell.Creature
        Get
            Return Nothing
        End Get
        Set(value As ICreature)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Trigger As IEvent Implements ICell.Trigger
        Get
            Return Nothing
        End Get
        Set(value As IEvent)
            Throw New NotImplementedException()
        End Set
    End Property
End Class
