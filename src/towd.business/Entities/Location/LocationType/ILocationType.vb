Imports towd.data

Public Interface ILocationType
    ReadOnly Property LocationType As String
    ReadOnly Property Name As String
    Sub Initialize(location As ILocation)
    Sub AdvanceTime(location As ILocation, amount As Integer)
    Function Describe(location As ILocation) As String
    ReadOnly Property MapLegend As Char
    ReadOnly Property StatisticTypes As IEnumerable(Of String)
End Interface
