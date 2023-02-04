﻿Friend Class Creature
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

    Public Property CreatureType As CreatureType Implements ICreature.CreatureType
        Get
            Return CreatureType.Dude
        End Get
        Set(value As CreatureType)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property OnInteract As EventData Implements ICreature.OnInteract
        Get
            Return Nothing
        End Get
        Set(value As EventData)
            Throw New NotImplementedException()
        End Set
    End Property
End Class