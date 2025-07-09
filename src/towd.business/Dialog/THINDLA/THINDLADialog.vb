Friend Class THINDLADialog
    Inherits StatefulDialog

    Friend Const INTRODUCTION_TAG = "introduction"
    Friend Const ACCEPTED_TAG = "thindla-quest-accepted"
    Private ReadOnly player As ICharacter
    Private ReadOnly thindla As ICharacter
    Protected Overrides Function CreateSubdialog() As IDialog
        If thindla.HasTag(INTRODUCTION_TAG) Then
            Return New THINDLAQuestGiverDialog(player, thindla, Me)
        Else
            Return New THINDLAIntroductionDialog(player, thindla, Me)
        End If
    End Function
    Public Sub New(player As ICharacter, thindla As ICharacter)
        Me.player = player
        Me.thindla = thindla
    End Sub
End Class
