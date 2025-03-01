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
End Class
