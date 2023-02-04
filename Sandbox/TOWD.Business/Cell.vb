Public Class Cell
    Implements ICell

    Private ReadOnly _worldData As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer

    Friend Sub New(worldData As WorldData, mapName As String, column As Integer, row As Integer)
        _worldData = worldData
        _mapName = mapName
        _column = column
        _row = row
    End Sub

    Public Function CreateCreature() As ICreature Implements ICell.CreateCreature
        Data.Creature = New CreatureData
        Return Creature
    End Function

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
            If Data.Creature Is Nothing Then
                Return Nothing
            End If
            Return New Creature(_worldData, _mapName, _column, _row)
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
