Friend Class THINDLADialog
    Inherits StatefulDialog

    Friend Const INTRODUCTION_TAG = "thindla-introduction"
    Friend Const ACCEPTED_TAG = "thindla-quest-accepted"
    Friend Const COMPLETED_TAG = "thindla-quest-completed"
    Private ReadOnly player As ICharacter
    Protected Overrides Function CreateSubdialog() As IDialog
        If player.HasTag(INTRODUCTION_TAG) Then
            If player.HasTag(COMPLETED_TAG) Then
                Return New THINDLAQuestCompletedDialog()
            End If
            If player.HasTag(ACCEPTED_TAG) Then
                If IsAssPresent() Then
                    Return New THINDLAQuestDeliveryDialog(player)
                End If
                Return New THINDLAQuestAcceptedDialog(player)
            End If
            Return New THINDLAQuestOfferedDialog(player)
        End If
        Return New THINDLAIntroductionDialog(player, Me)
    End Function

    Private Function IsAssPresent() As Boolean
        Return player.CurrentLocation.GetOtherCharacters(player).Any(Function(x) x.EntityType.CharacterType = CharacterType.THINDLAsAss)
    End Function

    Public Sub New(player As ICharacter)
        Me.player = player
    End Sub
End Class
