Friend Class THINDLADialog
    Implements IDialog

    Private player As ICharacter
    Private thindla As ICharacter

    Public Sub New(player As ICharacter, thindla As ICharacter)
        Me.player = player
        Me.thindla = thindla
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"Before you is THINDLA. He's a viking."}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {"Bye!"}
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
