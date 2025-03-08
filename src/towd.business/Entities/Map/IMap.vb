Imports System.Security.Principal

Public Interface IMap
    Inherits IEntity(Of IMapType)
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Function GetLocation(column As Integer, row As Integer) As ILocation
    ReadOnly Property Locations As IEnumerable(Of ILocation)
End Interface
