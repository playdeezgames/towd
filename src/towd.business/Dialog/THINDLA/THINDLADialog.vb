Friend Class THINDLADialog
    Inherits StatefulDialog

    Friend Const INTRODUCTION_TAG = "thindla-introduction"
    Friend Const ACCEPTED_TAG = "thindla-quest-accepted"
    Friend Const COMPLETED_TAG = "thindla-quest-completed"
    Private ReadOnly player As ICharacter
    Private ReadOnly thindla As ICharacter
    Protected Overrides Function CreateSubdialog() As IDialog
        If player.HasTag(INTRODUCTION_TAG) Then
            If player.HasTag(ACCEPTED_TAG) Then
                Return New THINDLAQuestAcceptedDialog(player, thindla, Me)
            End If
            Return New THINDLAQuestOfferedDialog(player, Me)
        End If
        Return New THINDLAIntroductionDialog(player, Me)
    End Function
    Public Sub New(player As ICharacter, thindla As ICharacter)
        Me.player = player
        Me.thindla = thindla
    End Sub
End Class
