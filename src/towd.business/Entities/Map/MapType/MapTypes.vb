Imports System.Runtime.CompilerServices
Imports towd.data

Public Module MapTypes
    Friend Descriptors As IReadOnlyDictionary(Of String, IMapType) =
        New List(Of IMapType) From
        {
            New NormalMapTypeDescriptor()
        }.
        ToDictionary(Function(x) x.MapType, Function(x) x)
    <Extension>
    Public Function ToMapTypeDescriptor(mapType As String) As IMapType
        Return Descriptors(mapType)
    End Function

End Module
