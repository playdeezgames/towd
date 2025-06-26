Imports System.Runtime.CompilerServices
Imports towd.data

Public Module VerbTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of VerbType, IVerbType) =
        New List(Of IVerbType) From
        {
            New CraftVerbTypeDescriptor(),
            New EatGrubVerbTypeDescriptor(),
            New EatFishVerbTypeDescriptor(),
            New AddFuelVerbTypeDescriptor()
        }.ToDictionary(Function(x) x.VerbType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(verbType As VerbType) As IVerbType
        Return Descriptors(verbType)
    End Function
End Module
