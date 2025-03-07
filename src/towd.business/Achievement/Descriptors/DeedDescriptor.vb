Imports towd.data

Public MustInherit Class DeedDescriptor
    Implements IDeed
    Private ReadOnly needed As HashSet(Of data.Deed)
    Sub New(deed As data.Deed, name As String, needed As data.Deed())
        Me.needed = New HashSet(Of data.Deed)(needed)
        Me.Deed = deed
        Me.Name = name
    End Sub

    Public ReadOnly Property Deed As Deed Implements IDeed.Deed

    Public ReadOnly Property Name As String Implements IDeed.Name
    Public Sub [Do](character As ICharacter) Implements IDeed.Do
        character.AppendMessage($"You have done ""{Name}""")
        OnDo(character)
    End Sub
    Protected MustOverride Sub OnDo(character As ICharacter)
    Public Overridable Function IsAvailable(character As ICharacter) As Boolean Implements IDeed.IsAvailable
        Return needed.All(Function(x) character.HasDone(x.ToDescriptor))
    End Function
    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public MustOverride Function HasDone(character As ICharacter) As Boolean Implements IDeed.HasDone
End Class
