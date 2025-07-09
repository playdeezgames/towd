Friend Class THINDLAsAssDialog
    Inherits StatefulDialog

    Private character As ICharacter
    Private otherCharacter As ICharacter

    Public Sub New(character As ICharacter, otherCharacter As ICharacter)
        Me.character = character
        Me.otherCharacter = otherCharacter
    End Sub

    Protected Overrides Function CreateSubdialog() As IDialog
        Throw New NotImplementedException()
    End Function
End Class
