﻿Imports towd.data

Friend Class ForageDeedDescriptor
    Inherits DeedDescriptor
    Private ReadOnly forageCount As Integer

    Public Sub New(deed As String, name As String, forageCount As Integer, xp As Integer, needed As String())
        MyBase.New(deed, name, xp, needed)
        Me.forageCount = forageCount
    End Sub

    Public Overrides ReadOnly Property Description As String
        Get
            Return $"Successfully forage {forageCount} times."
        End Get
    End Property

    Protected Overrides Sub OnDo(character As ICharacter)
    End Sub

    Public Overrides Function IsAvailable(character As ICharacter) As Boolean
        Return MyBase.IsAvailable(character)
    End Function

    Public Overrides Function HasDone(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.ForagingCounter) >= forageCount
    End Function
End Class
