Public Interface IMap
    ReadOnly Property Id As Integer
    ReadOnly Property EntityType As IMapType
    Sub AdvanceTime(amount As Integer)

    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Function GetLocation(column As Integer, row As Integer) As ILocation
    ReadOnly Property Locations As IEnumerable(Of ILocation)
End Interface
