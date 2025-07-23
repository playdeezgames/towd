Friend Class MasterKnapperQuestCompletedDialog
    Implements IDialog

    Private ReadOnly player As ICharacter
    Private ReadOnly knapper As ICharacter
    Private ReadOnly root As IDialog
    Const BYE_TEXT = "Thank you."

    Public Sub New(player As ICharacter, knapper As ICharacter, root As IDialog)
        Me.player = player
        Me.knapper = knapper
        Me.root = root
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"No more training do you require.", "A knapper, you are."}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {BYE_TEXT}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "Master Knapper"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Return Nothing
    End Function
End Class
