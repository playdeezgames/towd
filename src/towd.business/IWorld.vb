Imports towd.data

Public Interface IWorld
    Sub Initialize()
    Sub Abandon()
    Function CreateCharacter(characterType As CharacterType) As ICharacter
    Property Avatar As ICharacter
End Interface
