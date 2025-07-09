Friend Class THINDLAQuestAcceptedDialog
    Implements IDialog

    Private player As ICharacter
    Private thindla As ICharacter
    Private root As THINDLADialog

    Public Sub New(player As ICharacter, thindla As ICharacter, root As THINDLADialog)
        Me.player = player
        Me.thindla = thindla
        Me.root = root
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"THINDLA says: ""Please! Find my hairy ass! I miss him!"""}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {"I'm trying...."}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "THINDLA the Viking"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Return Nothing
    End Function
End Class
