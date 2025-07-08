Imports towd.data

Public Interface IWorld
    Sub Initialize()
    Sub Abandon()
    Function CreateCharacter(characterType As ICharacterType, location As ILocation) As ICharacter
    Function CreateLocation(locationType As ILocationType, map As IMap, column As Integer, row As Integer) As ILocation
    Function CreateMap(mapType As IMapType) As IMap
    Function CreateItem(itemType As IItemType) As IItem
    Property Avatar As ICharacter
    Sub AdvanceTime(amount As Integer)
    ReadOnly Property Characters As IEnumerable(Of ICharacter)
    ReadOnly Property Maps As IEnumerable(Of IMap)
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
