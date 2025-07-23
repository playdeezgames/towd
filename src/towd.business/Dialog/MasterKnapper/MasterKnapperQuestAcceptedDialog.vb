Friend Class MasterKnapperQuestAcceptedDialog
    Implements IDialog

    Private ReadOnly player As ICharacter
    Private ReadOnly knapper As ICharacter
    Private ReadOnly root As IDialog
    Const COMPLETE_TEXT = "Here you go... or, is it ""Go here you?"""
    Const INCOMPLETE_TEXT = "I'll return. I promise."

    Public Sub New(player As ICharacter, knapper As ICharacter, root As IDialog)
        Me.player = player
        Me.knapper = knapper
        Me.root = root
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {
                "If learning the ways of knapping desire you, a hammer and a sharp rock bring me."
                }
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            If player.GetCountOfItemType(ItemType.Hammer.ToItemTypeDescriptor) > 0 AndAlso player.GetCountOfItemType(ItemType.SharpRock.ToItemTypeDescriptor) > 0 Then
                Return {
                    COMPLETE_TEXT
                    }
            End If
            Return {
                INCOMPLETE_TEXT
                }
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "Master Knapper"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Select Case choice
            Case INCOMPLETE_TEXT
                Return Nothing
            Case COMPLETE_TEXT
                player.AppendMessage(
                    "The master knapper demonstrates the ancient skill of knapping.",
                    "You think you get the fundamentals, but will need some practice.")
                player.SetTag(MasterKnapperDialog.COMPLETED_TAG, True)
                player.SetDone(Deed.CompleteKnapperQuest.ToDeedDescriptor)
                Return root
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
