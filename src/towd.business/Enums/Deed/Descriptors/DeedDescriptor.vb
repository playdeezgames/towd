Imports towd.data

Public MustInherit Class DeedDescriptor
    Implements IDeed
    Private ReadOnly needed As HashSet(Of data.Deed)
    Sub New(deed As data.Deed, name As String, xp As Integer, needed As data.Deed())
        Me.needed = New HashSet(Of data.Deed)(needed)
        Me.Deed = deed
        Me.Name = name
        Me.XP = xp
    End Sub

    Public ReadOnly Property Deed As Deed Implements IDeed.Deed

    Public ReadOnly Property Name As String Implements IDeed.Name
    Public MustOverride ReadOnly Property Description As String Implements IDeed.Description

    Public ReadOnly Property XP As Integer Implements IDeed.XP

    Public Sub [Do](character As ICharacter) Implements IDeed.Do
        character.AppendMessage($"You have done ""{Name}""")
        character.ReportChangeStatistic(StatisticType.XP, XP)
        OnDo(character)
    End Sub
    Protected MustOverride Sub OnDo(character As ICharacter)
    Public Overridable Function IsAvailable(character As ICharacter) As Boolean Implements IDeed.IsAvailable
        Return needed.All(Function(x) character.HasDone(x.ToDeedDescriptor))
    End Function
    Public Overrides Function ToString() As String
        Return $"{Name}(XP: {XP})"
    End Function

    Public MustOverride Function HasDone(character As ICharacter) As Boolean Implements IDeed.HasDone
End Class
