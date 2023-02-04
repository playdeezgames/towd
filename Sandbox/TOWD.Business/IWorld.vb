Public Interface IWorld
    ReadOnly Property Maps As IEnumerable(Of IMap)
    Function CreateMap(name As String) As IMap
End Interface
