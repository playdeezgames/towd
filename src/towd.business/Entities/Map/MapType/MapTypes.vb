Imports System.Runtime.CompilerServices
Imports towd.data

Module MapTypes
    Friend Descriptors As IReadOnlyDictionary(Of String, IMapType) =
        New List(Of IMapType) From
        {
            New NormalMapTypeDescriptor()
        }.
        ToDictionary(Function(x) x.MapType, Function(x) x)
    <Extension>
    Friend Function ToMapTypeDescriptor(mapType As String) As IMapType
        Return Descriptors(mapType)
    End Function

End Module
