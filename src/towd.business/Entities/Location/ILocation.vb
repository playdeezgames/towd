Imports System.Security.Principal

Public Interface ILocation
    Inherits IEntity(Of ILocationType)
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property Neighbors As IReadOnlyDictionary(Of Direction, ILocation)
End Interface
