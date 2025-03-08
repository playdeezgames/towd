Imports System.Runtime.CompilerServices

Public Module Deeds
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of data.Deed, IDeed) =
        New List(Of IDeed) From
        {
            New MoveDeedDescriptor(data.Deed.MoveOnce, "Move once", 1, 1, Array.Empty(Of data.Deed)()),
            New MoveDeedDescriptor(data.Deed.MoveTenTimes, "Move ten times", 10, 1, {data.Deed.MoveOnce}),
            New MoveDeedDescriptor(data.Deed.MoveHundredTimes, "Move a hundred times", 100, 1, {data.Deed.MoveTenTimes}),
            New MoveDeedDescriptor(data.Deed.MoveThousandTimes, "Move a thousand times", 1000, 1, {data.Deed.MoveHundredTimes})
        }.ToDictionary(Function(x) x.Deed, Function(x) x)
    <Extension>
    Public Function ToDescriptor(deed As data.Deed) As IDeed
        Return Descriptors(deed)
    End Function
End Module
