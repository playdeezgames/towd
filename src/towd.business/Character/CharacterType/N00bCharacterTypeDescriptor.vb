Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(data.CharacterType.N00b, "N00b")
    End Sub

    Public Overrides Sub AdvanceTime(character As ICharacter, amount As Integer)
        character.AppendMessage("-1 Satiety")
    End Sub
End Class
