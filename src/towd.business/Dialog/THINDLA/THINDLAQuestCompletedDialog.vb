Friend Class THINDLAQuestCompletedDialog
    Implements IDialog

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
            Return {"THINDLA says: ""Thanks again! You sure saved my ass!"""}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {"No worries"}
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
