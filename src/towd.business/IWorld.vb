Imports towd.data

Public Interface IWorld
    Sub Initialize()
    Sub Abandon()
    Function CreateCharacter(characterType As CharacterType, location As ILocation) As ICharacter
    Function CreateLocation(locationType As LocationType) As ILocation
    Property Avatar As ICharacter
End Interface
