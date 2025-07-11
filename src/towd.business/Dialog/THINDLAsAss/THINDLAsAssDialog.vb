Friend Class THINDLAsAssDialog
    Inherits StatefulDialog

    Friend Const ASS_FED_TAG = "ass-fed"
    Private ReadOnly player As ICharacter
    Private ReadOnly ass As ICharacter


    Public Sub New(character As ICharacter, otherCharacter As ICharacter)
        Me.player = character
        Me.ass = otherCharacter
    End Sub

    Protected Overrides Function CreateSubdialog() As IDialog
        Return New THINDLAsAssIntroductionDialog(player, ass, Me)
    End Function
End Class
