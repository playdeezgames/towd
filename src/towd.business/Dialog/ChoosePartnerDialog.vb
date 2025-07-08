Friend Class ChoosePartnerDialog
    Implements IDialog
    Const NEVER_MIND_TEXT = "Never Mind"

    Private character As ICharacter

    Public Sub New(character As ICharacter)
        Me.character = character
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"With whom or what would you like to dialog?"}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {NEVER_MIND_TEXT}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "Dialog With..."
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Return Nothing
    End Function
End Class
