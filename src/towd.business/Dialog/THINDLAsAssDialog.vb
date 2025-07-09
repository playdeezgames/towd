Friend Class THINDLAsAssDialog
    Implements IDialog

    Private character As ICharacter
    Private otherCharacter As ICharacter

    Public Sub New(character As ICharacter, otherCharacter As ICharacter)
        Me.character = character
        Me.otherCharacter = otherCharacter
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"Before you stands the hairiest ass you have ever seen."}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {"Bye!"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "Hairy Ass"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Return Nothing
    End Function
End Class
