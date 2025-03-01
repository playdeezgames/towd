Public Interface IMap
    ReadOnly Property Id As Integer
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    ReadOnly Property MapType As IMapType
    Function GetLocation(column As Integer, row As Integer) As ILocation
    Sub AdvanceTime(amount As Integer)
    ReadOnly Property Locations As IEnumerable(Of ILocation)
End Interface
