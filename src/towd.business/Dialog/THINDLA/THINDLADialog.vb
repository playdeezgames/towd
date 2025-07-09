Friend Class THINDLADialog
    Inherits StatefulDialog

    Friend Const INTRODUCTION_TAG = "introduction"
    Friend Const ACCEPTED_TAG = "thindla-quest-accepted"
    Private ReadOnly player As ICharacter
    Private ReadOnly thindla As ICharacter
    Protected Overrides Function CreateSubdialog() As IDialog
        If thindla.HasTag(INTRODUCTION_TAG) Then
            If player.HasTag(ACCEPTED_TAG) Then
                Return New THINDLAQuestAcceptedDialog(player, thindla, Me)
            End If
            Return New THINDLAQuestOfferedDialog(player, thindla, Me)
        End If
        Return New THINDLAIntroductionDialog(player, thindla, Me)
    End Function
    Public Sub New(player As ICharacter, thindla As ICharacter)
        Me.player = player
        Me.thindla = thindla
    End Sub
End Class
