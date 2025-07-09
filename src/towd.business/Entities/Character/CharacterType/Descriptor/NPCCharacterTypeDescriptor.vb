Imports towd.data

Friend MustInherit Class NPCCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Protected Sub New(characterType As data.CharacterType, name As String)
        MyBase.New(characterType, name, Array.Empty(Of StatisticType))
    End Sub

    Public Overrides Function CanDialog(character As ICharacter) As Boolean
        Return False
    End Function

    Public Overrides Function StartDialog(character As ICharacter, otherCharacter As ICharacter) As IDialog
        Return Nothing
    End Function
End Class
