Friend Class THINDLADialog
    Inherits StatefulDialog

    Friend Const INTRODUCTION_TAG = "thindla-introduction"
    Friend Const ACCEPTED_TAG = "thindla-quest-accepted"
    Private ReadOnly player As ICharacter
    Protected Overrides Function CreateSubdialog() As IDialog
        If player.HasTag(INTRODUCTION_TAG) Then
            If player.HasTag(ACCEPTED_TAG) Then
                Return New THINDLAQuestAcceptedDialog(player, Me)
            End If
            Return New THINDLAQuestOfferedDialog(player, Me)
        End If
        Return New THINDLAIntroductionDialog(player, Me)
    End Function
    Public Sub New(player As ICharacter)
        Me.player = player
    End Sub
End Class
