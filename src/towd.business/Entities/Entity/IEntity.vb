Imports towd.data

Public Interface IEntity(Of TEntityType)
    ReadOnly Property Id As Integer
    ReadOnly Property World As IWorld
    Property EntityType As TEntityType
    Sub AdvanceTime(amount As Integer)

    Sub SetStatistic(statisticType As String, value As Integer)
    Function GetStatistic(statisticType As String) As Integer
    Sub SetStatisticMinimum(statisticType As String, value As Integer)
    Function GetStatisticMinimum(statisticType As String) As Integer
    Sub SetStatisticMaximum(statisticType As String, value As Integer)
    Function GetStatisticMaximum(statisticType As String) As Integer
    Sub ChangeStatistic(statisticType As String, delta As Integer)
    Function HasStatistic(statisticType As String) As Boolean
    Sub ClearStatistic(statisticType As String)

    Sub SetTag(tagType As String, value As Boolean)
    Function HasTag(tagType As String) As Boolean
End Interface
