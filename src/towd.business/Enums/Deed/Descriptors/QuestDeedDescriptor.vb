Friend Class QuestDeedDescriptor
    Implements IDeed

    Sub New(deed As String, name As String, description As String)
        Me.Deed = deed
        Me.Name = name
        Me.Description = description
    End Sub

    Public ReadOnly Property Deed As String Implements IDeed.Deed

    Public ReadOnly Property Name As String Implements IDeed.Name

    Public ReadOnly Property Description As String Implements IDeed.Description

    Public ReadOnly Property XP As Integer Implements IDeed.XP
        Get
            Return 1
        End Get
    End Property

    Public Sub [Do](character As ICharacter) Implements IDeed.Do
    End Sub

    Public Function IsAvailable(character As ICharacter) As Boolean Implements IDeed.IsAvailable
        Return False
    End Function

    Public Function HasDone(character As ICharacter) As Boolean Implements IDeed.HasDone
        Return character.HasTag(MasterKnapperDialog.COMPLETED_TAG)
    End Function
End Class
