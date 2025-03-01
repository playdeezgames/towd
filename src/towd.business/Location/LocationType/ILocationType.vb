Imports towd.data

Public Interface ILocationType
    ReadOnly Property LocationType As LocationType
    ReadOnly Property Name As String
    Sub Initialize(location As ILocation)
End Interface
