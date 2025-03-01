Friend MustInherit Class Entity(Of TEntityType, TEntityData)
    Inherits WorldDataClient
    Implements IEntity(Of TEntityType)

    Public Sub New(worldData As data.WorldData, entityId As Integer)
        MyBase.New(worldData)
        Me.Id = entityId
    End Sub

    Public ReadOnly Property Id As Integer Implements IEntity(Of TEntityType).Id

    Public ReadOnly Property World As IWorld Implements IEntity(Of TEntityType).World
        Get
            Return New World(WorldData)
        End Get
    End Property

    Public MustOverride Property EntityType As TEntityType Implements IEntity(Of TEntityType).EntityType
    Public MustOverride Sub AdvanceTime(amount As Integer) Implements IEntity(Of TEntityType).AdvanceTime
End Class
