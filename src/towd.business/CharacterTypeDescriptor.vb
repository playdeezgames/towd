Imports towd.data

Friend Class CharacterTypeDescriptor
    Implements ICharacterType
    Public ReadOnly Property CharacterType As CharacterType Implements ICharacterType.CharacterType

    Public ReadOnly Property Name As String Implements ICharacterType.Name
    Sub New(characterType As CharacterType, name As String)
        Me.CharacterType = characterType
        Me.Name = name
    End Sub
End Class
