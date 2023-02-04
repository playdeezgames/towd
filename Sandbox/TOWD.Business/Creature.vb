Friend Class Creature
    Implements ICreature

    Private _worldData As WorldData
    Private _mapName As String
    Private _column As Integer
    Private _row As Integer

    Friend Sub New(worldData As WorldData, mapName As String, column As Integer, row As Integer)
        _worldData = worldData
        _mapName = mapName
        _column = column
        _row = row
    End Sub

    Private ReadOnly Property Data As CreatureData
        Get
            Return _worldData.Maps(_mapName).Cells(_column + _row * _worldData.Maps(_mapName).Columns).Creature
        End Get
    End Property

    Public Property CreatureType As CreatureType Implements ICreature.CreatureType
        Get
            Return Data.CreatureType
        End Get
        Set(value As CreatureType)
            Data.CreatureType = value
        End Set
    End Property

    Public Property OnInteract As EventData Implements ICreature.OnInteract
        Get
            Return Data.OnInteract
        End Get
        Set(value As EventData)
            Data.OnInteract = value
        End Set
    End Property
End Class
