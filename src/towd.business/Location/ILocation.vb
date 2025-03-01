Public Interface ILocation
    ReadOnly Property Id As Integer
    Property EntityType As ILocationType
    Sub AdvanceTime(amount As Integer)

    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
End Interface
