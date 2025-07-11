Friend Class THINDLAsAssIntroductionDialog
    Implements IDialog

    Private ReadOnly player As ICharacter
    Private ReadOnly ass As ICharacter
    Private ReadOnly root As IDialog
    Const BYE_TEXT = "Leave the ass alone"
    Const CARROT_TEXT = "Give the ass a carrot"

    Public Sub New(player As ICharacter, ass As ICharacter, root As IDialog)
        Me.player = player
        Me.root = root
        Me.ass = ass
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Dim results As New List(Of String) From {
                "You see an ass. It is very hairy.",
                "It is the hairiest ass you have every seen."}
            If ass.HasTag(THINDLAsAssDialog.ASS_FED_TAG) Then
                results.Add("He is following you, hoping for another carrot.")
            End If
            Return results
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Dim results As New List(Of String) From
                {
                    BYE_TEXT
                }
            If Not ass.HasTag(THINDLAsAssDialog.ASS_FED_TAG) AndAlso player.GetCountOfItemType(ItemType.Carrot.ToItemTypeDescriptor) > 0 Then
                results.Add(CARROT_TEXT)
            End If
            Return results.ToArray
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "THINDLA's Hairy Ass"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Select Case choice
            Case BYE_TEXT
                Return Nothing
            Case CARROT_TEXT
                Return GiveAssCarrot()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function GiveAssCarrot() As IDialog
        ass.SetTag(THINDLAsAssDialog.ASS_FED_TAG, True)
        player.RemoveItemOfType(ItemType.Carrot.ToItemTypeDescriptor)
        player.AppendMessage($"You give the ass a carrot.", "The ass starts to follow you.")
        Return Nothing
    End Function
End Class
