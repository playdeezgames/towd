Imports System.Text
Imports towd.data

Friend MustInherit Class LocationTypeDescriptor
    Implements ILocationType
    Public ReadOnly Property LocationType As LocationType Implements ILocationType.LocationType

    Public ReadOnly Property Name As String Implements ILocationType.Name
    Sub New(locationType As LocationType, name As String)
        Me.LocationType = locationType
        Me.Name = name
    End Sub

    Public MustOverride Sub Initialize(location As ILocation) Implements ILocationType.Initialize
    Public MustOverride Sub AdvanceTime(location As ILocation, amount As Integer) Implements ILocationType.AdvanceTime
    Public Overridable Function Describe(location As ILocation) As String Implements ILocationType.Describe
        Dim builder As New StringBuilder
        builder.AppendLine($"Location: [{location.Column}, {location.Row}]")
        builder.AppendLine($"Terrain: {Name}")
        Return builder.ToString
    End Function
End Class
