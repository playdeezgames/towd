Imports System.Runtime.CompilerServices

Public Module LocationTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of String, ILocationType) =
        New List(Of ILocationType) From
        {
            New GrassLocationTypeDescriptor(),
            New PineLocationTypeDescriptor(),
            New RockLocationTypeDescriptor(),
            New CookingFireLocationTypeDescriptor(),
            New DirtLocationTypeDescriptor(),
            New PondLocationTypeDescriptor(),
            New FurnaceLocationTypeDescriptor()
        }.
        ToDictionary(Function(x) x.LocationType, Function(x) x)
    <Extension>
    Public Function ToLocationTypeDescriptor(locationType As String) As ILocationType
        Return Descriptors(locationType)
    End Function
End Module
