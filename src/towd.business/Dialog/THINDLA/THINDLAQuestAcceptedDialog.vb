Friend Class THINDLAQuestAcceptedDialog
    Implements IDialog
    Const NEED_CARROT = "I need another carrot..."
    Const BYE = "I'm trying...."

    Private ReadOnly player As ICharacter

    Public Sub New(player As ICharacter)
        Me.player = player
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {"THINDLA says: ""Please! Find my hairy ass! I miss him!"""}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            If player.GetCountOfItemType(ItemType.Carrot.ToItemTypeDescriptor) = 0 Then
                Return {NEED_CARROT}
            End If
            Return {BYE}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "THINDLA the Viking"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Select Case choice
            Case NEED_CARROT
                Return NeedsCarrot()
            Case BYE
                Return Nothing
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function NeedsCarrot() As IDialog
        'TODO: check THINDLA's patience!
        player.AppendMessage("THINDLA gives you another carrot.")
        player.AddItem(player.World.CreateItem(ItemType.Carrot.ToItemTypeDescriptor))
        Return Nothing
    End Function
End Class
