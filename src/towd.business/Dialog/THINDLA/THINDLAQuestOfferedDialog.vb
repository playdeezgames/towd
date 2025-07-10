Friend Class THINDLAQuestOfferedDialog
    Implements IDialog

    Private player As ICharacter
    Private root As THINDLADialog
    Const ACCEPT_TEXT = "Sure, I'll find yer hairy ass."
    Const LATER_TEXT = "...Maybe later."

    Public Sub New(player As ICharacter, root As THINDLADialog)
        Me.player = player
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
                player.AppendMessage("THINDLA hands you a carrot.")
                player.AddItem(player.World.CreateItem(ItemType.Carrot.ToItemTypeDescriptor))
                player.SetTag(THINDLADialog.ACCEPTED_TAG, True)
                Return Nothing
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
