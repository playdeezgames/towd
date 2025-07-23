Friend Class MasterKnapperDialog
    Inherits StatefulDialog

    Private ReadOnly player As ICharacter
    Private ReadOnly knapper As ICharacter
    Friend Const ACCEPTED_TAG = "master-knapper-quest-accepted"
    Friend Const COMPLETED_TAG = "master-knapper-quest-completed"

    Public Sub New(player As ICharacter, knapper As ICharacter)
        Me.player = player
        Me.knapper = knapper
    End Sub

    Protected Overrides Function CreateSubdialog() As IDialog
        If player.HasTag(COMPLETED_TAG) Then
            Return New MasterKnapperQuestCompletedDialog(player, knapper, Me)
        End If
        If player.HasTag(ACCEPTED_TAG) Then
            Return New MasterKnapperQuestAcceptedDialog(player, knapper, Me)
        End If
        Return New MasterKnapperIntroductionDialog(player, knapper, Me)
    End Function
End Class
