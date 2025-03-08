Imports System.Runtime.CompilerServices
Imports towd.data

Module LocationTypes
    Friend Descriptors As IReadOnlyDictionary(Of LocationType, ILocationType) =
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
    Friend Function ToDescriptor(locationType As LocationType) As ILocationType
        Return Descriptors(locationType)
    End Function
End Module
