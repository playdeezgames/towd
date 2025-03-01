Imports towd.data

Public Interface ICharacter
    ReadOnly Property Id As Integer
    Property Location As ILocation
    ReadOnly Property CharacterType As ICharacterType
    Sub Move(direction As Direction)
    ReadOnly Property CanDoAnyVerb As Boolean
    Function CanDoVerb(verbType As VerbType) As Boolean
    Sub SetFlag(flagType As FlagType, flagValue As Boolean)
    Function HasFlag(flagType As FlagType) As Boolean
    Sub AddMessage(ParamArray lines() As String)
    ReadOnly Property IsAvatar As Boolean
    ReadOnly Property HasMessages As Boolean
    Sub DismissMessage()
    ReadOnly Property CurrentMessage As String()
End Interface
