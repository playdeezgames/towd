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

    Public Property OnInteract As IEvent Implements ICreature.OnInteract
        Get
            If Not Data.OnInteract.HasValue Then
                Return Nothing
            End If
            Return New EventInstance(_worldData, Data.OnInteract.Value)
        End Get
        Set(value As IEvent)
            If value Is Nothing Then
                Data.OnInteract = Nothing
                Return
            End If
            Data.OnInteract = value.Index
        End Set
    End Property
End Class
