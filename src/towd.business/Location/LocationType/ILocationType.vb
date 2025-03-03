Imports towd.data

Public Interface ILocationType
    ReadOnly Property LocationType As LocationType
    ReadOnly Property Name As String
    Sub Initialize(location As ILocation)
    Sub AdvanceTime(location As ILocation, amount As Integer)
    Function Describe(location As ILocation) As String
End Interface
