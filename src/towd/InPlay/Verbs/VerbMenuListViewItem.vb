Imports towd.business

Public Class VerbMenuListViewItem
    Sub New(verbType As IVerbType, character As ICharacter)
        Me.VerbType = verbType
        Me.Character = character
    End Sub

    Public ReadOnly Property VerbType As IVerbType
    Private ReadOnly Character As ICharacter

    Public Overrides Function ToString() As String
        Dim performCount = VerbType.GetPerformCount(Character)
        Return $"{VerbType}{If(performCount.HasValue, $"({performCount.Value} remaining)", "")}"
    End Function
End Class
