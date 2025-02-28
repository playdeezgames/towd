Public Interface ICharacter
    ReadOnly Property Id As Integer
    Property Location As ILocation
    ReadOnly Property CharacterType As ICharacterType
    Sub Move(direction As Direction)
End Interface
