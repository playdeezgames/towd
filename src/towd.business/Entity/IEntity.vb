Imports towd.data

Public Interface IEntity(Of TEntityType)
    ReadOnly Property Id As Integer
    ReadOnly Property World As IWorld
    Sub AdvanceTime(amount As Integer)
    Property EntityType As TEntityType
    Sub SetStatistic(statisticType As StatisticType, value As Integer)
    Function GetStatistic(statisticType As StatisticType) As Integer
    Sub SetStatisticMinimum(statisticType As StatisticType, value As Integer)
    Function GetStatisticMinimum(statisticType As StatisticType) As Integer
    Sub SetStatisticMaximum(statisticType As StatisticType, value As Integer)
    Function GetStatisticMaximum(statisticType As StatisticType) As Integer
    Sub SetFlag(flagType As FlagType, flagValue As Boolean)
    Function HasFlag(flagType As FlagType) As Boolean
    Sub ChangeStatistic(statisticType As StatisticType, delta As Integer)
End Interface
