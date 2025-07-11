Friend Class THINDLAQuestDeliveryDialog
    Implements IDialog

    Const COMPLETE_TEXT = "Yer welcome!"

    Private player As ICharacter
    Private thindla As ICharacter
    Private root As IDialog

    Public Sub New(player As ICharacter, thindla As ICharacter, root As IDialog)
        Me.player = player
        Me.thindla = thindla
        Me.root = root
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"THINDLA says: ""You found my ass! Thank you!"""}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {COMPLETE_TEXT}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "THINDLA the Viking"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        player.SetTag(THINDLADialog.COMPLETED_TAG, True)
        Return Nothing
    End Function
End Class
