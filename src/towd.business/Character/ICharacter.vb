Imports towd.data

Public Interface ICharacter
    ReadOnly Property Id As Integer
    Property Location As ILocation
    ReadOnly Property CharacterType As ICharacterType
    Sub Move(direction As Direction)
    ReadOnly Property CanDoAnyVerb As Boolean
    Sub SetFlag(flagType As FlagType, flagValue As Boolean)
    Function HasFlag(flagType As FlagType) As Boolean
End Interface
