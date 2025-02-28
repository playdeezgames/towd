Imports towd.data

Public Interface IWorld
    Sub Initialize()
    Sub Abandon()
    Function CreateCharacter(characterType As ICharacterType, location As ILocation) As ICharacter
    Function CreateLocation(locationType As ILocationType, map As IMap, column As Integer, row As Integer) As ILocation
    Function CreateMap(mapType As IMapType, columns As Integer, rows As Integer) As IMap
    Property Avatar As ICharacter
End Interface
