Friend Class THINDLAQuestOfferedDialog
    Implements IDialog

    Private player As ICharacter
    Private thindla As ICharacter
    Private root As THINDLADialog
    Const ACCEPT_TEXT = "Sure, I'll find yer hairy ass."
    Const LATER_TEXT = "...Maybe later."

    Public Sub New(player As ICharacter, thindla As ICharacter, root As THINDLADialog)
        Me.player = player
        Me.thindla = thindla
        Me.root = root
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {
                "THINDLA says: ""My ass! He is lost, and I cannot find him. Please, take this carrot and lead him back here, would you?"""}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {LATER_TEXT, ACCEPT_TEXT}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "THINDLA the Viking"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Select Case choice
            Case LATER_TEXT
                Return Nothing
            Case ACCEPT_TEXT
                player.AddMessage("THINDLA hands you a carrot.")
                player.SetTag(THINDLADialog.ACCEPTED_TAG, True)
                Return root
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
