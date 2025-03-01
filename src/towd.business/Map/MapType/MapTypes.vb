Imports System.Runtime.CompilerServices
Imports towd.data

Module MapTypes
    Friend Descriptors As IReadOnlyDictionary(Of MapType, IMapType) =
        New List(Of IMapType) From
        {
            New NormalMapTypeDescriptor()
        }.
        ToDictionary(Function(x) x.MapType, Function(x) x)
    <Extension>
    Friend Function ToDescriptor(mapType As MapType) As IMapType
        Return Descriptors(mapType)
    End Function

End Module
