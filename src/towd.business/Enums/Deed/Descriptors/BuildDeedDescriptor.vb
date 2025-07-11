﻿Friend Class BuildDeedDescriptor
    Inherits DeedDescriptor
    Implements IDeed

    Private ReadOnly buildCount As Integer

    Public Sub New(deed As String, name As String, performCount As Integer, xp As Integer, needed() As String)
        MyBase.New(deed, name, xp, needed)
        Me.buildCount = performCount
    End Sub

    Public Overrides ReadOnly Property Description As String
        Get
            Return $"Successfully build {buildCount} times."
        End Get
    End Property

    Protected Overrides Sub OnDo(character As ICharacter)
    End Sub

    Public Overrides Function HasDone(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.BuildCounter) >= buildCount
    End Function
End Class
