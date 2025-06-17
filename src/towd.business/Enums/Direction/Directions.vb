Imports System.Runtime.CompilerServices

Public Module Directions
    Friend Descriptors As IReadOnlyDictionary(Of Direction, IDirection) =
        New List(Of IDirection) From
        {
            New DirectionDescriptor(Direction.North, "North", 0, -1),
            New DirectionDescriptor(Direction.East, "East", 1, 0),
            New DirectionDescriptor(Direction.South, "South", 0, 1),
            New DirectionDescriptor(Direction.West, "West", -1, 0)
        }.
        ToDictionary(Function(x) x.Direction, Function(x) x)
    <Extension>
    Public Function ToDescriptor(direction As Direction) As IDirection
        Return Descriptors(direction)
    End Function
End Module
