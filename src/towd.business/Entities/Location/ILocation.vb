Public Interface ILocation
    Inherits IEntity(Of ILocationType)
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property Neighbors As IReadOnlyDictionary(Of String, ILocation)
    ReadOnly Property Characters As IEnumerable(Of ICharacter)
    Function HasOtherCharacters(character As ICharacter) As Boolean
    Function GetOtherCharacters(character As ICharacter) As IEnumerable(Of ICharacter)
    Sub AddCharacter(character As ICharacter)
    Sub RemoveCharacter(character As ICharacter)
End Interface
