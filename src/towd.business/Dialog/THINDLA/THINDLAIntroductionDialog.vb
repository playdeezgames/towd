Friend Class THINDLAIntroductionDialog
    Implements IDialog

    Private ReadOnly player As ICharacter
    Private ReadOnly root As IDialog
    Const BYE_TEXT = "..."
    Const ASS_TEXT = "Hairy ass?"

    Public Sub New(player As ICharacter, root As IDialog)
        Me.player = player
        Me.root = root
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {
                "You see a viking.",
                "He says ""Hello, my name is THINDLA. Have you seen my hairy ass?"""}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {BYE_TEXT, ASS_TEXT}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "THINDLA the Viking"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Select Case choice
            Case BYE_TEXT
                Return Nothing
            Case ASS_TEXT
                player.SetTag(THINDLADialog.INTRODUCTION_TAG, True)
                Return root
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
