Imports System.Text
Imports towd.data

Friend MustInherit Class LocationTypeDescriptor
    Implements ILocationType
    Public ReadOnly Property LocationType As String Implements ILocationType.LocationType
    Public ReadOnly Property Name As String Implements ILocationType.Name
    Public ReadOnly Property MapLegend As Char Implements ILocationType.MapLegend
    Public ReadOnly Property StatisticTypes As IEnumerable(Of String) Implements ILocationType.StatisticTypes
    Sub New(
           locationType As String,
           name As String,
           mapLegend As Char,
           statisticTypes As IEnumerable(Of String))
        Me.LocationType = locationType
        Me.Name = name
        Me.MapLegend = mapLegend
        Me.StatisticTypes = statisticTypes
    End Sub

    Public MustOverride Sub Initialize(location As ILocation) Implements ILocationType.Initialize
    Public MustOverride Sub AdvanceTime(location As ILocation, amount As Integer) Implements ILocationType.AdvanceTime
    Public Overridable Function Describe(location As ILocation) As IEnumerable(Of String) Implements ILocationType.Describe
        Return {
            $"Location: [{location.Column}, {location.Row}]",
            $"Terrain: {Name}"
            }
    End Function
End Class
