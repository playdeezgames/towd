Public Interface IEntity(Of TEntityType)
    ReadOnly Property Id As Integer
    ReadOnly Property World As IWorld
    Sub AdvanceTime(amount As Integer)
    Property EntityType As TEntityType
End Interface
