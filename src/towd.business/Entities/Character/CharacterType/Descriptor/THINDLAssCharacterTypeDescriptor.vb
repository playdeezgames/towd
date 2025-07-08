Friend Class THINDLAssCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(data.CharacterType.THINDLAsAss, "THINDLA's Hairy Ass")
    End Sub

    Public Overrides Sub AdvanceTime(character As ICharacter, amount As Integer)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Initialize(character As ICharacter)
        Throw New NotImplementedException()
    End Sub
End Class
