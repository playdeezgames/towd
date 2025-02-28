Imports towd.data

Public Interface IWorld
    Sub Initialize()
    Sub Abandon()
    Function CreateCharacter(characterType As CharacterType, location As ILocation) As ICharacter
    Function CreateLocation(locationType As LocationType) As ILocation
    Function CreateMap(mapType As MapType, columns As Integer, rows As Integer) As IMap
    Property Avatar As ICharacter
End Interface
