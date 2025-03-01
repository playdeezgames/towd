Public Interface ILocation
    ReadOnly Property Id As Integer
    Property LocationType As ILocationType
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
End Interface
