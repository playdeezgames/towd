Public Interface IWorld
    ReadOnly Property Maps As IEnumerable(Of IMap)
    Function CreateMap() As IMap
End Interface
