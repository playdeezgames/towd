﻿Friend Class CraftDeedDescriptor
    Inherits DeedDescriptor
    Implements IDeed

    Private ReadOnly craftCount As Integer

    Public Sub New(deed As String, name As String, craftCount As Integer, xp As Integer, needed() As String)
        MyBase.New(deed, name, xp, needed)
        Me.craftCount = craftCount
    End Sub

    Public Overrides ReadOnly Property Description As String
        Get
            Return $"Successfully craft {craftCount} times."
        End Get
    End Property

    Protected Overrides Sub OnDo(character As ICharacter)
    End Sub

    Public Overrides Function HasDone(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.CraftCounter) >= craftCount
    End Function
End Class
